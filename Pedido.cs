using System;
using System.Collections.Generic;

namespace SistemaGestionPedidos
{
    // Estructura auxiliar para saber qué producto y cuánta cantidad se pidió
    public class DetallePedido
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => Producto.Precio * Cantidad;
    }

    public class Pedido
    {
        public int Numero { get; set; }
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetallePedido> Productos { get; set; }
        public decimal Total { get; private set; }
        // Prioridad: 1 = Urgente (Alta), 2 = Normal
        public int Prioridad { get; set; }
        public string Estado { get; set; } // "Pendiente", "Procesado", "Cancelado"

        public Pedido(int numero, string cliente, int prioridad)
        {
            Numero = numero;
            Cliente = cliente;
            Fecha = DateTime.Now;
            Productos = new List<DetallePedido>();
            Prioridad = prioridad;
            Estado = "Pendiente";
            Total = 0;
        }

        public void AgregarProducto(Producto producto, int cantidad)
        {
            Productos.Add(new DetallePedido { Producto = producto, Cantidad = cantidad });
            CalcularTotal();
        }

        public void CalcularTotal()
        {
            Total = 0;
            foreach (var item in Productos)
            {
                Total += item.Subtotal;
            }
        }

        public override string ToString()
        {
            string tipoPrioridad = Prioridad == 1 ? "URGENTE" : "Normal";
            return $"Pedido #{Numero} | Cliente: {Cliente} | Fecha: {Fecha:g} | Prioridad: {tipoPrioridad} | Total: ${Total:F2} | Estado: {Estado}";
        }
    }
}