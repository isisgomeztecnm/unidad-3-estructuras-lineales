using System;

namespace proyecto3
{
    public class PilaLlamadas
    {
        private Nodo tope;

        // Atender llamada (Apilar)
        public void Atender(Llamada nuevaLlamada)
        {
            Nodo nuevoNodo = new Nodo(nuevaLlamada);
            nuevoNodo.Siguiente = tope;
            tope = nuevoNodo;
        }

        // Finalizar llamada (Desapilar)
        public Llamada Finalizar()
        {
            if (tope == null) return null;

            Llamada llamada = tope.Datos;
            tope = tope.Siguiente;
            return llamada;
        }

        // Recuperar última llamada sin eliminarla
        public Llamada RecuperarUltima()
        {
            return tope?.Datos;
        }

        // Mostrar todas las llamadas activas en la pila
        public void MostrarActivas()
        {
            if (tope == null)
            {
                Console.WriteLine("No hay llamadas activas.");
                return;
            }

            Nodo actual = tope;
            while (actual != null)
            {
                Console.WriteLine($"  -> {actual.Datos}");
                actual = actual.Siguiente;
            }
        }

        // Buscar si existe una llamada por su identificador
        public Llamada Buscar(string identificador)
        {
            Nodo actual = tope;
            while (actual != null)
            {
                if (actual.Datos.Identificador.Equals(identificador, StringComparison.OrdinalIgnoreCase))
                {
                    return actual.Datos;
                }
                actual = actual.Siguiente;
            }
            return null;
        }

        // Eliminar una llamada específica
        public bool Eliminar(string identificador)
        {
            if (tope == null) return false;

            // Caso 1: La llamada está en el tope
            if (tope.Datos.Identificador.Equals(identificador, StringComparison.OrdinalIgnoreCase))
            {
                tope = tope.Siguiente;
                return true;
            }

            // Caso 2: La llamada está en una posición intermedia o al final
            Nodo actual = tope;
            while (actual.Siguiente != null)
            {
                if (actual.Siguiente.Datos.Identificador.Equals(identificador, StringComparison.OrdinalIgnoreCase))
                {
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    return true;
                }
                actual = actual.Siguiente;
            }

            return false;
        }
    }
}