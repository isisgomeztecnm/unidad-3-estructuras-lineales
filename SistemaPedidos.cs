using System;

namespace SistemaGestionPedidos
{
    public class SistemaPedidos
    {
        private ListaEnlazadaProductos inventario;
        private ColaPedidos pedidosPendientes;
        private ColaPrioridadPedidos pedidosUrgentes;
        private PilaHistorial historial;
        private int contadorPedidos;

        public SistemaPedidos()
        {
            inventario = new ListaEnlazadaProductos();
            pedidosPendientes = new ColaPedidos();
            pedidosUrgentes = new ColaPrioridadPedidos();
            historial = new PilaHistorial();
            contadorPedidos = 100; // Inicia numeración de pedidos en 101
        }

        // --- GESTIÓN DE PRODUCTOS ---

        public void RegistrarProducto(int id, string nombre, decimal precio, int existencias, string categoria)
        {
            if (inventario.BuscarPorId(id) != null)
            {
                Console.WriteLine("Error: Ya existe un producto con ese ID.");
                return;
            }

            Producto nuevo = new Producto(id, nombre, precio, existencias, categoria);
            inventario.Insertar(nuevo);
            historial.Push($"Producto registrado: {nombre} (ID: {id}, Stock: {existencias})");
            Console.WriteLine("Producto registrado correctamente.");
        }

        public Producto BuscarProducto(int id)
        {
            Producto p = inventario.BuscarPorId(id);
            if (p != null)
                historial.Push($"Búsqueda de producto ID: {id} ({p.Nombre})");
            return p;
        }

        public void EliminarProducto(int id)
        {
            Producto p = inventario.BuscarPorId(id);
            if (p == null)
            {
                Console.WriteLine("Producto no encontrado.");
                return;
            }

            if (inventario.Eliminar(id))
            {
                historial.Push($"Producto eliminado: {p.Nombre} (ID: {id})");
                Console.WriteLine("Producto eliminado del catálogo.");
            }
        }

        public void ListarProductos()
        {
            Console.WriteLine("\n--- CATÁLOGO DE PRODUCTOS (Lista Enlazada) ---");
            inventario.MostrarTodos();
        }

        // --- GESTIÓN DE PEDIDOS ---

        public Pedido CrearPedido(string cliente, int prioridad)
        {
            contadorPedidos++;
            return new Pedido(contadorPedidos, cliente, prioridad);
        }

        public bool AgregarProductoAPedido(Pedido pedido, int idProducto, int cantidad)
        {
            Producto prod = inventario.BuscarPorId(idProducto);
            if (prod == null)
            {
                Console.WriteLine("El producto no existe.");
                return false;
            }
            if (cantidad <= 0 || prod.Existencias < cantidad)
            {
                Console.WriteLine($"Stock insuficiente. Disponibles: {prod.Existencias}");
                return false;
            }

            // Se aparta el stock inmediatamente al agregar al pedido
            prod.Existencias -= cantidad;
            pedido.AgregarProducto(prod, cantidad);
            return true;
        }

        public void ConfirmarRegistroPedido(Pedido pedido)
        {
            if (pedido.Productos.Count == 0)
            {
                Console.WriteLine("No se puede registrar un pedido vacío.");
                return;
            }

            if (pedido.Prioridad == 1)
            {
                pedidosUrgentes.Encolar(pedido);
                historial.Push($"Pedido URGENTE #{pedido.Numero} registrado para {pedido.Cliente} (${pedido.Total:F2})");
                Console.WriteLine($"Pedido #{pedido.Numero} enviado a la COLA DE PRIORIDAD.");
            }
            else
            {
                pedidosPendientes.Encolar(pedido);
                historial.Push($"Pedido Normal #{pedido.Numero} registrado para {pedido.Cliente} (${pedido.Total:F2})");
                Console.WriteLine($"Pedido #{pedido.Numero} enviado a la COLA DE PENDIENTES.");
            }
        }

        public void CancelarPedido(int numeroPedido)
        {
            // Buscar primero en urgentes, luego en pendientes
            Pedido cancelado = pedidosUrgentes.EliminarPorNumero(numeroPedido)
                            ?? pedidosPendientes.EliminarPorNumero(numeroPedido);

            if (cancelado == null)
            {
                Console.WriteLine($"No se encontró ningún pedido pendiente con el número #{numeroPedido}.");
                return;
            }

            // Devolver las existencias al inventario
            foreach (var detalle in cancelado.Productos)
            {
                detalle.Producto.Existencias += detalle.Cantidad;
            }

            cancelado.Estado = "Cancelado";
            historial.Push($"Pedido #{numeroPedido} CANCELADO. Stock restaurado.");
            Console.WriteLine($"Pedido #{numeroPedido} cancelado exitosamente y existencias devueltas.");
        }

        public void ProcesarSiguientePedido()
        {
            Pedido aProcesar = null;

            // Regla de negocio: Siempre se atienden primero los pedidos urgentes
            if (!pedidosUrgentes.EstaVacia())
            {
                aProcesar = pedidosUrgentes.Desencolar();
            }
            else if (!pedidosPendientes.EstaVacia())
            {
                aProcesar = pedidosPendientes.Desencolar();
            }

            if (aProcesar == null)
            {
                Console.WriteLine("No hay pedidos pendientes por procesar.");
                return;
            }

            aProcesar.Estado = "Procesado";
            historial.Push($"Pedido #{aProcesar.Numero} PROCESADO (Cliente: {aProcesar.Cliente}, Total: ${aProcesar.Total:F2})");

            Console.WriteLine("\n>>> PEDIDO PROCESADO CON ÉXITO <<<");
            Console.WriteLine(aProcesar.ToString());
            Console.WriteLine("Detalle de artículos:");
            foreach (var item in aProcesar.Productos)
            {
                Console.WriteLine($" - {item.Cantidad}x {item.Producto.Nombre} (${item.Subtotal:F2})");
            }
        }

        // --- VISUALIZACIÓN DE ESTRUCTURAS ---

        public void MostrarEstadoColas()
        {
            Console.WriteLine("\n--- COLA DE PRIORIDAD (Pedidos Urgentes) ---");
            pedidosUrgentes.Mostrar();

            Console.WriteLine("\n--- COLA ESTÁNDAR (Pedidos Pendientes) ---");
            pedidosPendientes.Mostrar();
        }

        public void MostrarHistorial()
        {
            Console.WriteLine("\n--- HISTORIAL DE OPERACIONES (Pila - Más reciente arriba) ---");
            historial.MostrarHistorial();
        }
    }
}