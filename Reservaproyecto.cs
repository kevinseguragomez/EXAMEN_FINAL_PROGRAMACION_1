namespace Hotel.Modelos
{
    /// <summary>
    /// Clase que representa una reserva de habitación en el hotel.
    /// Basada en la plantilla proporcionada por el instructor.
    /// </summary>
    public class Reserva
    {
        // Propiedades con encapsulamiento (get; set;) - modificadores apropiados
        public string Cliente { get; set; }
        public int Habitacion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int Noches { get; set; }
        public double PrecioPorNoche { get; set; }

        // Constructor con todos los parámetros necesarios
        public Reserva(string cliente, int habitacion, DateTime fechaIngreso, int noches, double precioPorNoche)
        {
            Cliente = cliente;
            Habitacion = habitacion;
            FechaIngreso = fechaIngreso;
            Noches = noches;
            PrecioPorNoche = precioPorNoche;
        }

        /// <summary>
        /// Calcula el costo total de la reserva (Noches * PrecioPorNoche).
        /// </summary>
        public double CalcularTotal()
        {
            return Noches * PrecioPorNoche;
        }

        /// <summary>
        /// Retorna un resumen legible de la reserva.
        /// </summary>
        public string ObtenerResumen()
        {
            return $"Cliente: {Cliente} | Hab: {Habitacion} | " +
                   $"Ingreso: {FechaIngreso:dd/MM/yyyy} | " +
                   $"Noches: {Noches} | Precio/Noche: Q{PrecioPorNoche:F2} | " +
                   $"Total: Q{CalcularTotal():F2}";
        }

        /// <summary>
        /// Convierte la reserva a formato CSV para guardar en archivo.
        /// </summary>
        public string AFormatoCSV()
        {
            return $"{Cliente},{Habitacion},{FechaIngreso:yyyy-MM-dd},{Noches},{PrecioPorNoche}";
        }

        /// <summary>
        /// Crea una Reserva a partir de una línea CSV.
        /// </summary>
        public static Reserva DesdeCsv(string linea)
        {
            string[] partes = linea.Split(',');
            return new Reserva(
                partes[0],
                int.Parse(partes[1]),
                DateTime.Parse(partes[2]),
                int.Parse(partes[3]),
                double.Parse(partes[4])
            );
        }
    }
}
