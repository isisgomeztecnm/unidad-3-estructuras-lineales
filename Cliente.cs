using System;

namespace SimuladorBanco
{
    public class Cliente
    {
        public string Turno { get; private set; }
        public string Nombre { get; set; }
        public TipoCliente Tipo { get; set; }
        public DateTime HoraLlegada { get; private set; }
        public DateTime HoraAtencion { get; set; }

        public Cliente(string turno, string nombre, TipoCliente tipo)
        {
            Turno = turno;
            Nombre = nombre;
            Tipo = tipo;
            HoraLlegada = DateTime.Now;
        }

        public int ObtenerNivelPrioridad()
        {
            return (int)Tipo;
        }

        public string ObtenerDescripcionTipo()
        {
            switch (Tipo)
            {
                case TipoCliente.PersonaConPrioridad:
                    return "Persona con prioridad";
                case TipoCliente.AdultoMayor:
                    return "Adulto mayor";
                case TipoCliente.ClientePreferente:
                    return "Cliente preferente";
                case TipoCliente.ClienteNormal:
                    return "Cliente normal";
                default:
                    return "Desconocido";
            }
        }

        public override string ToString()
        {
            return $"[{Turno}] {Nombre} - {ObtenerDescripcionTipo()} (Llegada: {HoraLlegada:HH:mm:ss})";
        }
    }
}