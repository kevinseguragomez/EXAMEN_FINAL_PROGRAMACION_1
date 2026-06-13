using Hotel.Modelos;

namespace Hotel.Modelos
{
    /// <summary>
    /// Clase que administra la lista de reservas del hotel.
    /// Usa List<Reserva> como colección en memoria temporal.
    /// La persistencia se hace en archivo de texto (reservas.csv).
    /// </summary>
    public class Hotel
    {
        // Colección de tipo List para almacenar reservas en memoria
        private List<Reserva> reservas;
        private const string ArchivoReservas = "reservas.csv";

        public Hotel()
        {
            reservas = new List<Reserva>();
            CargarDesdeArchivo();
        }

        // ──────────────────────────────────────────────
        // 1. REGISTRAR NUEVA RESERVA
        // ──────────────────────────────────────────────
        /// <summary>
        /// Agrega una nueva reserva a la colección y la persiste en el archivo.
        /// </summary>
        public void RegistrarReserva(Reserva reserva)
        {
            reservas.Add(reserva);
            GuardarEnArchivo();
            Console.WriteLine("\n✔  Reserva registrada correctamente.");
        }

        // ──────────────────────────────────────────────
        // 2. LISTAR TODAS LAS RESERVAS
        // ──────────────────────────────────────────────
        /// <summary>
        /// Muestra en consola todas las reservas registradas.
        /// </summary>
        public void ListarReservas()
        {
            if (reservas.Count == 0)
            {
                Console.WriteLine("\n  No hay reservas registradas.");
                return;
            }

            Console.WriteLine("\n══════════════════════════════════════════════════════════════════");
            Console.WriteLine("  LISTADO DE RESERVAS");
            Console.WriteLine("══════════════════════════════════════════════════════════════════");

            for (int i = 0; i < reservas.Count; i++)
            {
                Console.WriteLine($"  [{i + 1}] {reservas[i].ObtenerResumen()}");
            }

            Console.WriteLine("══════════════════════════════════════════════════════════════════");
            Console.WriteLine($"  Total de reservas: {reservas.Count}");
        }

        // ──────────────────────────────────────────────
        // 3. CALCULAR INGRESO TOTAL
        // ──────────────────────────────────────────────
        /// <summary>
        /// Calcula y muestra el ingreso total esperado de todas las reservas.
        /// </summary>
        public void CalcularIngresoTotal()
        {
            if (reservas.Count == 0)
            {
                Console.WriteLine("\n  No hay reservas para calcular ingresos.");
                return;
            }

            double total = 0;
            foreach (Reserva r in reservas)
            {
                total += r.CalcularTotal();
            }

            Console.WriteLine("\n══════════════════════════════════════════════════════════════════");
            Console.WriteLine($"  INGRESO TOTAL ESPERADO: Q{total:F2}");
            Console.WriteLine("══════════════════════════════════════════════════════════════════");
        }

        // ──────────────────────────────────────────────
        // 4. MOSTRAR RESERVA DE MAYOR DURACIÓN
        // ──────────────────────────────────────────────
        /// <summary>
        /// Busca y muestra la reserva con mayor número de noches.
        /// </summary>
        public void MostrarReservaMayorDuracion()
        {
            if (reservas.Count == 0)
            {
                Console.WriteLine("\n  No hay reservas registradas.");
                return;
            }

            Reserva mayor = reservas[0];
            foreach (Reserva r in reservas)
            {
                if (r.Noches > mayor.Noches)
                    mayor = r;
            }

            Console.WriteLine("\n══════════════════════════════════════════════════════════════════");
            Console.WriteLine("  RESERVA DE MAYOR DURACIÓN");
            Console.WriteLine("══════════════════════════════════════════════════════════════════");
            Console.WriteLine($"  {mayor.ObtenerResumen()}");
            Console.WriteLine("══════════════════════════════════════════════════════════════════");
        }

        // ──────────────────────────────────────────────
        // PERSISTENCIA EN ARCHIVO DE TEXTO
        // ──────────────────────────────────────────────
        private void GuardarEnArchivo()
        {
            List<string> lineas = new List<string>();
            foreach (Reserva r in reservas)
            {
                lineas.Add(r.AFormatoCSV());
            }
            File.WriteAllLines(ArchivoReservas, lineas);
        }

        private void CargarDesdeArchivo()
        {
            if (!File.Exists(ArchivoReservas)) return;

            string[] lineas = File.ReadAllLines(ArchivoReservas);
            foreach (string linea in lineas)
            {
                if (!string.IsNullOrWhiteSpace(linea))
                {
                    try
                    {
                        reservas.Add(Reserva.DesdeCsv(linea));
                    }
                    catch
                    {
                        // Ignorar líneas con formato inválido
                    }
                }
            }
        }
    }
}
