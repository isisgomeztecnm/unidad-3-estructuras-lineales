using System;

namespace SistemaOperativoProcesos
{
    class Program
    {
        static void Main(string[] args)
        {
            GestorProcesos so = new GestorProcesos();
            bool salir = false;

            // Carga inicial de prueba
            so.CrearProceso("System_Kernel", 1, 8);
            so.CrearProceso("Chrome.exe", 3, 5);
            so.CrearProceso("Spotify.exe", 4, 3);

            while (!salir)
            {
                Console.WriteLine("\n=============================================");
                Console.WriteLine("   SISTEMA DE CONTROL DE PROCESOS (SO)       ");
                Console.WriteLine("=============================================");
                Console.WriteLine("1. Crear proceso");
                Console.WriteLine("2. Ejecutar ciclo de CPU (Round-Robin)");
                Console.WriteLine("3. Bloquear proceso actual");
                Console.WriteLine("4. Desbloquear proceso");
                Console.WriteLine("5. Mostrar todos los procesos");
                Console.WriteLine("6. Buscar proceso por PID");
                Console.WriteLine("7. Eliminar proceso por PID");
                Console.WriteLine("8. Administrar colas");
                Console.WriteLine("9. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Nombre del proceso: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Prioridad (1-5): ");
                        int.TryParse(Console.ReadLine(), out int prio);
                        Console.Write("Tiempo de ejecución requerido (ej. 6): ");
                        int.TryParse(Console.ReadLine(), out int tiempo);
                        so.CrearProceso(nombre, prio > 0 ? prio : 3, tiempo > 0 ? tiempo : 4);
                        break;

                    case "2":
                        so.EjecutarProceso();
                        break;

                    case "3":
                        so.BloquearProceso();
                        break;

                    case "4":
                        so.DesbloquearProceso();
                        break;

                    case "5":
                        so.MostrarProcesos();
                        break;

                    case "6":
                        Console.Write("Ingrese el PID a buscar: ");
                        if (int.TryParse(Console.ReadLine(), out int pidBuscar))
                            so.BuscarProceso(pidBuscar);
                        break;

                    case "7":
                        Console.Write("Ingrese el PID a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int pidEliminar))
                            so.EliminarProceso(pidEliminar);
                        break;

                    case "8":
                        so.AdministrarColas();
                        break;

                    case "9":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }
    }
}