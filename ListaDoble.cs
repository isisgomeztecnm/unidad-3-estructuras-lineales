using System;

namespace proyecto6
{
    public class ListaDoble
    {
        private Nodo inicio;
        private Nodo fin;

        private class Nodo
        {
            public Tarea Datos;
            public Nodo Anterior;
            public Nodo Siguiente;

            public Nodo(Tarea tarea)
            {
                Datos = tarea;
            }
        }

        //  Agregar tarea
        public bool Agregar(Tarea tarea)
        {
            if (Buscar(tarea.Id) != null)
            {
                return false; // Evita IDs duplicados
            }

            Nodo nuevo = new Nodo(tarea);
            if (inicio == null)
            {
                inicio = fin = nuevo;
            }
            else
            {
                fin.Siguiente = nuevo;
                nuevo.Anterior = fin;
                fin = nuevo;
            }
            return true;
        }

        //  Eliminar tarea
        public bool Eliminar(int id)
        {
            Nodo actual = inicio;
            while (actual != null)
            {
                if (actual.Datos.Id == id)
                {
                    if (actual == inicio && actual == fin)
                    {
                        inicio = fin = null;
                    }
                    else if (actual == inicio)
                    {
                        inicio = inicio.Siguiente;
                        inicio.Anterior = null;
                    }
                    else if (actual == fin)
                    {
                        fin = fin.Anterior;
                        fin.Siguiente = null;
                    }
                    else
                    {
                        actual.Anterior.Siguiente = actual.Siguiente;
                        actual.Siguiente.Anterior = actual.Anterior;
                    }
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }

        // Buscar tarea
        public Tarea Buscar(int id)
        {
            Nodo actual = inicio;
            while (actual != null)
            {
                if (actual.Datos.Id == id)
                {
                    return actual.Datos;
                }
                actual = actual.Siguiente;
            }
            return null;
        }

        // Modificar tarea
        public bool Modificar(int id, string nuevaDescripcion)
        {
            Tarea tarea = Buscar(id);
            if (tarea != null)
            {
                tarea.Descripcion = nuevaDescripcion;
                return true;
            }
            return false;
        }

        // Marcar como terminada
        public bool MarcarComoTerminada(int id)
        {
            Tarea tarea = Buscar(id);
            if (tarea != null)
            {
                tarea.Estado = "Terminada";
                return true;
            }
            return false;
        }

        // Mostrar tareas por estado ("Pendiente" o "Terminada")
        public void MostrarPorEstado(string estado)
        {
            Nodo actual = inicio;
            bool hayResultados = false;

            while (actual != null)
            {
                if (actual.Datos.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(actual.Datos);
                    hayResultados = true;
                }
                actual = actual.Siguiente;
            }

            if (!hayResultados)
            {
                Console.WriteLine($"No hay tareas con estado '{estado}'.");
            }
        }

        // Ordenar tareas por ID (ascendente)
        public void OrdenarPorId()
        {
            if (inicio == null || inicio.Siguiente == null) return;

            bool huboIntercambio;
            do
            {
                huboIntercambio = false;
                Nodo actual = inicio;

                while (actual.Siguiente != null)
                {
                    if (actual.Datos.Id > actual.Siguiente.Datos.Id)
                    {
                        Tarea temp = actual.Datos;
                        actual.Datos = actual.Siguiente.Datos;
                        actual.Siguiente.Datos = temp;
                        huboIntercambio = true;
                    }
                    actual = actual.Siguiente;
                }
            } while (huboIntercambio);
        }

        // Recorrer lista hacia adelante
        public void RecorrerAdelante()
        {
            if (inicio == null)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            Nodo actual = inicio;
            while (actual != null)
            {
                Console.WriteLine(actual.Datos);
                actual = actual.Siguiente;
            }
        }

        //  Recorrer lista hacia atrás
        public void RecorrerAtras()
        {
            if (fin == null)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            Nodo actual = fin;
            while (actual != null)
            {
                Console.WriteLine(actual.Datos);
                actual = actual.Anterior;
            }
        }
    }
}
