using System;

namespace Proyecto4
{
    public class PilaNumeros
    {
        private Nodo tope;

        private class Nodo
        {
            public double Dato;
            public Nodo Siguiente;

            public Nodo(double dato)
            {
                Dato = dato;
            }
        }

        public bool Vacia()
        {
            return tope == null;
        }

        public void Apilar(double dato)
        {
            Nodo nuevo = new Nodo(dato);
            nuevo.Siguiente = tope;
            tope = nuevo;
        }

        public double Desapilar()
        {
            if (Vacia())
                throw new InvalidOperationException("Error de sintaxis: faltan operandos.");

            double dato = tope.Dato;
            tope = tope.Siguiente;
            return dato;
        }
    }
}