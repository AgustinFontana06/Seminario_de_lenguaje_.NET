using SGE.Dominio;
using SGE.Dominio.Comun;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Expedientes.Agregar;
using SGE.Aplicacion.Expedientes.Eliminar;
using SGE.Aplicacion.Expedientes.Modificar;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Tramites.Agregar;
using SGE.Aplicacion.Tramites.Modificar;
using SGE.Aplicacion.Tramites.Eliminar;
using SGE.Aplicacion.Autorizaciones;
using SGE.Infraestructura.Expedientes;
using SGE.Infraestructura.Tramites;
using SGE.Infraestructura.Autorizaciones;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes.ObtenerTodos;
using SGE.Aplicacion.Tramites.ObtenerExpedientePorId;

IExpedienteRepository repositorioExpediente = new ExpedienteTXTRepository();
ITramiteRepository repositorioTramite = new TramitesTXTRepository();
IAutorizacionService repositorioAutorizacion = new AutorizacionProvisionalService();
IActualizacionEstadoExpedienteService repositorioActualizacion = new ActualizacionEstadoExpedienteService(repositorioExpediente, repositorioTramite);

// Casos de uso principales
var agregarExpedienteUseCase = new AgregarExpedienteUseCase(repositorioExpediente, repositorioAutorizacion);
var eliminarExpedienteUseCase = new EliminarExpedienteUseCase(repositorioExpediente, repositorioTramite, repositorioAutorizacion);
var agregarTramiteUseCase = new AgregarTramiteUseCase(repositorioTramite, repositorioActualizacion, repositorioAutorizacion);
var eliminarTramiteUseCase = new EliminarTramiteUseCase(repositorioTramite, repositorioActualizacion, repositorioAutorizacion);
var modificarCaratulaExpedienteUseCase = new ModificarCaratulExpedienteUseCase(repositorioExpediente, repositorioAutorizacion);
var modificarEstadoExpediente = new CambiarEstadoUseCase(repositorioExpediente, repositorioAutorizacion);
var modificarTramiteUseCase = new ModificarTramiteUseCase(repositorioTramite, repositorioActualizacion, repositorioAutorizacion);
var obtenerTodosUseCase = new ObtenerTodosUseCase(repositorioExpediente);
var obtenerPorExpedienteIdUseCase = new ObtenerPorExpedienteIdUseCase(repositorioExpediente, repositorioTramite);

// Instancias para probar caminos de error (servicio que niega permisos)
var modificarCaratulaUseCaseRechaza = new ModificarCaratulExpedienteUseCase(repositorioExpediente, new RechazaAutorizacionService());
var modificarTramiteUseCaseRechaza = new ModificarTramiteUseCase(repositorioTramite, repositorioActualizacion, new RechazaAutorizacionService());
var cambiarEstadoUseCaseRechaza = new CambiarEstadoUseCase(repositorioExpediente, new RechazaAutorizacionService());

Guid idUsuario = Guid.NewGuid();

// Limpio los archivos de persistencia para la ejecución de pruebas
File.WriteAllText("expedientes.txt", string.Empty);
File.WriteAllText("tramites.txt", string.Empty);

Console.WriteLine("1-- Prueba: alta de expedientes (escenario institucional)");
try
{
    var req1 = new AgregarExpedienteRequest("Informe de Solicitud de Licencia de Funcionamiento", idUsuario);
    var resp1 = agregarExpedienteUseCase.Ejecutar(req1);
    Console.WriteLine($"[Éxito] expediente registrado. ID: {resp1.id}\n");

    var req2 = new AgregarExpedienteRequest("Acta de Inicio: Investigación Administrativa", idUsuario);
    var resp2 = agregarExpedienteUseCase.Ejecutar(req2);
    Console.WriteLine($"[Éxito] expediente registrado. ID: {resp2.id}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error de Negocio]: {ex.Message}\n");
}

// Camino de error: carátula vacía
try
{
    var reqInvalid = new AgregarExpedienteRequest("", idUsuario);
    agregarExpedienteUseCase.Ejecutar(reqInvalid);
    Console.WriteLine("[Error de prueba] Esperaba excepción por carátula vacía (falla la prueba)");
}
catch (Exception ex)
{
    Console.WriteLine($"[Camino de error - Validación]: {ex.Message}\n");
}

