namespace SGE.Aplicacion.Usuarios.Modificar;

public record ModificarMisDatosRequest(Guid id, string nombre, string email, string contraseña){}
