# SGE - Instrucciones para probar Program.cs

Este documento detalla cómo ejecutar y probar la funcionalidad desarrollada desde `SGE.Consola/Program.cs` en este espacio de trabajo.

## Requisitos

- .NET SDK (compatible con la versión indicada en los proyectos, por ejemplo, .NET 10).
- Terminal (zsh) o IDE (Visual Studio / VS Code).

## Ejecución del proyecto

Desde la raíz del repositorio, ejecute el siguiente comando en la terminal:

```bash
dotnet run --project SGE.Consola/SGE.Consola.csproj
```

Alternativamente, puede situarse en la carpeta del proyecto y ejecutar la aplicación:

```bash
cd SGE.Consola
dotnet run
```

## Pruebas de Funcionalidad

El archivo `Program.cs` inicializa los repositorios de texto (`expedientes.txt` y `tramites.txt`), los limpia para partir de un estado inicial "vacío", y luego ejecuta de forma secuencial una batería de pruebas de los casos de uso para expedientes y trámites (caminos felices y caminos de error).

### Código de ejemplo

A continuación, un extracto de `Program.cs` que ejecuta el alta de expedientes y captura correctamente las excepciones de dominio (validación) y autorización:

```csharp
// 1-- PRUEBA: ALTA DE EXPEDIENTES (ESCENARIO INSTITUCIONAL)
try
{
    var req1 = new AgregarExpedienteRequest("Informe de Solicitud de Licencia de Funcionamiento", idUsuario);
    var resp1 = agregarExpedienteUseCase.Ejecutar(req1);
    Console.WriteLine($"[Éxito] expediente registrado. ID: {resp1.id}\n");

    var req2 = new AgregarExpedienteRequest("Acta de Inicio: Investigación Administrativa", idUsuario);
    var resp2 = agregarExpedienteUseCase.Ejecutar(req2);
    Console.WriteLine($"[Éxito] expediente registrado. ID: {resp2.id}\n");
}
catch (AutorizacionException ex)
{
    Console.WriteLine($"[Error de Autorizacion]: {ex.Message}");
}
catch (DominioException ex)
{
    Console.WriteLine($"[Error de Negocio]: {ex.Message}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error inesperado]: {ex.Message}");
}
```

### Salida por consola producida

Al ejecutar el proyecto usando `dotnet run`, se generan salidas estructuradas validando tanto las operaciones correctas como los rechazos controlados. La salida producida es similar a la siguiente (los IDs variarán debido a que los GUIDs son generados dinámicamente cada vez):

```text
1-- PRUEBA: ALTA DE EXPEDIENTES (ESCENARIO INSTITUCIONAL)
[Éxito] expediente registrado. ID: 3f1a2b4e-6c3d-4b2a-9f6c-1a2b3c4d5e6f

[Éxito] expediente registrado. ID: 7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d

1.1-- CAMINO ERROR, CARATULA VACIA
[Camino de error - Validación]: La carátula no puede estar vacía

2-- PRUEBA: ALTA DE TRAMITE ASOCIADO A EXPEDIENTE
[Éxito] expediente registrado. ID: 11112222-3333-4444-5555-666677778888

[Éxito] trámite registrado. ID: 9999aaaa-bbbb-cccc-dddd-eeeeffff0000

3-- PRUEBA: BAJA DE EXPEDIENTE
[Éxito] expediente registrado. ID: 22223333-4444-5555-6666-777788889999

[Éxito] expediente eliminado. ID: 22223333-4444-5555-6666-777788889999
```

## Detalle de Funcionalidades Probadas en `Program.cs`

A continuación, se detalla cada bloque de prueba implementado en el archivo y el comportamiento esperado por consola:

### 1. Alta de Expedientes (Caminos exitosos y de error)

- **Funcionalidad:** Crea nuevos expedientes.
- **Camino de Error:** Intenta crear un expediente pasando un `string` vacío como carátula.
- **Salida esperada:** Mensajes de éxito indicando que los expedientes se registraron con sus respectivos IDs generados. Para el caso de error, se captura una `DominioException` y se muestra un mensaje advirtiendo que la carátula no puede estar vacía.

### 2. Alta de Trámite Asociado

- **Funcionalidad:** Registra un expediente y, usando el ID resultante, le asocia un nuevo trámite.
- **Salida esperada:** Mensajes de confirmación sucesivos: primero el alta del expediente y luego el alta del trámite con sus correspondientes IDs.

### 3 y 4. Bajas (Eliminación de Expedientes y Trámites)

- **Funcionalidad:** Recrea entidades (expediente en el caso 3; expediente y trámite en el caso 4) para proceder inmediatamente a su eliminación a través del caso de uso correspondiente.
- **Salida esperada:** Se notificarán las creaciones y, a continuación, un mensaje de éxito afirmando que la entidad vinculada a dicho ID ha sido eliminada correctamente del repositorio.

### 5. Modificaciones en Expedientes (Carátula y Estado)

- **Funcionalidad:** Altera atributos de un expediente existente. Primero, su carátula, y más adelante, su estado mediante el `CambiarEstadoUseCase`.
- **Caminos de Error:** Se inyecta temporalmente un servicio (`RechazaAutorizacionService`) que deniega cualquier operación para simular el fallo en la validación de permisos.
- **Salida esperada:** Se mostrarán los mensajes de actualización de la carátula y el estado. En los bloques de error correspondientes, se atraparán excepciones del tipo `AutorizacionException`, notificando en pantalla de forma controlada que los accesos fueron denegados.

### 6. Modificación de Trámites

- **Funcionalidad:** Actualiza el texto descriptivo del contenido de un trámite previamente creado.
- **Caminos de Error:** Evalúa dos vertientes. La primera es el rechazo por falta de permisos (Autorización). La segunda intenta actualizar el trámite enviando texto vacío (Dominio).
- **Salida esperada:** La versión válida mostrará el trámite actualizado. Los caminos alternativos arrojarán explícitamente y por separado mensajes capturados de error por "Autorización" y por "Validación", demostrando la robustez de las validaciones.

### 7 y 8. Consultas y Listados

- **Funcionalidad:** Consumen los métodos de solo lectura. `ObtenerTodosUseCase` itera imprimiendo el estado íntegro de la lista de expedientes. `ObtenerPorExpedienteIdUseCase` filtra exclusivamente los trámites de un expediente dado.
- **Salida esperada:** Imprimirá en consola la cantidad total de entidades halladas seguido por una lista detalla (con sub-viñetas) que incluye atributos fundamentales como ID, carátula/etiqueta y estado/contenido tanto de expedientes como de trámites listados de los archivos.
