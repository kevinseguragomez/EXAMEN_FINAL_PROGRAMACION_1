using Hotel.Modelos;

namespace Hotel.App
{
    /// <summary>
    /// Punto de entrada de la aplicación de consola.
    /// Presenta el menú principal y gestiona la interacción con el usuario.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Instancia del hotel (carga reservas previas del archivo)
            Hotel.Modelos.Hotel hotel = new Hotel.Modelos.Hotel();

            bool salir = false;

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine()?.Trim() ?? "";

                switch (opcion)
                {
                    case "1":
                        RegistrarNuevaReserva(hotel);
                        break;
                    case "2":
                        hotel.ListarReservas();
                        break;
                    case "3":
                        hotel.CalcularIngresoTotal();
                        break;
                    case "4":
                        hotel.MostrarReservaMayorDuracion();
                        break;
                    case "5":
                        salir = true;
                        Console.WriteLine("\n  ¡Hasta luego!\n");
                        break;
                    default:
                        Console.WriteLine("\n  Opción no válida. Intente de nuevo.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\n  Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        // ──────────────────────────────────────────────
        // MENÚ PRINCIPAL
        // ──────────────────────────────────────────────
        static void MostrarMenu()
        {
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║     SISTEMA DE RESERVAS - HOTEL          ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║  1. Registrar nueva reserva              ║");
            Console.WriteLine("║  2. Listar todas las reservas            ║");
            Console.WriteLine("║  3. Calcular ingreso total               ║");
            Console.WriteLine("║  4. Reserva de mayor duración            ║");
            Console.WriteLine("║  5. Salir                                ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write("  Seleccione una opción: ");
        }

        // ──────────────────────────────────────────────
        // REGISTRO DE NUEVA RESERVA (lectura de datos)
        // ──────────────────────────────────────────────
        static void RegistrarNuevaReserva(Hotel.Modelos.Hotel hotel)
        {
            Console.WriteLine("\n── NUEVA RESERVA ─────────────────────────");

            Console.Write("  Nombre del cliente   : ");
            string cliente = Console.ReadLine()?.Trim() ?? "Sin nombre";

            int habitacion = LeerEntero("  Número de habitación : ");

            Console.Write("  Fecha de ingreso (DD/MM/AAAA): ");
            DateTime fechaIngreso;
            while (!DateTime.TryParseExact(
                Console.ReadLine()?.Trim(),
                "dd/MM/yyyy",
                null,
                System.Globalization.DateTimeStyles.None,
                out fechaIngreso))
            {
                Console.Write("  Formato inválido. Ingrese DD/MM/AAAA: ");
            }

            int noches = LeerEntero("  Número de noches     : ");

            double precio = LeerDecimal("  Precio por noche (Q) : ");

            Reserva nueva = new Reserva(cliente, habitacion, fechaIngreso, noches, precio);
            hotel.RegistrarReserva(nueva);
        }

        // ──────────────────────────────────────────────
        // HELPERS DE LECTURA CON VALIDACIÓN
        // ──────────────────────────────────────────────
        static int LeerEntero(string mensaje)
        {
            int valor;
            Console.Write(mensaje);
            while (!int.TryParse(Console.ReadLine(), out valor) || valor <= 0)
            {
                Console.Write($"  Valor inválido. {mensaje}");
            }
            return valor;
        }

        static double LeerDecimal(string mensaje)
        {
            double valor;
            Console.Write(mensaje);
            while (!double.TryParse(Console.ReadLine(), out valor) || valor <= 0)
            {
                Console.Write($"  Valor inválido. {mensaje}");
            }
            return valor;
        }
    }
}
