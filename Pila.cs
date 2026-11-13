using System;
using System.Collections.Generic;

namespace Proyecto5
{
    public class Pila
    {
        private Nodo tope;

        private class Nodo
        {
            public string Pagina;
            public Nodo Siguiente;

            public Nodo(string Pagina)
            {
                this.Pagina = Pagina;
            }
        }

        public void Push(string pagina)
        {
            Nodo nuevo = new Nodo(pagina);
            nuevo.Siguiente = tope;
            tope = nuevo;
        }

        public string Pop()
        {
            if (tope == null)
                return null;

            string pagina = tope.Pagina;
            tope = tope.Siguiente;

            return pagina;
        }

        // Muestra el elemento del tope sin eliminarlo
        public string Peek()
        {
            if (tope == null)
                return null;

            return tope.Pagina;
        }

       
        public void Mostrar()
        {
            if (tope == null)
            {
                Console.WriteLine("  (Vacío)");
                return;
            }

            Nodo actual = tope;
            while (actual != null)
            {
                Console.WriteLine($"  - {actual.Pagina}");
                actual = actual.Siguiente;
            }
        }

        // Busca una página dentro de la pila
        public bool Buscar(string paginaBuscada)
        {
            Nodo actual = tope;
            while (actual != null)
            {
                if (actual.Pagina.Equals(paginaBuscada, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }

        // Elimina todos los elementos de la pila
        public void Limpiar()
        {
            tope = null;
        }
    }
}