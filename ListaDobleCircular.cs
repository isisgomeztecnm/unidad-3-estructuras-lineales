using System;

namespace proyecto7
{
    public class ListaDobleCircular
    {
        private class Nodo
        {
            public Cancion Datos { get; set; }
            public Nodo Siguiente { get; set; }
            public Nodo Anterior { get; set; }

            public Nodo(Cancion cancion)
            {
                Datos = cancion;
            }
        }

        private Nodo cabeza;
        private Nodo actual;
        public int Cantidad { get; private set; }

        public bool EstaVacia => cabeza == null;

        public void Agregar(Cancion cancion)
        {
            Nodo nuevo = new Nodo(cancion);

            if (cabeza == null)
            {
                cabeza = nuevo;
                cabeza.Siguiente = cabeza;
                cabeza.Anterior = cabeza;
                actual = cabeza;
            }
            else
            {
                Nodo ultimo = cabeza.Anterior;

                ultimo.Siguiente = nuevo;
                nuevo.Anterior = ultimo;
                nuevo.Siguiente = cabeza;
                cabeza.Anterior = nuevo;
            }
            Cantidad++;
        }

        public bool Eliminar(string nombre)
        {
            if (cabeza == null) return false;

            Nodo temp = cabeza;
            for (int i = 0; i < Cantidad; i++)
            {
                if (temp.Datos.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    if (Cantidad == 1)
                    {
                        cabeza = null;
                        actual = null;
                    }
                    else
                    {
                        temp.Anterior.Siguiente = temp.Siguiente;
                        temp.Siguiente.Anterior = temp.Anterior;

                        if (temp == cabeza)
                            cabeza = temp.Siguiente;

                        if (temp == actual)
                            actual = temp.Siguiente;
                    }
                    Cantidad--;
                    return true;
                }
                temp = temp.Siguiente;
            }
            return false;
        }

        public Cancion Buscar(string nombre)
        {
            if (cabeza == null) return null;

            Nodo temp = cabeza;
            for (int i = 0; i < Cantidad; i++)
            {
                if (temp.Datos.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    actual = temp; // Posiciona el puntero en la canción encontrada
                    return temp.Datos;
                }
                temp = temp.Siguiente;
            }
            return null;
        }

        public Cancion ObtenerActual()
        {
            return actual?.Datos;
        }

        public Cancion Siguiente(bool modoCircular)
        {
            if (actual == null) return null;

            // Si no es circular y estamos en la última canción
            if (!modoCircular && actual.Siguiente == cabeza)
            {
                return null;
            }

            actual = actual.Siguiente;
            return actual.Datos;
        }

        public Cancion Anterior(bool modoCircular)
        {
            if (actual == null) return null;

            // Si no es circular y estamos en la primera canción
            if (!modoCircular && actual == cabeza)
            {
                return null;
            }

            actual = actual.Anterior;
            return actual.Datos;
        }

        public void Mostrar()
        {
            if (cabeza == null)
            {
                Console.WriteLine("   (La playlist está vacía)");
                return;
            }

            Nodo temp = cabeza;
            for (int i = 0; i < Cantidad; i++)
            {
                string indicador = (temp == actual) ? "▶ [ACTUAL] " : "   ";
                Console.WriteLine($"{indicador}{i + 1}. {temp.Datos}");
                temp = temp.Siguiente;
            }
        }
    }
}