Console.WriteLine("2-- Prueba: alta de trámite asociado a expediente");
try
{
    var reqExp = new AgregarExpedienteRequest("Expediente: Evaluación Técnica - Proyecto Urbano", idUsuario);
    var respExp = agregarExpedienteUseCase.Ejecutar(reqExp);
    Console.WriteLine($"[Éxito] expediente registrado. ID: {respExp.id}\n");

    var reqTram = new AgregarTramiteRequest("Recepción de documentación técnica para evaluación", idUsuario, respExp.id);
    var respTram = agregarTramiteUseCase.Ejecutar(reqTram);
    Console.WriteLine($"[Éxito] trámite registrado. ID: {respTram.idTramite}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error de Negocio]: {ex.Message}\n");
}

Console.WriteLine("3-- Prueba: baja de expediente");
try
{
    var req = new AgregarExpedienteRequest("Expediente: Solicitud de Baja de Registro", idUsuario);
    var resp = agregarExpedienteUseCase.Ejecutar(req);
    Console.WriteLine($"[Éxito] expediente registrado. ID: {resp.id}\n");

    var reqEliminar = new EliminarExpedienteRequest(resp.id, idUsuario);
    var respEliminar = eliminarExpedienteUseCase.Ejecutar(reqEliminar);
    Console.WriteLine($"[Éxito] expediente eliminado. ID: {respEliminar.id}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error de Negocio]: {ex.Message}\n");
}

Console.WriteLine("4-- Prueba: baja de trámite");
try
{
    var reqExp = new AgregarExpedienteRequest("Permiso de obra menor - Ampliación Edificio", idUsuario);
    var respExp = agregarExpedienteUseCase.Ejecutar(reqExp);

    var reqTr = new AgregarTramiteRequest("Se remite al área de estudio para evaluación técnica", idUsuario, respExp.id);
    var respTr = agregarTramiteUseCase.Ejecutar(reqTr);

    var reqEliminarTr = new EliminarTramiteRequest(respTr.idTramite, idUsuario);
    var respEliminarTr = eliminarTramiteUseCase.Ejecutar(reqEliminarTr);
    Console.WriteLine($"[Éxito] trámite eliminado. ID: {respEliminarTr.idTramite}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error de Negocio]: {ex.Message}\n");
}

Console.WriteLine("5-- Prueba: modificación de carátula de expediente");
try
{
    var req = new AgregarExpedienteRequest("Expediente: Solicitud de Revisión Administrativa", idUsuario);
    var resp = agregarExpedienteUseCase.Ejecutar(req);
    var reqModificar = new ModificarCaratulaExpedienteRequest(resp.id, "Carátula actualizada: Inspección completada - Conforme", idUsuario);
    var respModificar = modificarCaratulaExpedienteUseCase.Ejecutar(reqModificar);
    Console.WriteLine($"[Éxito] carátula modificada. ID: {respModificar.id}, nueva carátula: {respModificar.texto}\n");
}
catch (AutorizacionException ex)
{
    Console.WriteLine($"[Error de Autorización]: {ex.Message}\n");
}
catch (EntidadNoEncontradaException ex)
{
    Console.WriteLine($"[Error de entidad no encontrada]: {ex.Message}\n");
}
catch (DominioException ex)
{
    Console.WriteLine($"[Error de dominio]: {ex.Message}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error inesperado]: {ex.Message}\n");
}

// Camino de error: modificar carátula sin permiso
try
{
    var req = new AgregarExpedienteRequest("Expediente: Prueba error - carátula", idUsuario);
    var resp = agregarExpedienteUseCase.Ejecutar(req);

    var reqModError = new ModificarCaratulaExpedienteRequest(resp.id, "Intento de cambio sin autorización", idUsuario);
    modificarCaratulaUseCaseRechaza.Ejecutar(reqModError);

    Console.WriteLine("[Error de prueba] Esperaba AutorizacionException (falla la prueba)");
}
catch (AutorizacionException ex)
{
    Console.WriteLine($"[Camino de error - Autorización]: denegado correctamente: {ex.Message}");
}

Console.WriteLine("6-- Prueba: modificación de trámite");
try
{
    var reqExp2 = new AgregarExpedienteRequest("Expediente: Solicitud de Permiso Administrativo", idUsuario);
    var respExp2 = agregarExpedienteUseCase.Ejecutar(reqExp2);

    var reqTr2 = new AgregarTramiteRequest("Presentación inicial de planos y memoria técnica", idUsuario, respExp2.id);
    var respTr2 = agregarTramiteUseCase.Ejecutar(reqTr2);

    var reqModificarTr = new ModificarTramiteRequest(respTr2.idTramite, "Versión actualizada: planos revisados y observaciones incorporadas", idUsuario);
    var respModificarTr = modificarTramiteUseCase.Ejecutar(reqModificarTr);
    Console.WriteLine($"[Éxito] trámite modificado. ID: {respModificarTr.id}, contenido nuevo: {respModificarTr.texto}\n");
}
catch (AutorizacionException ex)
{
    Console.WriteLine($"[Error de Autorización]: {ex.Message}\n");
}
catch (EntidadNoEncontradaException ex)
{
    Console.WriteLine($"[Error de entidad no encontrada]: {ex.Message}\n");
}
catch (DominioException ex)
{
    Console.WriteLine($"[Error de dominio]: {ex.Message}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error inesperado]: {ex.Message}\n");
}

