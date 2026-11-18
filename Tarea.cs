using System;

namespace proyecto6
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        public Tarea(int id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
            Estado = "Pendiente";
        }

        public override string ToString()
        {
            return $"[{Id}] {Descripcion} - Estado: {Estado}";
        }
    }
}