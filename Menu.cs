using System;

namespace proyecto_1
{
    internal class Program
    {
        static Cola pacientesEspera = new Cola();
        static Cola historialAtendidos = new Cola();

        static void Main(string[] args)
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=== CLINICA ===");
                Console.WriteLine("1. Registrar paciente");
                Console.WriteLine("2. Eliminar paciente");
                Console.WriteLine("3. Buscar paciente por expediente");
                Console.WriteLine("4. Mostrar pacientes en espera");
                Console.WriteLine("5. Atender siguiente paciente");
                Console.WriteLine("6. Asignar/Cambiar prioridad");
                Console.WriteLine("7. Mostrar historial de pacientes atendidos");
                Console.WriteLine("8. Consultar cantidad de pacientes en espera");
                Console.WriteLine("0. Salir");
                Console.Write("\nOpción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = -1; // Evita que el programa falle si se ingresa una letra
                }

                Console.Clear();
                switch (opcion)
                {
                    case 1:
                        Registrar();
                        break;
                    case 2:
                        Eliminar();
                        break;
                    case 3:
                        Buscar();
                        break;
                    case 4:
                        Console.WriteLine("=== PACIENTES EN ESPERA ===");
                        pacientesEspera.Mostrar();
                        Console.ReadKey();
                        break;
                    case 5:
                        Atender();
                        break;
                    case 6:
                        AsignarPrioridad();
                        break;
                    case 7:
                        Console.WriteLine("=== HISTORIAL DE ATENDIDOS ===");
                        historialAtendidos.Mostrar();
                        Console.ReadKey();
                        break;
                    case 8:
                        Console.WriteLine($"Cantidad de pacientes en espera: {pacientesEspera.Cantidad()}");
                        Console.ReadKey();
                        break;
                    case 0:
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
            while (opcion != 0);
        }

        static void Registrar()
        {
            Console.WriteLine("=== REGISTRAR PACIENTE ===");
            Console.Write("Expediente: ");
            int expediente = int.Parse(Console.ReadLine());

            // Validar que no exista un expediente duplicado en espera
            if (pacientesEspera.BuscarPorExpediente(expediente) != null)
            {
                Console.WriteLine("Error: Ya existe un paciente en espera con ese número de expediente.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Edad: ");
            int edad = int.Parse(Console.ReadLine());

            Console.Write("Prioridad (1 = Alta, 2 = Media, 3 = Baja): ");
            int prioridad = int.Parse(Console.ReadLine());

            paciente nuevoPaciente = new paciente(expediente, nombre, edad, prioridad);
            pacientesEspera.Encolar(nuevoPaciente);
            Console.WriteLine("\nPaciente registrado correctamente.");
            Console.ReadKey();
        }

        static void Eliminar()
        {
            Console.WriteLine("=== ELIMINAR PACIENTE ===");
            Console.Write("Ingrese el expediente del paciente a eliminar: ");
            int expediente = int.Parse(Console.ReadLine());

            if (pacientesEspera.EliminarPorExpediente(expediente))
            {
                Console.WriteLine("Paciente eliminado de la lista de espera.");
            }
            else
            {
                Console.WriteLine("No se encontró ningún paciente con ese expediente.");
            }
            Console.ReadKey();
        }

        static void Buscar()
        {
            Console.WriteLine("=== BUSCAR PACIENTE ===");
            Console.Write("Ingrese el expediente a buscar: ");
            int expediente = int.Parse(Console.ReadLine());

            paciente encontrado = pacientesEspera.BuscarPorExpediente(expediente);
            if (encontrado != null)
            {
                Console.WriteLine("\nPaciente encontrado en espera:");
                Console.WriteLine(encontrado);
            }
            else
            {
                Console.WriteLine("\nEl paciente no se encuentra en la sala de espera.");
            }
            Console.ReadKey();
        }

        static void Atender()
        {
            Console.WriteLine("=== ATENDER PACIENTE ===");
            paciente pacienteAtendido = pacientesEspera.Desencolar();

            if (pacienteAtendido == null)
            {
                Console.WriteLine("No hay pacientes en espera.");
            }
            else
            {
                Console.WriteLine("Atendiendo al siguiente paciente:");
                Console.WriteLine(pacienteAtendido);

                // Se guarda en el historial de atendidos
                historialAtendidos.Encolar(pacienteAtendido);
            }
            Console.ReadKey();
        }

        static void AsignarPrioridad()
        {
            Console.WriteLine("=== ASIGNAR / CAMBIAR PRIORIDAD ===");
            Console.Write("Ingrese el expediente del paciente: ");
            int expediente = int.Parse(Console.ReadLine());

            paciente p = pacientesEspera.BuscarPorExpediente(expediente);
            if (p == null)
            {
                Console.WriteLine("No se encontró un paciente en espera con ese expediente.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Paciente actual: {p}");
            Console.Write("Nueva prioridad (1 = Alta, 2 = Media, 3 = Baja): ");
            int nuevaPrioridad = int.Parse(Console.ReadLine());

            if (pacientesEspera.CambiarPrioridad(expediente, nuevaPrioridad))
            {
                Console.WriteLine("Prioridad actualizada y cola reordenada.");
            }
            Console.ReadKey();
        }
    }
}