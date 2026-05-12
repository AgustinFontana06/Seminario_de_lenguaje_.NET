namespace SGE.Infraestructura.Expedientes;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes;

class RepositorioExpedienteTXT : IExpedienteRepository
{

    private readonly string _nombreArchivo = "expedientes.txt";

    public void Agregar(Expediente expedienteNuevo)
    {
        string separador = Environment.NewLine + Environment.NewLine;
        string bloque = CrearBloque(expedienteNuevo);

        File.AppendAllText(_nombreArchivo, bloque + separador);
    }
    
    public void Modificar(string textoNuevo, Guid expedienteId, Guid usuarioId)
    {
        if (!File.Exists(_nombreArchivo))
            throw new Exception("no existe el archivo de expedientes");

        Expediente? expAModificar = ObtenerPorId(expedienteId);
        if (expAModificar == null)
            throw new Exception($"expediente con id: {expedienteId} no fue encontrado.");

        var nuevaCaratula = new Caratula(textoNuevo);
        expAModificar.ModificarCaratula(nuevaCaratula, usuarioId);
        ActualizarArchivo(expAModificar);
    }
    
    public void CambiarEstado(EstadoExpediente estado,Guid expedienteId, Guid usuarioId)
    {
        if (!File.Exists(_nombreArchivo))
            throw new Exception("no existe el archivo de expedientes");

        Expediente? expAModificar = ObtenerPorId(expedienteId);
        if (expAModificar == null)
            throw new Exception($"expediente con id: {expedienteId} no fue encontrado.");
        expAModificar.CambiarEstado(estado, usuarioId);
        ActualizarArchivo(expAModificar);
    }
    public Expediente? ObtenerPorId(Guid expedienteId)
        {
            ListaExpedientes = ObtenerTodos();
            Expediente? expedienteIdEncontrado = null;
            foreach(Expediente exp in ListaExpedientes)
            {
                if (exp.Id == expedienteId)
                {
                    expedienteEncontrado =  exp;
                    break;
                }
            }
            return expedienteIdEncontrado;
        }

    public void Eliminar(Guid expedienteId)
    {
        if (!File.Exists(_nombreArchivo))
            throw new Exception("no existe el archivo de expedientes");

        string separador = Environment.NewLine + Environment.NewLine;
        string idLinea = "id del expediente: " + expedienteId;
        bool encontrado = false;
        List<string> bloquesRestantes = new();

        foreach (string bloque in LeerBloques())
        {
            if (bloque.Contains(idLinea))
            {
                encontrado = true;
                continue; // salta este bloque, no lo agrega
            }
            bloquesRestantes.Add(bloque);
        }

        if (!encontrado)
            throw new Exception($"expediente con id: {expedienteId} no fue encontrado.");

        File.WriteAllText(_nombreArchivo, string.Join(separador, bloquesRestantes) + separador);
    }

    private void ActualizarArchivo(Expediente expModificado)
    {
        string separador = Environment.NewLine + Environment.NewLine;
        string idLinea = "id del expediente: " + expModificado.Id;
        bool reemplazado = false;

        IEnumerable<string> bloquesActualizados = LeerBloques().Select(bloque =>
        {
            if (!reemplazado && bloque.Contains(idLinea))
            {
                reemplazado = true;
                return CrearBloque(expModificado);
            }
            return bloque;
        });

        if (!reemplazado)
            throw new Exception("expediente no encontrado en archivo");

        File.WriteAllText(_nombreArchivo, string.Join(separador, bloquesActualizados) + separador);
    }

    private static string CrearBloque(Expediente e)
    {
            string bloque =
            "id del expediente: " + e.Id + Environment.NewLine +
            "Caratula: " + e.Caratula.Texto + Environment.NewLine +
            "Fecha de creacion: " + e.FechaCreacion + Environment.NewLine;

            if (e.FechaUltimaModificacion != e.FechaCreacion)
            {
                bloque += "Fecha de ultima modificacion: " + e.FechaUltimaModificacion + Environment.NewLine;
            }

            bloque += "Estado: " + e.Estado;

        return bloque;
    }

    private IEnumerable<string> LeerBloques()
    {
        string separador = Environment.NewLine + Environment.NewLine;
        string texto = File.ReadAllText(_nombreArchivo);

        return texto.Split(separador, StringSplitOptions.RemoveEmptyEntries);
    }

    private Expediente ParsearBloque(string bloque)
    {
        var lineas = bloque.Split(Environment.NewLine);
        // indice 0 -> id, 1 -> textoCaratula, 2-> fecha creacion, 3 -> fecha modificacion, 4 -> estado
        Guid id = Guid.Parse(lineas[0].Replace("id del expediente: ", "").Trim());
        string textoCaratula = lineas[1].Replace("Caratula: ", "").Trim();
        DateTime fechaCreacion = DateTime.Parse(lineas[2].Replace("Fecha de creacion: ", "").Trim());

        DateTime fechaUltimaModificacion = fechaCreacion;
        EstadoExpediente estado;
        //asumo que el bloque recibido es de un expediente que nunca se modifico.
        if (lineas[3].StartsWith("Fecha de ultima modificacion: "))
        {
            fechaUltimaModificacion = DateTime.Parse(
            lineas[3].Replace("Fecha de ultima modificacion: ", "").Trim()
            );

            estado = Enum.Parse<EstadoExpediente>(
            lineas[4].Replace("Estado: ", "").Trim()
            );
        }
        else
        {
            estado = Enum.Parse<EstadoExpediente>(
            lineas[3].Replace("Estado: ", "").Trim()
        );
        }

        var caratula = new Caratula(textoCaratula);
    
        return Expediente.Reconstruir(id, caratula, fechaCreacion, fechaUltimaModificacion, Guid.Empty, estado);
    }

    public IEnumerable<Expediente> ObtenerTodos()
    {
        List<Expediente> expedientes = new();

        foreach (string bloque in LeerBloques())
        {
            expedientes.Add(ParsearBloque(bloque));
        }

        return expedientes;
    }
    }
