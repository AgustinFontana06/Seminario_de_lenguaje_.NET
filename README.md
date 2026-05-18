# SGE - Instrucciones para probar Program.cs

Este documento explica cómo ejecutar y probar la funcionalidad que se desarrolla desde `SGE.Consola/Program.cs` en este espacio de trabajo.

Requisitos

- .NET SDK (versión compatible con los proyectos del repositorio, por ejemplo .NET 10 o la versión indicada en los csproj).
- Terminal o un IDE (VS / VS Code).

Pasos rápidos (desde la raíz del repositorio)

1. Abrir una terminal y situarse en la carpeta del repo:

```bash
cd /Users/sans6114/Documents/desarrollo/proyectosDotnet/Seminario_de_lenguaje_.NET
```

2. Ejecutar el proyecto de consola:

````bash
```bash
dotnet run --project SGE.Consola/SGE.Consola.csproj
````

Alternativa: entrar en la carpeta del proyecto y ejecutar:

```bash
cd SGE.Consola
dotnet run
```

Qué hace `Program.cs`

- Inicializa repositorios en disco (`ExpedienteTXTRepository`, `TramitesTXTRepository`) y un servicio de autorizaciones.
- Ejecuta una serie de pruebas:
  1. Alta de varios expedientes (y un caso con carátula vacía para probar el manejo de errores de dominio).
  2. Alta de un trámite asociado a un expediente recién creado.
  3. Baja (eliminación) de un expediente.
- Muestra mensajes por consola indicando éxito o errores.

Ejemplo de código (extracto de `Program.cs` relevante)

```csharp
// se limpian los ficheros
File.WriteAllText("expedientes.txt", string.Empty);
File.WriteAllText("tramites.txt", string.Empty);

Console.WriteLine("1-- probando alta de expediente");
// ejemplo de alta
var request1 = new AgregarExpedienteRequest("Caratula1", idUsuario);
var response1 = agregarExpedienteUseCase.Ejecutar(request1);
Console.WriteLine($"[Éxito] Producto agregado. El ID generado es: {response1.id}\n");

// ejemplo de alta de tramite
var request2 = new AgregarTramiteRequest("holaaa", idUsuario, response1.id);
var response2 = agregarTramiteUseCase.Ejecutar(request2);
Console.WriteLine($"[Éxito] tramite agregado. El ID generado es: {response2.idTramite}\n");

// ejemplo de eliminación
var requestEliminar = new EliminarExpedienteRequest(response1.id, idUsuario);
var responseEliminar = eliminarExpedienteUseCase.Ejecutar(requestEliminar);
Console.WriteLine($"[Éxito] expediente Eliminado, Id del producto {responseEliminar.id}\n");
```

Ejemplo de salida por consola (salida de ejemplo; los GUIDs y timestamps variarán en cada ejecución)

```
1-- probando alta de expediente
[Éxito] Producto agregado. El ID generado es: 3f1a2b4e-6c3d-4b2a-9f6c-1a2b3c4d5e6f
[Éxito] Producto agregado. El ID generado es: 7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d
[Éxito] Producto agregado. El ID generado es: a1b2c3d4-e5f6-7a8b-9c0d-e1f2a3b4c5d6
[Éxito] Producto agregado. El ID generado es: f0e1d2c3-b4a5-6978-8c7d-6b5a4e3d2c1b
[Error de Negocio]: La carátula no puede estar vacía

2-- probando alta de tramite
[Éxito] expediente agregado. El ID generado es: 11112222-3333-4444-5555-666677778888
[Éxito] tramite agregado. El ID generado es: 9999aaaa-bbbb-cccc-dddd-eeeeffff0000

3-- probando baja de expediente
[Éxito] expediente agregado. El ID generado es: 22223333-4444-5555-6666-777788889999
[Éxito] expediente Eliminado, Id del producto 22223333-4444-5555-6666-777788889999
```
