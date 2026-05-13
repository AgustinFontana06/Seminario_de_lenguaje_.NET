namespace SGE.Infraestructura.Expedientes;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Comun;

class ExpedienteTXTRepository : IExpedienteRepository
{

    private readonly string _nombreArchivo = "expedientes.txt";

    public void Agregar(Expediente expedienteNuevo)
{
    // Armamos la línea SOLO con los datos puros, separados por comas
    string lineaNueva = $"{expedienteNuevo.Id},{expedienteNuevo.Caratula.Texto},{expedienteNuevo.FechaCreacion},{expedienteNuevo.FechaUltimaModificacion},{expedienteNuevo.Estado}";

    // Environment.NewLine es el "Enter" oficial del sistema operativo. 
    // Lo agregamos al final para que el próximo expediente vaya en el renglón de abajo.
    File.AppendAllText("expedientes.txt", lineaNueva + Environment.NewLine);
}
    
    public void Modificar(Expediente expedienteModificado)
    {
        if (!File.Exists(_nombreArchivo))
            throw new Exception("no existe el archivo de expedientes");
        
        var expedientesLista = ObtenerTodos().ToList();

        // Buscamos en qué posición (índice) de la lista está el expediente viejo
        int index = expedientesLista.FindIndex(e => e.Id == expedienteModificado.Id);// RARO PODRIA MODFICARSE

        // REGLA DEL TP: Si no existe, lanzamos excepción
        if (index == -1)
        {
            throw new RepositorioException($"No se encontró un expediente con ID {expedienteModificado.Id} para modificar.");
        }

        // Reemplazamos el expediente viejo por el modificado en esa posición
        expedientesLista[index] = expedienteModificado;
        
        // Sobrescribimos el archivo TXT completo con la lista actualizada
        ActualizarArchivo(expedientesLista);
    }
    public void cambiarEstado(EstadoExpediente estado,Guid expedienteId, Guid usuarioId)
    {
      //DEBO ESTO
    }
    public void Eliminar(Guid expedienteId)
    {
        if (!File.Exists(_nombreArchivo))
            throw new Exception("no existe el archivo de expedientes");

        // 1. Traemos TODOS los expedientes a la memoria y los convertimos en una Lista
        // Usamos .ToList() porque IEnumerable es de solo lectura y no nos dejaría usar .Remove()
        var expedientesLista = ObtenerTodos().ToList();

        // 2. Buscamos el expediente específico en la lista
        var expedienteAEliminar = expedientesList.FirstOrDefault(e => e.Id == id);

        // 3. REGLA DEL TP: Si no existe, lanzamos excepción
        if (expedienteAEliminar == null)
        {
            throw new RepositorioException($"No se encontró un expediente con ID {id} para eliminar.");
        }

        // 4. Lo borramos de nuestra lista temporal en memoria
        expedientesList.Remove(expedienteAEliminar);

        // 5. Sobrescribimos el archivo TXT completo (ahora sin ese expediente)
        ActualizarArchivo(expedientesLista);
    }

    private void ActualizarArchivo(Expediente expModificado)
    {
        // 1. Borramos el archivo viejo y lo preparamos para escribir de cero
        // File.WriteAllText lo sobrescribe automáticamente si ya existe.
    
        // 2. Usamos una lista de strings para armar las líneas
        var texto = new List<string>();

        foreach (var e in expedientesActualizados)
        {
            // Armamos la línea con el mismo formato que en el Agregar
            string linea = $"{e.Id},{e.Caratula.Texto},{e.FechaCreacion},{e.FechaUltimaModificacion},{e.Estado}";
            texto.Add(linea);
        }

        // 3. Escribimos todas las líneas de golpe en el TXT
        File.WriteAllLines("expedientes.txt", lineas);
    }



    private IEnumerable<string> LeerLineas()
    {
        string separador = Environment.NewLine;
        string texto = File.ReadAllText(_nombreArchivo);

        return texto.Split(separador);
    }


    public IEnumerable<Expediente> ObtenerTodos()
    {
        List<Expediente> expedientes = new();

        foreach (string linea in LeerLineas())
        {
            var datos = linea.Split(",");
            Expediente exp = Expediente.Reconstruir(datos[0],datos[1],datos[2],datos[3],datos[4],datos[5]);
            expedientes.Add(exp);
        }

        return expedientes;
    }
    }
