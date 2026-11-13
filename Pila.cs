using System;

namespace Proyecto4
{
    public class Pila
    {
        private Nodo tope;

        private class Nodo
        {
            public char Dato;
            public Nodo Siguiente;

            public Nodo(char dato)
            {
                Dato = dato;
            }
        }

        public bool Vacia()
        {
            return tope == null;
        }

        public void Apilar(char dato)
        {
            Nodo nuevo = new Nodo(dato);
            nuevo.Siguiente = tope;
            tope = nuevo;
        }

        public char Desapilar()
        {
            if (Vacia())
                return '\0';

            char dato = tope.Dato;
            tope = tope.Siguiente;
            return dato;
        }

        public char Tope()
        {
            if (Vacia())
                return '\0';

            return tope.Dato;
        }
    }
}