namespace SGE.Infraestructura.Tramites;
using SGE.Dominio.Tramites;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Excepciones;
using SGE.Infraestructura.RepositorioException;

public class TramitesTXTRepository : ITramiteRepository
{
    private readonly string _nombreArchivo = "tramites.txt";

        public void Agregar(Tramite tramiteNuevo)
        {
            
            string lineaNueva = $"{tramiteNuevo.Id}|{tramiteNuevo.ExpedienteId}|{tramiteNuevo.Etiqueta}|{tramiteNuevo.Contenido.Texto}|{tramiteNuevo.FechaCreacion}|{tramiteNuevo.FechaUltimaModificacion}|{tramiteNuevo.UsuarioUltimoCambio}";

            // Environment.NewLine es el "Enter" oficial del sistema operativo. 
            // Lo agregamos al final para que el próximo expediente vaya en el renglón de abajo.
            File.AppendAllText("tramites.txt", lineaNueva + Environment.NewLine);
        }


        public IEnumerable<Tramite> ObtenerTodos()
        {
            if (!File.Exists(_nombreArchivo))
                return new List<Tramite>();

            var tramites = new List<Tramite>();
            foreach (string linea in File.ReadAllLines(_nombreArchivo))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var d = linea.Split("|");
                var tramite = Tramite.Reconstruir(
                    Guid.Parse(d[0]),
                    Guid.Parse(d[1]),
                    Enum.Parse<EtiquetaTramite>(d[2]),
                    new ContenidoTramite(d[3]),
                    DateTime.Parse(d[4]),
                    DateTime.Parse(d[5]),
                    Guid.Parse(d[6])
                );
                tramites.Add(tramite);
            }
            return tramites;
        }

        public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId)
        {
            return ObtenerTodos().Where(t => t.ExpedienteId == expedienteId);
        }

        public Tramite? ObtenerPorId(Guid tramiteId)
        {
            return ObtenerTodos().FirstOrDefault(t => t.Id == tramiteId);
        }

        public void Modificar(Tramite tramiteModificado)
        {
            if (!File.Exists(_nombreArchivo))
                throw new RepositorioException("no existe el TXT de tramites.");

            var tramitesLista = ObtenerTodos().ToList();
            int index = tramitesLista.FindIndex(t => t.Id == tramiteModificado.Id);

            if (index == -1)
                throw new EntidadNoEncontradaException($"No se encontró un trámite con ID {tramiteModificado.Id} para modificar.");

            tramitesLista[index] = tramiteModificado;
            ActualizarArchivo(tramitesLista);
        }

        public void Eliminar(Guid tramiteId)
        {
            if (!File.Exists(_nombreArchivo))
                throw new RepositorioException("no existe el TXT de tramites.");

            var tramitesLista = ObtenerTodos().ToList();
            var tramiteAEliminar = tramitesLista.FirstOrDefault(t => t.Id == tramiteId);

            if (tramiteAEliminar == null)
                throw new EntidadNoEncontradaException($"No se encontró un trámite con ID {tramiteId} para eliminar.");

            tramitesLista.Remove(tramiteAEliminar);
            ActualizarArchivo(tramitesLista);
        }

        private void ActualizarArchivo(List<Tramite> tramites)
        {
            var lineas = tramites.Select(t =>
                $"{t.Id}|{t.ExpedienteId}|{t.Etiqueta}|{t.Contenido.Texto}|{t.FechaCreacion}|{t.FechaUltimaModificacion}|{t.UsuarioUltimoCambio}");
            File.WriteAllLines(_nombreArchivo, lineas);
        }

}
