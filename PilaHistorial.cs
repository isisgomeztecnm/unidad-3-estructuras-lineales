using System;

namespace SistemaGestionPedidos
{
    public class NodoHistorial
    {
        public string Operacion { get; set; }
        public NodoHistorial Siguiente { get; set; }

        public NodoHistorial(string operacion)
        {
            Operacion = operacion;
            Siguiente = null;
        }
    }

    public class PilaHistorial
    {
        private NodoHistorial cima;

        public PilaHistorial()
        {
            cima = null;
        }

        public void Push(string descripcion)
        {
            string registro = $"[{DateTime.Now:HH:mm:ss}] {descripcion}";
            NodoHistorial nuevo = new NodoHistorial(registro);
            nuevo.Siguiente = cima;
            cima = nuevo;
        }

        public string Pop()
        {
            if (cima == null) return null;
            string dato = cima.Operacion;
            cima = cima.Siguiente;
            return dato;
        }

        public void MostrarHistorial()
        {
            if (cima == null)
            {
                Console.WriteLine("El historial de operaciones está vacío.");
                return;
            }

            NodoHistorial actual = cima;
            while (actual != null)
            {
                Console.WriteLine(actual.Operacion);
                actual = actual.Siguiente;
            }
        }
    }
}