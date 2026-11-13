namespace proyecto_1
{
    public class Nodo
    {
        
        public paciente Datos { get; set; }
        public Nodo Siguiente { get; set; }

       
        public Nodo(paciente paciente)
        {
            Datos = paciente;
            Siguiente = null;
        }
    }
}