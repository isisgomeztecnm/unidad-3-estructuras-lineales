using System;
using System.Collections.Generic;

namespace proyecto7
{
    public class Reproductor
    {
        private Dictionary<string, ListaDobleCircular> playlists;
        private string nombrePlaylistActiva;
        private bool estaReproduciendo;
        public bool ModoCircular { get; private set; }

        public Reproductor()
        {
            playlists = new Dictionary<string, ListaDobleCircular>(StringComparer.OrdinalIgnoreCase);
            ModoCircular = true; // Activado por defecto
            estaReproduciendo = false;

            // Crear una playlist por defecto con canciones de ejemplo
            CrearPlaylist("Favoritas");
            AgregarCancion("Bohemian Rhapsody", "Queen");
            AgregarCancion("Blinding Lights", "The Weeknd");
            AgregarCancion("Hotel California", "Eagles");
        }

        private ListaDobleCircular PlaylistActual => playlists[nombrePlaylistActiva];

        public void CrearPlaylist(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Nombre inválido.");
                return;
            }

            if (playlists.ContainsKey(nombre))
            {
                Console.WriteLine($"La playlist '{nombre}' ya existe.");
                return;
            }

            playlists[nombre] = new ListaDobleCircular();
            nombrePlaylistActiva = nombre;
            estaReproduciendo = false;
            Console.WriteLine($"Playlist '{nombre}' creada y seleccionada.");
        }

        public void CambiarPlaylist(string nombre)
        {
            if (playlists.ContainsKey(nombre))
            {
                nombrePlaylistActiva = nombre;
                estaReproduciendo = false;
                Console.WriteLine($"Cambiado a la playlist: {nombrePlaylistActiva}");
            }
            else
            {
                Console.WriteLine("Esa playlist no existe.");
            }
        }

        public void ListarPlaylists()
        {
            Console.WriteLine("\n--- Playlists Disponibles ---");
            foreach (var key in playlists.Keys)
            {
                string marca = key.Equals(nombrePlaylistActiva, StringComparison.OrdinalIgnoreCase) ? " (Activa)" : "";
                Console.WriteLine($"- {key} [{playlists[key].Cantidad} canciones]{marca}");
            }
        }

        public void AgregarCancion(string nombre, string artista)
        {
            PlaylistActual.Agregar(new Cancion(nombre, artista));
            Console.WriteLine($"Agregada: '{nombre}' de {artista} a '{nombrePlaylistActiva}'.");
        }

        public void EliminarCancion(string nombre)
        {
            if (PlaylistActual.Eliminar(nombre))
                Console.WriteLine($"Canción '{nombre}' eliminada.");
            else
                Console.WriteLine($"No se encontró la canción '{nombre}'.");
        }

        public void BuscarCancion(string nombre)
        {
            Cancion encontrada = PlaylistActual.Buscar(nombre);
            if (encontrada != null)
            {
                Console.WriteLine($"Encontrada y seleccionada: {encontrada}");
                Reproducir();
            }
            else
            {
                Console.WriteLine($"No se encontró '{nombre}' en la playlist actual.");
            }
        }

        public void Reproducir()
        {
            Cancion actual = PlaylistActual.ObtenerActual();
            if (actual == null)
            {
                Console.WriteLine("No hay canciones en la playlist para reproducir.");
                return;
            }
            estaReproduciendo = true;
            Console.WriteLine($"▶ Reproduciendo: {actual}");
        }

        public void Pausar()
        {
            Cancion actual = PlaylistActual.ObtenerActual();
            if (actual == null || !estaReproduciendo)
            {
                Console.WriteLine("No hay ninguna canción sonando actualmente.");
                return;
            }
            estaReproduciendo = false;
            Console.WriteLine($"⏸ Pausado: {actual}");
        }

        public void Siguiente()
        {
            Cancion sig = PlaylistActual.Siguiente(ModoCircular);
            if (sig != null)
            {
                estaReproduciendo = true;
                Console.WriteLine($"⏭ Siguiente: {sig}");
            }
            else
            {
                estaReproduciendo = false;
                Console.WriteLine("⏹ Fin de la playlist (Modo circular desactivado).");
            }
        }

        public void Anterior()
        {
            Cancion ant = PlaylistActual.Anterior(ModoCircular);
            if (ant != null)
            {
                estaReproduciendo = true;
                Console.WriteLine($"⏮ Anterior: {ant}");
            }
            else
            {
                Console.WriteLine("⏹ Estás en la primera canción (Modo circular desactivado).");
            }
        }

        public void AlternarModoCircular()
        {
            ModoCircular = !ModoCircular;
            Console.WriteLine($"🔁 Reproducción circular: {(ModoCircular ? "ACTIVADA" : "DESACTIVADA")}");
        }

        public void MostrarPlaylist()
        {
            Console.WriteLine($"\n=== Playlist: {nombrePlaylistActiva} | Circular: {(ModoCircular ? "ON" : "OFF")} ===");
            PlaylistActual.Mostrar();
        }
    }
}