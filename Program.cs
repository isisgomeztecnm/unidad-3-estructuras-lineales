using System;

namespace proyecto2
{
    internal class Program
    {
        static Cola espera = new Cola();
       
        static Cliente[] mesas = new Cliente[5];

        static void Main()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=== RESTAURANTE ===");
                Console.WriteLine("1. Registrar cliente y agregar a lista de espera");
                Console.WriteLine("2. Asignar mesa al siguiente cliente");
                Console.WriteLine("3. Cancelar reservación / Liberar mesa");
                Console.WriteLine("4. Buscar cliente");
                Console.WriteLine("5. Eliminar cliente de la lista de espera");
                Console.WriteLine("6. Mostrar lista de espera");
                Console.WriteLine("7. Mostrar mesas disponibles");
                Console.WriteLine("8. Mostrar mesas ocupadas");
                Console.WriteLine("0. Salir");
                Console.Write("\nOpción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = -1;
                }

                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        RegistrarYAgregar();
                        break;
                    case 2:
                        AsignarMesa();
                        break;
                    case 3:
                        CancelarReservacion();
                        break;
                    case 4:
                        BuscarCliente();
                        break;
                    case 5:
                        EliminarCliente();
                        break;
                    case 6:
                        espera.Mostrar();
                        Pausar();
                        break;
                    case 7:
                        MostrarMesasDisponibles();
                        break;
                    case 8:
                        MostrarMesasOcupadas();
                        break;
                    case 0:
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Pausar();
                        break;
                }
            }
            while (opcion != 0);
        }

        static void RegistrarYAgregar()
        {
            Console.Write("Nombre del cliente: ");
            string nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                Pausar();
                return;
            }

            Console.Write("Número de personas: ");
            if (!int.TryParse(Console.ReadLine(), out int personas) || personas <= 0)
            {
                Console.WriteLine("Cantidad de personas no válida.");
                Pausar();
                return;
            }

            Cliente nuevoCliente = new Cliente(nombre, personas);
            espera.Encolar(nuevoCliente);
            Console.WriteLine($"\nCliente '{nuevoCliente}' registrado y agregado a la lista de espera.");
            Pausar();
        }

        static void AsignarMesa()
        {
            if (espera.EstaVacia())
            {
                Console.WriteLine("No hay clientes en la lista de espera.");
                Pausar();
                return;
            }

            int mesaLibre = -1;
            for (int i = 0; i < mesas.Length; i++)
            {
                if (mesas[i] == null)
                {
                    mesaLibre = i;
                    break;
                }
            }

            if (mesaLibre == -1)
            {
                Console.WriteLine("Todas las mesas están ocupadas en este momento.");
            }
            else
            {
                Cliente cliente = espera.Desencolar();
                mesas[mesaLibre] = cliente;
                Console.WriteLine($"Mesa #{mesaLibre + 1} asignada a {cliente}.");
            }

            Pausar();
        }

        static void CancelarReservacion()
        {
            Console.Write("Ingrese el nombre del cliente o número de mesa (1-5) para cancelar/liberar: ");
            string entrada = Console.ReadLine();

            // Verificar si ingresó un número de mesa
            if (int.TryParse(entrada, out int numMesa) && numMesa >= 1 && numMesa <= mesas.Length)
            {
                if (mesas[numMesa - 1] != null)
                {
                    Console.WriteLine($"Reservación cancelada. Mesa #{numMesa} (de {mesas[numMesa - 1].Nombre}) ahora está libre.");
                    mesas[numMesa - 1] = null;
                }
                else
                {
                    Console.WriteLine($"La mesa #{numMesa} ya estaba disponible.");
                }
                Pausar();
                return;
            }

            
            for (int i = 0; i < mesas.Length; i++)
            {
                if (mesas[i] != null && mesas[i].Nombre.Equals(entrada, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Reservación en Mesa #{i + 1} cancelada para {mesas[i].Nombre}.");
                    mesas[i] = null;
                    Pausar();
                    return;
                }
            }

          
            if (espera.Eliminar(entrada))
            {
                Console.WriteLine($"Reservación en lista de espera cancelada para {entrada}.");
            }
            else
            {
                Console.WriteLine("No se encontró ninguna reservación activa ni en espera con ese dato.");
            }

            Pausar();
        }

        static void BuscarCliente()
        {
            Console.Write("Nombre del cliente a buscar: ");
            string nombre = Console.ReadLine();

            Cliente enEspera = espera.Buscar(nombre);
            if (enEspera != null)
            {
                Console.WriteLine($"Encontrado en LISTA DE ESPERA: {enEspera}");
                Pausar();
                return;
            }

           
            for (int i = 0; i < mesas.Length; i++)
            {
                if (mesas[i] != null && mesas[i].Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Encontrado en MESA #{i + 1}: {mesas[i]}");
                    Pausar();
                    return;
                }
            }

            Console.WriteLine("Cliente no encontrado en el restaurante.");
            Pausar();
        }

        static void EliminarCliente()
        {
            Console.Write("Nombre del cliente a eliminar de la lista de espera: ");
            string nombre = Console.ReadLine();

            if (espera.Eliminar(nombre))
            {
                Console.WriteLine($"Cliente '{nombre}' eliminado de la lista de espera.");
            }
            else
            {
                Console.WriteLine($"No se encontró a '{nombre}' en la lista de espera.");
            }

            Pausar();
        }

        static void MostrarMesasDisponibles()
        {
            Console.WriteLine("--- MESAS DISPONIBLES ---");
            bool hayDisponibles = false;

            for (int i = 0; i < mesas.Length; i++)
            {
                if (mesas[i] == null)
                {
                    Console.WriteLine($"Mesa #{i + 1}: Disponible");
                    hayDisponibles = true;
                }
            }

            if (!hayDisponibles)
            {
                Console.WriteLine("No hay mesas disponibles.");
            }

            Pausar();
        }

        static void MostrarMesasOcupadas()
        {
            Console.WriteLine("--- MESAS OCUPADAS ---");
            bool hayOcupadas = false;

            for (int i = 0; i < mesas.Length; i++)
            {
                if (mesas[i] != null)
                {
                    Console.WriteLine($"Mesa #{i + 1}: Ocupada por {mesas[i]}");
                    hayOcupadas = true;
                }
            }

            if (!hayOcupadas)
            {
                Console.WriteLine("Todas las mesas están libres.");
            }

            Pausar();
        }

        static void Pausar()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}













