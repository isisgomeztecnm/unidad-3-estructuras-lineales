namespace proyecto_1
{
    public class paciente
    {
        
        public int Expediente { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int Prioridad { get; set; }

        
        public paciente(int expediente, string nombre, int edad, int prioridad)
        {
            Expediente = expediente;
            Nombre = nombre;
            Edad = edad;
            Prioridad = prioridad;
        }

        
        public override string ToString()
        {
            return $"Expediente:{Expediente}|" +
                   $"Nombre:{Nombre}|" +
                   $"Edad:{Edad}|" +
                   $"Prioridad:{Prioridad}";
        }
    }
}






