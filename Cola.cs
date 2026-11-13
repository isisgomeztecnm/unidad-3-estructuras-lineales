using System;

namespace proyecto_1
{
    public class Cola
    {
        private Nodo frente;
        private Nodo final;
        private int contador; 

        public Cola()
        {
            frente = null;
            final = null;
            contador = 0;
        }

       
        public void Encolar(paciente paciente)
        {
            Nodo nuevo = new Nodo(paciente);

            
            if (frente == null || paciente.Prioridad < frente.Datos.Prioridad)
            {
                nuevo.Siguiente = frente;
                frente = nuevo;
                if (final == null)
                {
                    final = nuevo;
                }
            }
            else
            {
              
                Nodo actual = frente;
                while (actual.Siguiente != null && actual.Siguiente.Datos.Prioridad <= paciente.Prioridad)
                {
                    actual = actual.Siguiente;
                }

                nuevo.Siguiente = actual.Siguiente;
                actual.Siguiente = nuevo;

                if (nuevo.Siguiente == null)
                {
                    final = nuevo;
                }
            }
            contador++;
        }

      
        public paciente Desencolar()
        {
            if (frente == null)
                return null;

            paciente paciente = frente.Datos;
            frente = frente.Siguiente;

            if (frente == null)
                final = null;

            contador--;
            return paciente;
        }

        
        public void Mostrar()
        {
            if (frente == null)
            {
                Console.WriteLine("No hay pacientes registrados en esta lista.");
                return;
            }

            Nodo aux = frente;
            while (aux != null)
            {
                Console.WriteLine(aux.Datos);
                aux = aux.Siguiente;
            }
        }

        // Buscar paciente por expediente
        public paciente BuscarPorExpediente(int expediente)
        {
            Nodo aux = frente;
            while (aux != null)
            {
                if (aux.Datos.Expediente == expediente)
                {
                    return aux.Datos;
                }
                aux = aux.Siguiente;
            }
            return null;
        }

        // Eliminar paciente específico por expediente
        public bool EliminarPorExpediente(int expediente)
        {
            if (frente == null)
                return false;

            
            if (frente.Datos.Expediente == expediente)
            {
                frente = frente.Siguiente;
                if (frente == null)
                    final = null;

                contador--;
                return true;
            }

           
            Nodo actual = frente;
            while (actual.Siguiente != null && actual.Siguiente.Datos.Expediente != expediente)
            {
                actual = actual.Siguiente;
            }

            if (actual.Siguiente != null)
            {
                if (actual.Siguiente == final)
                {
                    final = actual;
                }
                actual.Siguiente = actual.Siguiente.Siguiente;
                contador--;
                return true;
            }

            return false;
        }

       
        public bool CambiarPrioridad(int expediente, int nuevaPrioridad)
        {
            paciente p = BuscarPorExpediente(expediente);
            if (p != null)
            {
                
                EliminarPorExpediente(expediente);
                p.Prioridad = nuevaPrioridad;
                Encolar(p);
                return true;
            }
            return false;
        }

        // Consultar cantidad de pacientes en espera
        public int Cantidad()
        {
            return contador;
        }
    }
}