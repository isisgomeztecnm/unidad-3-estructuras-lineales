using System;

namespace proyecto7
{
    class Program
    {
        static void Main(string[] args)
        {
            Reproductor reproductor = new Reproductor();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n==========================================");
                Console.WriteLine("       REPRODUCTOR MUSICAL       ");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Reproducir");
                Console.WriteLine("2. Pausar");
                Console.WriteLine("3. Siguiente canción");
                Console.WriteLine("4. Canción anterior");
                Console.WriteLine("5. Agregar canción");
                Console.WriteLine("6. Eliminar canción");
                Console.WriteLine("7. Buscar canción");
                Console.WriteLine("8. Mostrar playlist actual");
                Console.WriteLine("9. Crear nueva playlist");
                Console.WriteLine("10. Cambiar de playlist");
                Console.WriteLine("11. Activar/Desactivar reproducción circular");
                Console.WriteLine("0. Salir");
                Console.Write("\nSelecciona una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        reproductor.Reproducir();
                        break;
                    case "2":
                        reproductor.Pausar();
                        break;
                    case "3":
                        reproductor.Siguiente();
                        break;
                    case "4":
                        reproductor.Anterior();
                        break;
                    case "5":
                        Console.Write("Nombre de la canción: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Artista: ");
                        string artista = Console.ReadLine();
                        reproductor.AgregarCancion(nombre, artista);
                        break;
                    case "6":
                        Console.Write("Nombre exacto de la canción a eliminar: ");
                        reproductor.EliminarCancion(Console.ReadLine());
                        break;
                    case "7":
                        Console.Write("Buscar canción por nombre: ");
                        reproductor.BuscarCancion(Console.ReadLine());
                        break;
                    case "8":
                        reproductor.MostrarPlaylist();
                        break;
                    case "9":
                        Console.Write("Nombre de la nueva playlist: ");
                        reproductor.CrearPlaylist(Console.ReadLine());
                        break;
                    case "10":
                        reproductor.ListarPlaylists();
                        Console.Write("Escribe el nombre de la playlist a abrir: ");
                        reproductor.CambiarPlaylist(Console.ReadLine());
                        break;
                    case "11":
                        reproductor.AlternarModoCircular();
                        break;
                    case "0":
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



