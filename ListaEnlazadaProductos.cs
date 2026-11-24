using System;

namespace SistemaGestionPedidos
{
    public class NodoProducto
    {
        public Producto Dato { get; set; }
        public NodoProducto Siguiente { get; set; }

        public NodoProducto(Producto dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }

    public class ListaEnlazadaProductos
    {
        private NodoProducto cabeza;

        public ListaEnlazadaProductos()
        {
            cabeza = null;
        }

        public void Insertar(Producto producto)
        {
            NodoProducto nuevo = new NodoProducto(producto);
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                NodoProducto actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
        }

        public Producto BuscarPorId(int id)
        {
            NodoProducto actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Id == id)
                    return actual.Dato;
                actual = actual.Siguiente;
            }
            return null;
        }

        public Producto BuscarPorNombre(string nombre)
        {
            NodoProducto actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                    return actual.Dato;
                actual = actual.Siguiente;
            }
            return null;
        }

        public bool Eliminar(int id)
        {
            if (cabeza == null) return false;

            if (cabeza.Dato.Id == id)
            {
                cabeza = cabeza.Siguiente;
                return true;
            }

            NodoProducto actual = cabeza;
            while (actual.Siguiente != null && actual.Siguiente.Dato.Id != id)
            {
                actual = actual.Siguiente;
            }

            if (actual.Siguiente != null)
            {
                actual.Siguiente = actual.Siguiente.Siguiente;
                return true;
            }

            return false;
        }

        public void MostrarTodos()
        {
            if (cabeza == null)
            {
                Console.WriteLine("No hay productos registrados en el inventario.");
                return;
            }

            NodoProducto actual = cabeza;
            while (actual != null)
            {
                Console.WriteLine(actual.Dato.ToString());
                actual = actual.Siguiente;
            }
        }
    }
}