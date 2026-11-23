using System;

namespace SistemaOperativoProcesos
{
    public enum EstadoProceso
    {
        NUEVO,
        LISTO,
        EJECUTANDO,
        BLOQUEADO,
        TERMINADO
    }

    public class Proceso
    {
        public int PID { get; private set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; } // 1 (Alta) a 5 (Baja)
        public EstadoProceso Estado { get; set; }
        public int TiempoEjecucion { get; set; } // Tiempo restante en milisegundos o ciclos

        public Proceso(int pid, string nombre, int prioridad, int tiempoEjecucion)
        {
            PID = pid;
            Nombre = nombre;
            Prioridad = prioridad;
            TiempoEjecucion = tiempoEjecucion;
            Estado = EstadoProceso.NUEVO;
        }

        public override string ToString()
        {
            return $"[PID: {PID,-3} | {Nombre,-15} | Prio: {Prioridad} | Estado: {Estado,-10} | T. Restante: {TiempoEjecucion}u]";
        }
    }
}