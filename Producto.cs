using System;

namespace SistemaGestionPedidos
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Existencias { get; set; }
        public string Categoria { get; set; }

        public Producto(int id, string nombre, decimal precio, int existencias, string categoria)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            Existencias = existencias;
            Categoria = categoria;
        }

        public override string ToString()
        {
            return $"[ID: {Id}] {Nombre} | Cat: {Categoria} | Precio: ${Precio:F2} | Stock: {Existencias}";
        }
    }
}