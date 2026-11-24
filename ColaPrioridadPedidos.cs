namespace SistemaGestionPedidos
{
    public class ColaPrioridadPedidos : ColaPedidos
    {
        public override void Encolar(Pedido pedido)
        {
            NodoPedido nuevo = new NodoPedido(pedido);

            // Si está vacía o el nuevo tiene mayor prioridad (número menor) que el frente
            if (EstaVacia() || pedido.Prioridad < frente.Dato.Prioridad)
            {
                nuevo.Siguiente = frente;
                frente = nuevo;
                if (final == null) final = nuevo;
            }
            else
            {
                NodoPedido actual = frente;
                while (actual.Siguiente != null && actual.Siguiente.Dato.Prioridad <= pedido.Prioridad)
                {
                    actual = actual.Siguiente;
                }
                nuevo.Siguiente = actual.Siguiente;
                actual.Siguiente = nuevo;

                if (nuevo.Siguiente == null)
                    final = nuevo;
            }
        }
    }
}