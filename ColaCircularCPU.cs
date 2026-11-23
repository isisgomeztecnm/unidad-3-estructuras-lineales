using System;

namespace SistemaOperativoProcesos
{
    public class ColaCircularCPU
    {
        private Proceso[] buffer;
        private int frente;
        private int final;
        private int capacidad;
        public int Cantidad { get; private set; }

        public ColaCircularCPU(int capacidadMaxima = 5)
        {
            capacidad = capacidadMaxima;
            buffer = new Proceso[capacidad];
            frente = -1;
            final = -1;
            Cantidad = 0;
        }

        public bool EstaVacia() => Cantidad == 0;
        public bool EstaLlena() => Cantidad == capacidad;

        public bool Encolar(Proceso proceso)
        {
            if (EstaLlena()) return false;

            if (EstaVacia())
            {
                frente = 0;
                final = 0;
            }
            else
            {
                final = (final + 1) % capacidad;
            }

            buffer[final] = proceso;
            Cantidad++;
            return true;
        }

        public Proceso Desencolar()
        {
            if (EstaVacia()) return null;

            Proceso proceso = buffer[frente];
            buffer[frente] = null;

            if (frente == final)
            {
                frente = -1;
                final = -1;
            }
            else
            {
                frente = (frente + 1) % capacidad;
            }

            Cantidad--;
            return proceso;
        }

        public void Rotar()
        {
            if (Cantidad > 1)
            {
                Proceso p = Desencolar();
                Encolar(p);
            }
        }

        public Proceso VerFrente() => EstaVacia() ? null : buffer[frente];

        public void MostrarBufferCircular()
        {
            Console.WriteLine($"--- Cola Circular CPU (Capacidad: {Cantidad}/{capacidad}) ---");
            if (EstaVacia())
            {
                Console.WriteLine("  [CPU inactiva - Sin procesos en anillo]");
                return;
            }

            int i = frente;
            int recorridos = 0;
            while (recorridos < Cantidad)
            {
                string marcador = (recorridos == 0) ? "[TURNO ACTUAL] " : "               ";
                Console.WriteLine($"  {marcador}{buffer[i]}");
                i = (i + 1) % capacidad;
                recorridos++;
            }
        }
    }
}