using System;

namespace proyecto2
{
    public class Cola
    {
        private Nodo frente;
        private Nodo final;

        public bool EstaVacia()
        {
            return frente == null;
        }

        public void Encolar(Cliente cliente)
        {
            Nodo nuevo = new Nodo(cliente);
            if (frente == null)
            {
                frente = nuevo;
                final = nuevo;
            }
            else
            {
                final.Siguiente = nuevo;
                final = nuevo;
            }
        }

        public Cliente Desencolar()
        {
            if (frente == null)
                return null;

            Cliente cliente = frente.Datos;
            frente = frente.Siguiente;

            if (frente == null)
                final = null;

            return cliente;
        }

        public Cliente Buscar(string nombre)
        {
            Nodo actual = frente;
            while (actual != null)
            {
                if (actual.Datos.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return actual.Datos;
                }
                actual = actual.Siguiente;
            }
            return null;
        }

        public bool Eliminar(string nombre)
        {
            if (frente == null)
                return false;

            // Si el cliente a eliminar es el primero
            if (frente.Datos.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
            {
                frente = frente.Siguiente;
                if (frente == null)
                    final = null;
                return true;
            }

            // Buscar en el resto de la cola
            Nodo actual = frente;
            while (actual.Siguiente != null)
            {
                if (actual.Siguiente.Datos.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    if (actual.Siguiente == null)
                        final = actual;
                    return true;
                }
                actual = actual.Siguiente;
            }

            return false;
        }

        public void Mostrar()
        {
            if (frente == null)
            {
                Console.WriteLine("No hay clientes en espera.");
                return;
            }

            Nodo actual = frente;
            int posicion = 1;
            while (actual != null)
            {
                Console.WriteLine($"{posicion}. {actual.Datos}");
                actual = actual.Siguiente;
                posicion++;
            }
        }
    }
}