// Camino de error: modificar trámite sin permiso y validaciones
try
{
    var reqExp = new AgregarExpedienteRequest("Expediente: Prueba error - trámite autorización", idUsuario);
    var respExp = agregarExpedienteUseCase.Ejecutar(reqExp);

    var reqTr = new AgregarTramiteRequest("Entrega inicial de antecedentes técnicos", idUsuario, respExp.id);
    var respTr = agregarTramiteUseCase.Ejecutar(reqTr);

    var reqModTrError = new ModificarTramiteRequest(respTr.idTramite, "Modificación sin autorización", idUsuario);
    modificarTramiteUseCaseRechaza.Ejecutar(reqModTrError);
    Console.WriteLine("[Error de prueba] Esperaba AutorizacionException (falla la prueba)");
}
catch (AutorizacionException ex)
{
    Console.WriteLine($"[Camino de error - Autorización trámite]: denegado correctamente: {ex.Message}");
}
catch (EntidadNoEncontradaException ex)
{
    Console.WriteLine($"[Camino de error - Entidad no encontrada]: {ex.Message}");
}

try
{
    var reqExp = new AgregarExpedienteRequest("Expediente: Prueba error - trámite validación", idUsuario);
    var respExp = agregarExpedienteUseCase.Ejecutar(reqExp);

    var reqTr = new AgregarTramiteRequest("Entrega de documentación técnica", idUsuario, respExp.id);
    var respTr = agregarTramiteUseCase.Ejecutar(reqTr);

    var reqModTrInvalid = new ModificarTramiteRequest(respTr.idTramite, "", idUsuario); // texto vacío -> DominioException
    modificarTramiteUseCase.Ejecutar(reqModTrInvalid);
    Console.WriteLine("[Error de prueba] Esperaba DominioException (falla la prueba)");
}
catch (DominioException ex)
{
    Console.WriteLine($"[Camino de error - Validación dominio trámite]: {ex.Message}");
}

// Últimas pruebas: listados
Console.WriteLine("7-- Prueba: ObtenerTodos de expedientes");
try
{
    var response = obtenerTodosUseCase.Ejecutar();
    Console.WriteLine($"[Éxito] Se encontraron {response.Expedientes.Count()} expedientes:");
    foreach (var exp in response.Expedientes)
    {
        Console.WriteLine($"- ID: {exp.Id} | Carátula: {exp.Caratula} | Estado: {exp.Estado}");
    }
}
catch (DominioException ex)
{
    Console.WriteLine($"[Error de Negocio]: {ex.Message}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error inesperado]: {ex.Message}");
}

Console.WriteLine("8-- Prueba: ObtenerPorExpedienteId de trámites");
try
{
    var request1 = new AgregarExpedienteRequest("Expediente para listar trámites", idUsuario);
    var response1 = agregarExpedienteUseCase.Ejecutar(request1);

    var request2 = new AgregarTramiteRequest("Trámite 1: Recepción de documentación", idUsuario, response1.id);
    agregarTramiteUseCase.Ejecutar(request2);

    var request3 = new AgregarTramiteRequest("Trámite 2: Evaluación técnica preliminar", idUsuario, response1.id);
    agregarTramiteUseCase.Ejecutar(request3);

    var request4 = new ObtenerPorExpedienteIdRequest(response1.id);
    var response4 = obtenerPorExpedienteIdUseCase.Ejecutar(request4);
    Console.WriteLine($"[Éxito] Se encontraron {response4.tramites.Count()} trámites:");
    foreach (var tramite in response4.tramites)
    {
        Console.WriteLine($"  - ID: {tramite.Id} | Etiqueta: {tramite.Etiqueta} | Contenido: {tramite.Contenido}");
    }
}
catch (DominioException ex)
{
    Console.WriteLine($"[Error de Negocio]: {ex.Message}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error inesperado]: {ex.Message}");
}