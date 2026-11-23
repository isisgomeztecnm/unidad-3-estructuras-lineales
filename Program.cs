using System;

namespace SimuladorBanco
{
    class Program
    {
        static void Main(string[] args)
        {
            SistemaAtencionBanco banco = new SistemaAtencionBanco();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n==========================================");
                Console.WriteLine("   SIMULADOR DE ATENCIÓN BANCARIA");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Generar turno");
                Console.WriteLine("2. Atender siguiente cliente");
                Console.WriteLine("3. Cancelar turno");
                Console.WriteLine("4. Mostrar clientes esperando");
                Console.WriteLine("5. Mostrar clientes atendidos");
                Console.WriteLine("6. Asignar / Cambiar prioridad a un turno");
                Console.WriteLine("7. Mostrar tiempo estimado");
                Console.WriteLine("8. Mostrar número de clientes en espera");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese el nombre del cliente: ");
                        string nombre = Console.ReadLine();
                        TipoCliente tipo = SeleccionarTipoCliente();
                        Cliente nuevo = banco.GenerarTurno(nombre, tipo);
                        Console.WriteLine($"\nTurno generado exitosamente: {nuevo}");
                        break;

                    case "2":
                        Cliente atendido = banco.AtenderCliente();
                        if (atendido != null)
                            Console.WriteLine($"\nAtendiendo ahora a: {atendido}");
                        else
                            Console.WriteLine("\nNo hay clientes en espera para atender.");
                        break;

                    case "3":
                        Console.Write("Ingrese el código del turno a cancelar (ej. N-001): ");
                        string turnoCancelar = Console.ReadLine();
                        if (banco.CancelarTurno(turnoCancelar))
                            Console.WriteLine($"\nEl turno {turnoCancelar.Trim().ToUpper()} ha sido cancelado.");
                        else
                            Console.WriteLine("\nNo se encontró ese turno en espera.");
                        break;

                    case "4":
                        banco.MostrarClientesEsperando();
                        break;

                    case "5":
                        banco.MostrarClientesAtendidos();
                        break;

                    case "6":
                        Console.Write("Ingrese el código del turno a modificar (ej. N-001): ");
                        string turnoPrioridad = Console.ReadLine();
                        Console.WriteLine("Seleccione la nueva prioridad:");
                        TipoCliente nuevaPrioridad = SeleccionarTipoCliente();
                        if (banco.AsignarPrioridad(turnoPrioridad, nuevaPrioridad))
                            Console.WriteLine($"\nPrioridad actualizada y fila reordenada para el turno {turnoPrioridad.Trim().ToUpper()}.");
                        else
                            Console.WriteLine("\nNo se encontró ese turno en espera.");
                        break;

                    case "7":
                        Console.Write("Ingrese un código de turno específico (o presione Enter para ver el tiempo general): ");
                        string turnoTiempo = Console.ReadLine();
                        banco.MostrarTiempoEstimado(turnoTiempo);
                        break;

                    case "8":
                        int cantidad = banco.ObtenerNumeroClientesEnEspera();
                        Console.WriteLine($"\nNúmero actual de clientes en espera: {cantidad}");
                        break;

                    case "0":
                        salir = true;
                        Console.WriteLine("Saliendo del simulador...");
                        break;

                    default:
                        Console.WriteLine("\nOpción no válida. Intente de nuevo.");
                        break;
                }
            }
        }

        static TipoCliente SeleccionarTipoCliente()
        {
            Console.WriteLine("Tipos de cliente:");
            Console.WriteLine("  1. Persona con prioridad (discapacidad, embarazo, etc.)");
            Console.WriteLine("  2. Adulto mayor");
            Console.WriteLine("  3. Cliente preferente");
            Console.WriteLine("  4. Cliente normal");
            Console.Write("Elija una opción (1-4): ");

            string seleccion = Console.ReadLine();
            switch (seleccion)
            {
                case "1":
                    return TipoCliente.PersonaConPrioridad;
                case "2":
                    return TipoCliente.AdultoMayor;
                case "3":
                    return TipoCliente.ClientePreferente;
                default:
                    return TipoCliente.ClienteNormal;
            }
        }
    }
}