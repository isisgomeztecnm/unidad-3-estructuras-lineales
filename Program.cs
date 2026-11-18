using System;

namespace proyecto6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ListaDoble listaTareas = new ListaDoble();
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("=== GESTOR DE TAREAS (LISTA DOBLE) ===");
                Console.WriteLine("1. Agregar tarea");
                Console.WriteLine("2. Eliminar tarea");
                Console.WriteLine("3. Buscar tarea");
                Console.WriteLine("4. Modificar tarea");
                Console.WriteLine("5. Marcar tarea como terminada");
                Console.WriteLine("6. Mostrar tareas pendientes");
                Console.WriteLine("7. Mostrar tareas terminadas");
                Console.WriteLine("8. Ordenar tareas por ID");
                Console.WriteLine("9. Recorrer lista hacia adelante");
                Console.WriteLine("10. Recorrer lista hacia atrás");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = -1;
                }

                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese ID numérico de la tarea: ");
                        if (int.TryParse(Console.ReadLine(), out int idNuevo))
                        {
                            Console.Write("Ingrese descripción: ");
                            string desc = Console.ReadLine();
                            bool agregada = listaTareas.Agregar(new Tarea(idNuevo, desc));
                            Console.WriteLine(agregada ? "Tarea agregada correctamente." : "Error: Ya existe una tarea con ese ID.");
                        }
                        else
                        {
                            Console.WriteLine("ID no válido.");
                        }
                        break;

                    case 2:
                        Console.Write("Ingrese ID de la tarea a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int idEliminar))
                        {
                            Console.WriteLine(listaTareas.Eliminar(idEliminar) ? "Tarea eliminada." : "Tarea no encontrada.");
                        }
                        break;

                    case 3:
                        Console.Write("Ingrese ID de la tarea a buscar: ");
                        if (int.TryParse(Console.ReadLine(), out int idBuscar))
                        {
                            Tarea encontrada = listaTareas.Buscar(idBuscar);
                            Console.WriteLine(encontrada != null ? encontrada.ToString() : "Tarea no encontrada.");
                        }
                        break;

                    case 4:
                        Console.Write("Ingrese ID de la tarea a modificar: ");
                        if (int.TryParse(Console.ReadLine(), out int idMod))
                        {
                            Console.Write("Ingrese nueva descripción: ");
                            string nuevaDesc = Console.ReadLine();
                            Console.WriteLine(listaTareas.Modificar(idMod, nuevaDesc) ? "Tarea actualizada." : "Tarea no encontrada.");
                        }
                        break;

                    case 5:
                        Console.Write("Ingrese ID de la tarea a marcar como terminada: ");
                        if (int.TryParse(Console.ReadLine(), out int idTerm))
                        {
                            Console.WriteLine(listaTareas.MarcarComoTerminada(idTerm) ? "Tarea marcada como terminada." : "Tarea no encontrada.");
                        }
                        break;

                    case 6:
                        Console.WriteLine("--- TAREAS PENDIENTES ---");
                        listaTareas.MostrarPorEstado("Pendiente");
                        break;

                    case 7:
                        Console.WriteLine("--- TAREAS TERMINADAS ---");
                        listaTareas.MostrarPorEstado("Terminada");
                        break;

                    case 8:
                        listaTareas.OrdenarPorId();
                        Console.WriteLine("Tareas ordenadas por ID de menor a mayor.");
                        listaTareas.RecorrerAdelante();
                        break;

                    case 9:
                        Console.WriteLine("--- RECORRIDO HACIA ADELANTE ---");
                        listaTareas.RecorrerAdelante();
                        break;

                    case 10:
                        Console.WriteLine("--- RECORRIDO HACIA ATRÁS ---");
                        listaTareas.RecorrerAtras();
                        break;

                    case 0:
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }
    }
}



