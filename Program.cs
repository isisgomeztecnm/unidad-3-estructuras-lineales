using System;

namespace Proyecto5
{
    internal class Program
    {
        static Pila atras = new Pila();
        static Pila adelante = new Pila();
        static string actual = "inicio";

        static void Main()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=== NAVEGADOR ===");
                Console.WriteLine($"Página actual: {actual}");
                Console.WriteLine("-----------------");
                Console.WriteLine("1. Visitar página");
                Console.WriteLine("2. Atrás");
                Console.WriteLine("3. Adelante");
                Console.WriteLine("4. Mostrar historial");
                Console.WriteLine("5. Eliminar historial");
                Console.WriteLine("6. Buscar una página");
                Console.WriteLine("7. Mostrar última página visitada");
                Console.WriteLine("0. Salir");
                Console.Write("\nOpción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = -1; 
                }

                switch (opcion)
                {
                    case 1:
                        atras.Push(actual);
                        adelante.Limpiar(); 
                        Console.Write("Nueva página: ");
                        actual = Console.ReadLine();
                        break;

                    case 2:
                        string anterior = atras.Pop();
                        if (anterior != null)
                        {
                            adelante.Push(actual);
                            actual = anterior;
                        }
                        break;

                    case 3:
                        string siguiente = adelante.Pop();
                        if (siguiente != null)
                        {
                            atras.Push(actual);
                            actual = siguiente;
                        }
                        break;

                    case 4:
                        Console.WriteLine("\n--- HISTORIAL ---");
                        Console.WriteLine($"Página actual: {actual}");
                        Console.WriteLine("Páginas anteriores (Atrás):");
                        atras.Mostrar();
                        Console.WriteLine("Páginas siguientes (Adelante):");
                        adelante.Mostrar();
                        Pausar();
                        break;

                    case 5:
                        atras.Limpiar();
                        adelante.Limpiar();
                        Console.WriteLine("\nHistorial eliminado correctamente.");
                        Pausar();
                        break;

                    case 6:
                        Console.Write("\nIngresa el nombre de la página a buscar: ");
                        string buscada = Console.ReadLine();

                        bool esActual = actual.Equals(buscada, StringComparison.OrdinalIgnoreCase);
                        bool enAtras = atras.Buscar(buscada);
                        bool enAdelante = adelante.Buscar(buscada);

                        if (esActual || enAtras || enAdelante)
                        {
                            Console.WriteLine($"La página '{buscada}' SÍ se encuentra en el historial.");
                        }
                        else
                        {
                            Console.WriteLine($"La página '{buscada}' NO se encontró.");
                        }
                        Pausar();
                        break;

                    case 7:
                        string ultima = atras.Peek();
                        if (ultima != null)
                        {
                            Console.WriteLine($"\nÚltima página visitada (anterior a la actual): {ultima}");
                        }
                        else
                        {
                            Console.WriteLine("\nNo hay páginas visitadas anteriormente.");
                        }
                        Pausar();
                        break;

                    case 0:
                        Console.WriteLine("\nSaliendo del navegador...");
                        break;

                    default:
                        Console.WriteLine("\nOpción no válida.");
                        Pausar();
                        break;
                }
            } while (opcion != 0);
        }

        
        static void Pausar()
        {
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}



