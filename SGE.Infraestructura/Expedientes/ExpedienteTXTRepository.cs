namespace SGE.Infraestructura.Expedientes;
using SGE.Dominio.Expedientes;
using SGE.Infraestructura.RepositorioException;
using SGE.Aplicacion.Expedientes;

public class ExpedienteTXTRepository : IExpedienteRepository
{

    private readonly string _nombreArchivo = "expedientes.txt";
    public void Agregar(Expediente expedienteNuevo)
    {
    // Armamos la línea SOLO con los datos puros, separados por comas
        string lineaNueva = $"{expedienteNuevo.Id}|{expedienteNuevo.Caratula.Texto}|{expedienteNuevo.FechaCreacion}|{expedienteNuevo.FechaUltimaModificacion}|{expedienteNuevo.UsuarioUltimoCambio}|{expedienteNuevo.Estado}";

    // Environment.NewLine es el "Enter" oficial del sistema operativo. 
    // Lo agregamos al final para que el próximo expediente vaya en el renglón de abajo.
        File.AppendAllText("expedientes.txt", lineaNueva + Environment.NewLine);
    }   
    
    public Expediente? ObtenerPorId(Guid expedienteId)
    {
        Expediente? expedienteBuscado = null;
        var datos = ObtenerTodos();
    
        foreach (Expediente e in datos)
            {
                if (expedienteId == e.Id)
            {
                expedienteBuscado = e;
            }
        }
        return expedienteBuscado; 
    }
    public void Modificar(Expediente expedienteModificado)
    {
        if (!File.Exists(_nombreArchivo))
            throw new RepositorioException("no existe el archivo de expedientes");

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
    public void Eliminar(Guid expedienteId)
    {
        if (!File.Exists(_nombreArchivo))
            throw new RepositorioException("no existe el archivo de expedientes");

        // 1. Traemos TODOS los expedientes a la memoria y los convertimos en una Lista
        // Usamos .ToList() porque IEnumerable es de solo lectura y no nos dejaría usar .Remove()
        var expedientesLista = ObtenerTodos().ToList();

        // 2. Buscamos el expediente específico en la lista
        var expedienteAEliminar = expedientesLista.FirstOrDefault(e => e.Id == expedienteId);

        // 3. REGLA DEL TP: Si no existe, lanzamos excepción
        if (expedienteAEliminar == null)
        {
            throw new RepositorioException($"No se encontró un expediente con ID {expedienteId} para eliminar.");
        }

        // 4. Lo borramos de nuestra lista temporal en memoria
        expedientesLista.Remove(expedienteAEliminar);

        // 5. Sobrescribimos el archivo TXT completo (ahora sin ese expediente)
        ActualizarArchivo(expedientesLista);
    }

    private void ActualizarArchivo( IEnumerable<Expediente> expedientesActualizados)
    {
        // 1. Borramos el archivo viejo y lo preparamos para escribir de cero
        // File.WriteAllText lo sobrescribe automáticamente si ya existe.
    
        // 2. Usamos una lista de strings para armar las líneas
        var texto = new List<string>();

        foreach (var e in expedientesActualizados)
        {
            // Armamos la línea con el mismo formato que en el Agregar
            string linea = $"{e.Id}|{e.Caratula.Texto}|{e.FechaCreacion}|{e.FechaUltimaModificacion}|{e.UsuarioUltimoCambio}|{e.Estado}";
            texto.Add(linea);
        }

        // 3. Escribimos todas las líneas de golpe en el TXT
        File.WriteAllLines("expedientes.txt", texto);
    }



    private IEnumerable<string> LeerLineas()
    {
        string separador = Environment.NewLine;
        string texto = File.ReadAllText(_nombreArchivo);

        return texto.Split(separador);
    }
    public IEnumerable<Expediente> ObtenerTodos()
    {
        if (!File.Exists(_nombreArchivo)) return new List<Expediente>();

        List<Expediente> expedientes = new();

        foreach (string linea in LeerLineas())
        {
            if (string.IsNullOrWhiteSpace(linea)) continue;
            var datos = linea.Split("|");
            // debo parsea la informacion en string a datos nesesarios para la reconstrucion, podria hacerlo dentro del mismo metodo con dato pero por prolijidad decalre variables
            Guid id = Guid.Parse(datos[0]);
            Caratula caratula = new Caratula (datos[1]);
            DateTime fechaCreacion = DateTime.Parse(datos[2]);
            DateTime fechaUltimaModificacion = DateTime.Parse(datos[3]);
            Guid idUltimo = Guid.Parse(datos[4]);
            EstadoExpediente estado = Enum.Parse<EstadoExpediente>(datos[5]);

            Expediente exp = Expediente.Reconstruir(id, caratula, fechaCreacion, fechaUltimaModificacion,idUltimo, estado);
            expedientes.Add(exp);
        }

        return expedientes;
    }

    }
