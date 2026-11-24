using System;

namespace SistemaGestionPedidos
{
    public class NodoPedido
    {
        public Pedido Dato { get; set; }
        public NodoPedido Siguiente { get; set; }

        public NodoPedido(Pedido dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }

    public class ColaPedidos
    {
        protected NodoPedido frente;
        protected NodoPedido final;

        public ColaPedidos()
        {
            frente = null;
            final = null;
        }

        public bool EstaVacia() => frente == null;

        public virtual void Encolar(Pedido pedido)
        {
            NodoPedido nuevo = new NodoPedido(pedido);
            if (EstaVacia())
            {
                frente = final = nuevo;
            }
            else
            {
                final.Siguiente = nuevo;
                final = nuevo;
            }
        }

        public Pedido Desencolar()
        {
            if (EstaVacia()) return null;

            Pedido dato = frente.Dato;
            frente = frente.Siguiente;

            if (frente == null)
                final = null;

            return dato;
        }

        // Método adicional para permitir cancelar un pedido específico en la cola
        public Pedido EliminarPorNumero(int numeroPedido)
        {
            if (EstaVacia()) return null;

            if (frente.Dato.Numero == numeroPedido)
            {
                return Desencolar();
            }

            NodoPedido actual = frente;
            while (actual.Siguiente != null && actual.Siguiente.Dato.Numero != numeroPedido)
            {
                actual = actual.Siguiente;
            }

            if (actual.Siguiente != null)
            {
                Pedido eliminado = actual.Siguiente.Dato;
                if (actual.Siguiente == final)
                    final = actual;
                actual.Siguiente = actual.Siguiente.Siguiente;
                return eliminado;
            }

            return null;
        }

        public void Mostrar()
        {
            if (EstaVacia())
            {
                Console.WriteLine("  (Vacía)");
                return;
            }

            NodoPedido actual = frente;
            while (actual != null)
            {
                Console.WriteLine("  " + actual.Dato.ToString());
                actual = actual.Siguiente;
            }
        }
    }
}