namespace proyecto3
{
    public class Nodo
    {
        public Llamada Datos { get; set; }
        public Nodo Siguiente { get; set; }

        public Nodo(Llamada datos)
        {
            Datos = datos;
            Siguiente = null;
        }
    }
}