namespace SGE.Aplicacion.Tramites.Modificar;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Excepciones;
using SGE.Dominio.Tramites;
using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Permisos;

public class ModificarTramiteUseCase(ITramiteRepository repositorioTramite,IActualizacionEstadoExpedienteService actualizacion, IAutorizacionService autorizacion, IUnidadDeTrabajo udt)
{
    public ModificarTramiteResponse Ejecutar(ModificarTramiteRequest request, Guid idUsuario, Guid idTramite)
    {
        if(!autorizacion.PoseeElPermiso(idUsuario, Permiso.TramiteModificacion))
        {
            throw new AutorizacionException("No tienes permiso para modificar el tramite");
        }

        //obtengo su id
        Tramite? tramite = repositorioTramite.ObtenerPorId(idTramite);

        if(tramite == null)
        {
            //crear nueva excepcion, algo como ExcepcionIdentidadNoEncontrada
            throw new EntidadNoEncontradaException($"No se encontro el tramite con el id {idTramite}");
        }

        //creo caratula, ya paso las validaciones
        var nuevoContenido = new ContenidoTramite(request.texto);
        tramite.ModificarContenido(nuevoContenido, idUsuario);

        repositorioTramite.Modificar(tramite);

        actualizacion.ActualizacionEstado(tramite.ExpedienteId, idUsuario);

        udt.GuardarCambios();

        return new ModificarTramiteResponse(tramite.Id, tramite.Contenido.Texto);
    }
}