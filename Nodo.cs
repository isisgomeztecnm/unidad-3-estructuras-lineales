namespace SistemaOperativoProcesos
{
    public class Nodo
    {
        public Proceso Dato { get; set; }
        public Nodo Siguiente { get; set; }

        public Nodo(Proceso proceso)
        {
            Dato = proceso;
            Siguiente = null;
        }
    }
}
