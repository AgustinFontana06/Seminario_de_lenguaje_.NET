# SGE — Sistema de Gestión de Expedientes
## TP2 — Guía de prueba de endpoints desde Scalar

---

## Requisitos previos

1. Tener .NET 10 instalado
2. Clonar el repositorio y pararse en la carpeta `SGE.WebApi`
3. Ejecutar:
```bash
dotnet run
```
4. Abrir Scalar en el navegador: http://localhost:5049/scalar

---

## Credenciales de usuarios de prueba

| Nombre | Email | Contraseña | Característica |
|---|---|---|---|
| Admin del sistema | admin@sge.com | admin123 | Administrador (todos los permisos) |
| juan | juan@sge.com | juan123 | Usuario con todos los permisos |
| maria | maria@sge.com | maria123 | Usuario sin permisos |


---

## Cómo autenticarse en Scalar

1. Ejecutar `POST /api/login` con las credenciales deseadas
2. Copiar el token JWT de la respuesta
3. Tocar "Test request" en algun caso de uso de Scalar 
4. En el apartado "Header" crear un campo llamado "Authorization" donde al lado tendra la palabra "Bearer" y el token jwt

---

## Camino de usuario nuevo

### 1. Registrarse
**`POST /api/register`** — no requiere token

**Body de ejemplo:**
```json
{
  "nombre": "Carlos",
  "email": "carlos@sge.com",
  "contrasena": "carlos123"
}
```
Si los datos son válidos, se crea el usuario en la base de datos. Al ser un usuario nuevo, no cuenta con permisos, por lo que solo podrá realizar tareas de lectura.

### 2. Loguearse
**`POST /api/login`** — no requiere token

**Body:**
```json
{
  "email": "carlos@sge.com",
  "contrasena": "carlos123"
}
```
Devuelve un token JWT. Copiarlo y pegarlo en Scalar para autenticarse.

---

## Camino del administrador (camino feliz)

### 1. Loguearse
**`POST /api/login`**

**Body:**
```json
{
  "email": "admin@sge.com",
  "contrasena": "admin123"
}
```
Guardar el token generado y pegarlo en la parte de Authorization.

### 2. Modificar mis datos
**`PUT /usuarios/me`** — requiere token

**Body:**
```json
{
  "nombre": "Nuevo nombre",
  "email": "nuevo@sge.com",
  "contrasena": "nuevaContrasena123"
}
```

---

### Métodos de administrador

#### Listar usuarios
**`GET /usuarios/admin/listar`** — requiere token de administrador

Devuelve la lista de todos los usuarios registrados en el sistema.

#### Eliminar usuario
**`DELETE /usuarios/admin/eliminar/{id}`** — requiere token de administrador

Reemplazar `{id}` con el Guid del usuario a eliminar (obtenible desde el listado o la base de datos).

#### Agregar permisos a un usuario
**`PUT /usuarios/admin/agregar-permisos/{id}`** — requiere token de administrador

**Body:**
```json
{
  "permisos": ["ExpedienteAlta", "TramiteAlta"]
}
```

#### Eliminar permisos a un usuario
**`PUT /usuarios/admin/eliminar-permisos/{id}`** — requiere token de administrador

**Body:**
```json
{
  "permisos": ["TramiteAlta"]
}
```

---

### Métodos para expedientes

#### Obtener todos
**`GET /expedientes/obtener-todos`** — no requiere token

Devuelve la lista completa de expedientes con su estado actual.

#### Agregar expediente
**`POST /expedientes/agregar-expediente`** — requiere token + permiso `ExpedienteAlta`

**Body:**
```json
{
  "caratulaText": "Expediente de prueba"
}
```
Devuelve el `id` del expediente creado. Guardarlo para los pasos siguientes. El estado inicial es `RecienIniciado`.

#### Eliminar expediente
**`DELETE /expedientes/eliminar-expediente/{id}`** — requiere token + permiso `ExpedienteBaja`

Al eliminar el expediente, todos sus trámites asociados también son eliminados (baja en cascada).

#### Modificar carátula
**`PUT /expedientes/modificar-caratula/{id}`** — requiere token + permiso `ExpedienteModificacion`

**Body:**
```json
{
  "texto": "Carátula corregida"
}
```

#### Cambiar estado manualmente
**`PUT /expedientes/cambiar-estado/{id}`** — requiere token + permiso `ExpedienteModificacion`

**Body:**
```json
{
  "estado": "EnNotificacion"
}
```
Valores posibles: `RecienIniciado`, `ParaResolver`, `ConResolucion`, `EnNotificacion`, `Finalizado`.

---

### Métodos para trámites

#### Obtener por Id
**`GET /tramites/obtener-por/{id}`** — no requiere token

Devuelve los datos del trámite con el id indicado.

#### Obtener por expediente Id
**`GET /tramites/obtener-por-expediente/{expedienteId}`** — no requiere token

Devuelve todos los trámites asociados al expediente indicado, junto con los datos del expediente.

#### Agregar trámite
**`POST /tramites/agregar-tramite/{expedienteId}`** — requiere token + permiso `TramiteAlta`

Reemplazar `{expedienteId}` con el Guid del expediente al que se quiere asociar el trámite.

**Body:**
```json
{
  "contenidoText": "Contenido del trámite"
}
```
Al agregar el trámite, el estado del expediente se actualiza automáticamente según la etiqueta del último trámite.

#### Eliminar trámite
**`DELETE /tramites/eliminar-tramite/{id}`** — requiere token + permiso `TramiteBaja`

Al eliminar el trámite, el estado del expediente se recalcula automáticamente según el trámite que quede como último.

#### Modificar trámite
**`PUT /tramites/modificar-tramite/{id}`** — requiere token + permiso `TramiteModificacion`

**Body:**
```json
{
  "texto": "Contenido modificado"
}
```

---

## Camino de juan (camino feliz — usuario con permisos)

Juan cuenta con todos los permisos:

### 1. Loguearse
**`POST /api/login`**

```json
{
  "email": "juan@sge.com",
  "contrasena": "juan123"
}
```

### 2. Modificar mis datos
**`PUT /usuarios/me`** — igual que el administrador.

### Métodos de administrador
Si juan intenta usar cualquier endpoint de `/usuarios/admin/`, recibirá `403 Forbidden` porque no es administrador.

---

## Camino de maria (camino de errores — usuario sin permisos)

### 1. Loguearse
**`POST /api/login`**

```json
{
  "email": "maria@sge.com",
  "contrasena": "maria123"
}
```

### Métodos de administrador
Si maria intenta usar cualquier endpoint de `/usuarios/admin/`, recibirá `403 Forbidden`.

### Métodos para expedientes y trámites
Maria solo puede usar los endpoints de lectura (`GET`). Si intenta cualquier operación mutativa (agregar, modificar, eliminar), recibirá `403 Forbidden` por falta de permisos.
