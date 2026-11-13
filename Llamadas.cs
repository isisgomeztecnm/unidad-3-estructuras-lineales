using System;

namespace proyecto3
{
    public class Llamada
    {
        public string Identificador { get; set; }
        public DateTime HoraEntrada { get; set; }

        public Llamada(string identificador)
        {
            Identificador = identificador;
            HoraEntrada = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{HoraEntrada:HH:mm:ss}] Contacto: {Identificador}";
        }
    }
}