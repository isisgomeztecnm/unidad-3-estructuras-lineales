using System;

namespace proyecto3
{
    internal class Program
    {
        static PilaLlamadas gestor = new PilaLlamadas();

        static void Main()
        {
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("    SIMULADOR DE GESTIÓN DE LLAMADAS    ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Atender llamada (Entrante)");
                Console.WriteLine("2. Finalizar llamada");
                Console.WriteLine("3. Recuperar / Ver última llamada");
                Console.WriteLine("4. Mostrar llamadas activas");
                Console.WriteLine("5. Buscar llamada");
                Console.WriteLine("6. Eliminar llamada específica");
                Console.WriteLine("0. Salir");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = -1;
                }

                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        Console.Write("Nombre o número del contacto: ");
                        string id = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(id))
                        {
                            Llamada nueva = new Llamada(id);
                            gestor.Atender(nueva);
                            Console.WriteLine("\n[✓] Llamada atendida y registrada en la pila.");
                        }
                        else
                        {
                            Console.WriteLine("\n[X] Identificador inválido.");
                        }
                        Pausar();
                        break;

                    case 2:
                        Llamada finalizada = gestor.Finalizar();
                        if (finalizada != null)
                            Console.WriteLine($"[✓] Se ha finalizado la llamada: {finalizada}");
                        else
                            Console.WriteLine("[!] No hay llamadas activas para finalizar.");
                        Pausar();
                        break;

                    case 3:
                        Llamada ultima = gestor.RecuperarUltima();
                        if (ultima != null)
                            Console.WriteLine($"[i] Última llamada recibida: {ultima}");
                        else
                            Console.WriteLine("[!] No hay llamadas en la pila.");
                        Pausar();
                        break;

                    case 4:
                        Console.WriteLine("--- LLAMADAS ACTIVAS (de más reciente a más antigua) ---");
                        gestor.MostrarActivas();
                        Pausar();
                        break;

                    case 5:
                        Console.Write("Ingrese el nombre/número a buscar: ");
                        string buscarId = Console.ReadLine();
                        Llamada encontrada = gestor.Buscar(buscarId);
                        if (encontrada != null)
                            Console.WriteLine($"\n[✓] Encontrada: {encontrada}");
                        else
                            Console.WriteLine($"\n[X] No se encontró la llamada '{buscarId}'.");
                        Pausar();
                        break;

                    case 6:
                        Console.Write("Ingrese el nombre/número a eliminar: ");
                        string eliminarId = Console.ReadLine();
                        bool exito = gestor.Eliminar(eliminarId);
                        if (exito)
                            Console.WriteLine($"\n[✓] Llamada '{eliminarId}' eliminada con éxito.");
                        else
                            Console.WriteLine($"\n[X] No se encontró la llamada '{eliminarId}'.");
                        Pausar();
                        break;
                }
            }
            while (opcion != 0);
        }

        private static void Pausar()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}