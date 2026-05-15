using SGE.Dominio;
using SGE.Dominio.Comun;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Expedientes.Agregar;
using SGE.Aplicacion.Expedientes.Eliminar;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Tramites.Agregar;
using SGE.Aplicacion.Autorizaciones;
using SGE.Infraestructura.Expedientes;
using SGE.Infraestructura.Tramites;
using SGE.Infraestructura.Autorizaciones;

IExpedienteRepository repositorioExpediente = new ExpedienteTXTRepository();
ITramiteRepository repositorioTramite = new TramitesTXTRepository();
IAutorizacionService repositorioAutorizacion = new AutorizacionProvisionalService();
IActualizacionEstadoExpedienteService repositorioActualizacion = new ActualizacionEstadoExpedienteService(repositorioExpediente, repositorioTramite);

var agregarExpedienteUseCase = new AgregarExpedienteUseCase(repositorioExpediente, repositorioAutorizacion);
var eliminarExpedienteUseCase = new EliminarExpedienteUseCase(repositorioExpediente, repositorioTramite, repositorioAutorizacion);
var agregarTramiteUseCase = new AgregarTramiteUseCase(repositorioTramite, repositorioActualizacion, repositorioAutorizacion);

Guid idUsuario = Guid.NewGuid();


Console.WriteLine("1-- probando agregar un expediente");
try
{
    /*
    var request1 = new AgregarExpedienteRequest("Caratula1", idUsuario);
    var response1 = agregarExpedienteUseCase.Ejecutar(request1);
    Console.WriteLine($"[Éxito] Producto agregado. El ID generado es: {response1.id}\n");
    */

    var request2 = new AgregarExpedienteRequest("Caratula2", idUsuario);
    var response2 = agregarExpedienteUseCase.Ejecutar(request2);
    Console.WriteLine($"[Éxito] Producto agregado. El ID generado es: {response2.id}\n");

    var request3 = new AgregarExpedienteRequest("Caratula3", idUsuario);
    var response3 = agregarExpedienteUseCase.Ejecutar(request3);
    Console.WriteLine($"[Éxito] Producto agregado. El ID generado es: {response3.id}\n");

    var request4 = new AgregarExpedienteRequest("Caratula4", idUsuario);
    var response4 = agregarExpedienteUseCase.Ejecutar(request4);
    Console.WriteLine($"[Éxito] Producto agregado. El ID generado es: {response4.id}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error de Negocio]: {ex.Message}\n");
}

try
{
    //probando bloque catch (caratula vacia)
    var request5 = new AgregarExpedienteRequest("", idUsuario);
    var response5 = agregarExpedienteUseCase.Ejecutar(request5);
    Console.WriteLine($"[Éxito] Producto agregado. El ID generado es: {response5.id}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error de Negocio]: {ex.Message}\n");
}


Console.WriteLine("2-- probando alta de tramite");
try
{
    var request1 = new AgregarExpedienteRequest("nuevo", idUsuario);
    var response1 = agregarExpedienteUseCase.Ejecutar(request1);
    Console.WriteLine($"[Éxito] expediente agregado. El ID generado es: {response1.id}\n");

    var request2 = new AgregarTramiteRequest("holaaa", idUsuario, response1.id);
    var response2 = agregarTramiteUseCase.Ejecutar(request2);
    Console.WriteLine($"[Éxito] tramite agregado. El ID generado es: {response2.idTramite}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error de Negocio]: {ex.Message}\n");
}







Console.WriteLine("3-- probando baja de expediente");
try
{
    var request1 = new AgregarExpedienteRequest("Caratula1", idUsuario);
    var response1 = agregarExpedienteUseCase.Ejecutar(request1);
    Console.WriteLine($"[Éxito] expediente agregado. El ID generado es: {response1.id}\n");

   var request = new EliminarExpedienteRequest(response1.id, idUsuario);
   var response = eliminarExpedienteUseCase.Ejecutar(request);
   Console.WriteLine($"[Éxito] expediente Eliminado, Id del producto {response.id}\n"); 
}
catch (Exception ex)
{
    Console.WriteLine($"[Error de Negocio]: {ex.Message}\n");
}
