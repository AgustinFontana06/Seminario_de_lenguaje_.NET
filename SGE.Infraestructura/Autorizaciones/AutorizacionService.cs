namespace SGE.Infraestructura.Autorizaciones;
using SGE.Dominio.Permisos;
using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuario;
using SGE.Aplicacion.Excepciones;
using System.Runtime.CompilerServices;

public class AutorizacionService : IAutorizacionService
{

    private readonly IUsuarioRepository _usuarioRepository;
    public AutorizacionService(IUsuarioRepository repositorioUsuario)
    {
        _usuarioRepository = repositorioUsuario;
    }
     public bool PoseeElPermiso(Guid idUsuario, Permiso permisoRequerido)
    {
        var usuario = _usuarioRepository.obtenerPorEmail(idUsuario);
        if (usuario == null) throw new EntidadNoEncontradaException($"el usuario con id {idUsuario} no se encuentra en la base de datos.");
        bool permitido = false;
        //infiere el tipo de dato.
        var permisos = usuario.ListaDePermisos.ToList();
        foreach(Permiso p in permisos)
        {
            if(p == permisoRequerido)
            {
                permitido = true;
            }
        }
        return permitido;
    }
}