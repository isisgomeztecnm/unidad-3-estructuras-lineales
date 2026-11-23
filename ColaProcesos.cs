using System;

namespace SistemaOperativoProcesos
{
    public class ColaProcesos
    {
        private Nodo frente;
        private Nodo final;
        public int Cantidad { get; private set; }

        public ColaProcesos()
        {
            frente = null;
            final = null;
            Cantidad = 0;
        }

        public bool EstaVacia() => frente == null;

        public void Encolar(Proceso proceso)
        {
            Nodo nuevo = new Nodo(proceso);
            if (EstaVacia())
            {
                frente = final = nuevo;
            }
            else
            {
                final.Siguiente = nuevo;
                final = nuevo;
            }
            Cantidad++;
        }

        public Proceso Desencolar()
        {
            if (EstaVacia()) return null;

            Proceso temp = frente.Dato;
            frente = frente.Siguiente;

            if (frente == null)
                final = null;

            Cantidad--;
            return temp;
        }

        public bool EliminarEspecifico(int pid)
        {
            if (EstaVacia()) return false;

            if (frente.Dato.PID == pid)
            {
                Desencolar();
                return true;
            }

            Nodo actual = frente;
            while (actual.Siguiente != null && actual.Siguiente.Dato.PID != pid)
            {
                actual = actual.Siguiente;
            }

            if (actual.Siguiente != null)
            {
                if (actual.Siguiente == final)
                    final = actual;

                actual.Siguiente = actual.Siguiente.Siguiente;
                Cantidad--;
                return true;
            }
            return false;
        }

        public void MostrarCola(string titulo)
        {
            Console.WriteLine($"--- {titulo} ({Cantidad} procesos) ---");
            if (EstaVacia())
            {
                Console.WriteLine("  [Cola vacía]");
                return;
            }

            Nodo actual = frente;
            while (actual != null)
            {
                Console.WriteLine("  -> " + actual.Dato.ToString());
                actual = actual.Siguiente;
            }
        }
    }
}