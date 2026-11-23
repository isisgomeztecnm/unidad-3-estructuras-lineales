using System;

namespace SistemaOperativoProcesos
{
    public class ListaEnlazadaProcesos
    {
        private Nodo cabeza;

        public ListaEnlazadaProcesos()
        {
            cabeza = null;
        }

        public void Insertar(Proceso proceso)
        {
            Nodo nuevo = new Nodo(proceso);
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
        }

        public Proceso BuscarPorPID(int pid)
        {
            Nodo actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.PID == pid)
                    return actual.Dato;
                actual = actual.Siguiente;
            }
            return null;
        }

        public bool EliminarPorPID(int pid)
        {
            if (cabeza == null) return false;

            if (cabeza.Dato.PID == pid)
            {
                cabeza = cabeza.Siguiente;
                return true;
            }

            Nodo actual = cabeza;
            while (actual.Siguiente != null && actual.Siguiente.Dato.PID != pid)
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
                Console.WriteLine("  (No hay procesos registrados en el sistema)");
                return;
            }

            Nodo actual = cabeza;
            while (actual != null)
            {
                Console.WriteLine("  " + actual.Dato.ToString());
                actual = actual.Siguiente;
            }
        }
    }
}