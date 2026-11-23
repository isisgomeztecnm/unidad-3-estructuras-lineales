using System;

namespace SistemaOperativoProcesos
{
    public class GestorProcesos
    {
        private ListaEnlazadaProcesos tablaMaestra; // Todos los procesos
        private ColaProcesos colaListos;             // Cola de admisión (FIFO)
        private ColaProcesos colaBloqueados;         // Cola de espera E/S
        private ColaCircularCPU planificadorCPU;     // Round-Robin
        private Proceso procesoEnEjecucion;
        private int contadorPID;
        private const int QUANTUM = 3;

        public GestorProcesos()
        {
            tablaMaestra = new ListaEnlazadaProcesos();
            colaListos = new ColaProcesos();
            colaBloqueados = new ColaProcesos();
            planificadorCPU = new ColaCircularCPU(5); // Máximo 5 procesos en CPU
            procesoEnEjecucion = null;
            contadorPID = 100;
        }

        // Crear proceso
        public void CrearProceso(string nombre, int prioridad, int tiempoEjecucion)
        {
            contadorPID++;
            Proceso nuevo = new Proceso(contadorPID, nombre, prioridad, tiempoEjecucion);
            tablaMaestra.Insertar(nuevo);

            // Transición NUEVO -> LISTO
            nuevo.Estado = EstadoProceso.LISTO;
            if (!planificadorCPU.EstaLlena())
            {
                planificadorCPU.Encolar(nuevo);
                Console.WriteLine($"[OK] Proceso '{nombre}' (PID {nuevo.PID}) creado e ingresado directamente al anillo de CPU.");
            }
            else
            {
                colaListos.Encolar(nuevo);
                Console.WriteLine($"[OK] Anillo CPU lleno. Proceso '{nombre}' (PID {nuevo.PID}) encolado en espera (LISTO).");
            }
        }

        // Ejecutar proceso
        public void EjecutarProceso()
        {
            AlimentarCPUDesdeColaListos();

            if (planificadorCPU.EstaVacia())
            {
                Console.WriteLine("[INFO] No hay procesos listos para ejecutar en la CPU.");
                return;
            }

            procesoEnEjecucion = planificadorCPU.Desencolar();
            procesoEnEjecucion.Estado = EstadoProceso.EJECUTANDO;

            Console.WriteLine($"\n>>> EJECUTANDO EN CPU: {procesoEnEjecucion.Nombre} (PID: {procesoEnEjecucion.PID}) | Quantum: {QUANTUM}u");
            procesoEnEjecucion.TiempoEjecucion -= QUANTUM;

            if (procesoEnEjecucion.TiempoEjecucion <= 0)
            {
                procesoEnEjecucion.TiempoEjecucion = 0;
                procesoEnEjecucion.Estado = EstadoProceso.TERMINADO;
                Console.WriteLine($"[FIN] El proceso PID {procesoEnEjecucion.PID} ha TERMINADO su ejecución.");
                procesoEnEjecucion = null;
                AlimentarCPUDesdeColaListos();
            }
            else
            {
                procesoEnEjecucion.Estado = EstadoProceso.LISTO;
                planificadorCPU.Encolar(procesoEnEjecucion);
                Console.WriteLine($"[QUANTUM AGOTADO] PID {procesoEnEjecucion.PID} vuelve a LISTO (Restante: {procesoEnEjecucion.TiempoEjecucion}u).");
                procesoEnEjecucion = null;
            }
        }

        // Bloquear proceso (Simula interrupción por E/S del siguiente en turno)
        public void BloquearProceso()
        {
            if (planificadorCPU.EstaVacia())
            {
                Console.WriteLine("[ERROR] No hay procesos activos en el anillo de CPU para bloquear.");
                return;
            }

            Proceso p = planificadorCPU.Desencolar();
            p.Estado = EstadoProceso.BLOQUEADO;
            colaBloqueados.Encolar(p);
            Console.WriteLine($"[BLOQUEADO] Proceso PID {p.PID} ({p.Nombre}) movido a la cola de dispositivos E/S.");

            AlimentarCPUDesdeColaListos();
        }

        // Desbloquear proceso (Fin operación E/S)
        public void DesbloquearProceso()
        {
            if (colaBloqueados.EstaVacia())
            {
                Console.WriteLine("[INFO] No hay procesos bloqueados.");
                return;
            }

            Proceso p = colaBloqueados.Desencolar();
            p.Estado = EstadoProceso.LISTO;

            if (!planificadorCPU.EstaLlena())
            {
                planificadorCPU.Encolar(p);
                Console.WriteLine($"[DESBLOQUEADO] PID {p.PID} reingresado al anillo de CPU.");
            }
            else
            {
                colaListos.Encolar(p);
                Console.WriteLine($"[DESBLOQUEADO] PID {p.PID} enviado a la cola de espera LISTO.");
            }
        }

        // Eliminar proceso por PID
        public void EliminarProceso(int pid)
        {
            Proceso p = tablaMaestra.BuscarPorPID(pid);
            if (p == null)
            {
                Console.WriteLine($"[ERROR] No existe ningún proceso con PID {pid}.");
                return;
            }

            colaListos.EliminarEspecifico(pid);
            colaBloqueados.EliminarEspecifico(pid);
            EliminarDeColaCircular(pid);
            tablaMaestra.EliminarPorPID(pid);

            Console.WriteLine($"[ELIMINADO] Proceso PID {pid} ({p.Nombre}) purgado del sistema.");
        }

        // Buscar proceso
        public void BuscarProceso(int pid)
        {
            Proceso p = tablaMaestra.BuscarPorPID(pid);
            if (p != null)
                Console.WriteLine($"[ENCONTRADO] {p}");
            else
                Console.WriteLine($"[NO ENCONTRADO] No hay registros para el PID {pid}.");
        }

        //  Mostrar todos los procesos
        public void MostrarProcesos()
        {
            Console.WriteLine("\n=== TABLA MAESTRA DE PROCESOS (Lista Enlazada) ===");
            tablaMaestra.MostrarTodos();
        }

        // Adm, Ver colas de procesos
        public void AdministrarColas()
        {
            Console.WriteLine("\n=== ESTADO DE LAS ESTRUCTURAS DE PLANIFICACIÓN ===");
            planificadorCPU.MostrarBufferCircular();
            Console.WriteLine();
            colaListos.MostrarCola("Cola de Espera Secundaria (FIFO - LISTO)");
            Console.WriteLine();
            colaBloqueados.MostrarCola("Cola de Dispositivos E/S (FIFO - BLOQUEADO)");
        }

        // Métodos auxiliares internos
        private void AlimentarCPUDesdeColaListos()
        {
            while (!planificadorCPU.EstaLlena() && !colaListos.EstaVacia())
            {
                Proceso siguiente = colaListos.Desencolar();
                planificadorCPU.Encolar(siguiente);
            }
        }

        private void EliminarDeColaCircular(int pid)
        {
            int n = planificadorCPU.Cantidad;
            for (int i = 0; i < n; i++)
            {
                Proceso p = planificadorCPU.Desencolar();
                if (p.PID != pid)
                {
                    planificadorCPU.Encolar(p);
                }
            }
        }
    }
}