using System;

namespace SistemaGestionPedidos
{
    class Program
    {
        static void Main(string[] args)
        {
            SistemaPedidos sistema = new SistemaPedidos();

            // Datos iniciales de prueba
            sistema.RegistrarProducto(1, "Laptop Dell XPS", 25000.00m, 5, "Cómputo");
            sistema.RegistrarProducto(2, "Mouse Inalámbrico", 450.50m, 20, "Accesorios");
            sistema.RegistrarProducto(3, "Teclado Mecánico", 1200.00m, 10, "Accesorios");
            sistema.RegistrarProducto(4, "Monitor 27 Pulgadas", 4800.00m, 8, "Monitores");

            int opcion = 0;
            do
            {
                Console.WriteLine("\n==========================================");
                Console.WriteLine("   SISTEMA DE GESTIÓN DE PEDIDOS EN LÍNEA ");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Registrar producto");
                Console.WriteLine("2. Buscar producto");
                Console.WriteLine("3. Eliminar producto");
                Console.WriteLine("4. Ver catálogo de productos");
                Console.WriteLine("5. Registrar nuevo pedido");
                Console.WriteLine("6. Cancelar pedido");
                Console.WriteLine("7. Procesar siguiente pedido");
                Console.WriteLine("8. Ver colas de pedidos (Urgentes y Pendientes)");
                Console.WriteLine("9. Ver historial de operaciones (Pila)");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Entrada inválida.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        Console.Write("ID del producto: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Nombre: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Precio: ");
                        decimal precio = decimal.Parse(Console.ReadLine());
                        Console.Write("Existencias: ");
                        int stock = int.Parse(Console.ReadLine());
                        Console.Write("Categoría: ");
                        string cat = Console.ReadLine();
                        sistema.RegistrarProducto(id, nombre, precio, stock, cat);
                        break;

                    case 2:
                        Console.Write("Ingrese ID del producto a buscar: ");
                        int idBuscar = int.Parse(Console.ReadLine());
                        Producto encontrado = sistema.BuscarProducto(idBuscar);
                        Console.WriteLine(encontrado != null ? encontrado.ToString() : "Producto no encontrado.");
                        break;

                    case 3:
                        Console.Write("Ingrese ID del producto a eliminar: ");
                        int idEliminar = int.Parse(Console.ReadLine());
                        sistema.EliminarProducto(idEliminar);
                        break;

                    case 4:
                        sistema.ListarProductos();
                        break;

                    case 5:
                        Console.Write("Nombre del cliente: ");
                        string cliente = Console.ReadLine();
                        Console.Write("Prioridad (1 = Urgente, 2 = Normal): ");
                        int prioridad = int.Parse(Console.ReadLine());
                        if (prioridad != 1 && prioridad != 2) prioridad = 2;

                        Pedido nuevoPedido = sistema.CrearPedido(cliente, prioridad);
                        string agregarMas;
                        do
                        {
                            sistema.ListarProductos();
                            Console.Write("ID del producto a agregar: ");
                            int idProd = int.Parse(Console.ReadLine());
                            Console.Write("Cantidad: ");
                            int cant = int.Parse(Console.ReadLine());

                            if (sistema.AgregarProductoAPedido(nuevoPedido, idProd, cant))
                                Console.WriteLine("Producto añadido al pedido.");

                            Console.Write("¿Desea agregar otro producto al pedido? (s/n): ");
                            agregarMas = Console.ReadLine();
                        } while (agregarMas.Equals("s", StringComparison.OrdinalIgnoreCase));

                        sistema.ConfirmarRegistroPedido(nuevoPedido);
                        break;

                    case 6:
                        Console.Write("Ingrese el número de pedido a cancelar: ");
                        int numCancelar = int.Parse(Console.ReadLine());
                        sistema.CancelarPedido(numCancelar);
                        break;

                    case 7:
                        sistema.ProcesarSiguientePedido();
                        break;

                    case 8:
                        sistema.MostrarEstadoColas();
                        break;

                    case 9:
                        sistema.MostrarHistorial();
                        break;

                    case 0:
                        Console.WriteLine("Saliendo del sistema...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } while (opcion != 0);
        }
    }
}
