using System;
using System.Collections.Generic;
using System.Linq;

namespace SimuladorBanco
{
    public class SistemaAtencionBanco
    {
        private List<Cliente> _colaEspera;
        private List<Cliente> _clientesAtendidos;
        private int _contadorTurnos;
        private const int MinutosPorCliente = 5;

        public SistemaAtencionBanco()
        {
            _colaEspera = new List<Cliente>();
            _clientesAtendidos = new List<Cliente>();
            _contadorTurnos = 1;
        }

        //  Generar turno
        public Cliente GenerarTurno(string nombre, TipoCliente tipo)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                nombre = "Cliente Anónimo";
            }

            string prefijo;
            switch (tipo)
            {
                case TipoCliente.PersonaConPrioridad:
                    prefijo = "P";
                    break;
                case TipoCliente.AdultoMayor:
                    prefijo = "AM";
                    break;
                case TipoCliente.ClientePreferente:
                    prefijo = "PR";
                    break;
                case TipoCliente.ClienteNormal:
                    prefijo = "N";
                    break;
                default:
                    prefijo = "G";
                    break;
            }

            string codigoTurno = $"{prefijo}-{_contadorTurnos:D3}";
            _contadorTurnos++;

            Cliente nuevoCliente = new Cliente(codigoTurno, nombre.Trim(), tipo);
            _colaEspera.Add(nuevoCliente);
            OrdenarColaPorPrioridad();

            return nuevoCliente;
        }

        private void OrdenarColaPorPrioridad()
        {
            _colaEspera = _colaEspera
                .OrderBy(c => c.ObtenerNivelPrioridad())
                .ThenBy(c => c.HoraLlegada)
                .ToList();
        }

        // Atender cliente
        public Cliente AtenderCliente()
        {
            if (_colaEspera.Count == 0)
            {
                return null;
            }

            Cliente siguiente = _colaEspera[0];
            _colaEspera.RemoveAt(0);

            siguiente.HoraAtencion = DateTime.Now;
            _clientesAtendidos.Add(siguiente);

            return siguiente;
        }

        // Cancelar turno
        public bool CancelarTurno(string turno)
        {
            if (string.IsNullOrWhiteSpace(turno)) return false;

            Cliente cliente = _colaEspera.FirstOrDefault(c => c.Turno.Equals(turno.Trim(), StringComparison.OrdinalIgnoreCase));
            if (cliente != null)
            {
                _colaEspera.Remove(cliente);
                return true;
            }
            return false;
        }

        //  Mostrar clientes esperando
        public void MostrarClientesEsperando()
        {
            Console.WriteLine("\n--- CLIENTES EN ESPERA ---");
            if (_colaEspera.Count == 0)
            {
                Console.WriteLine("No hay clientes esperando en este momento.");
                return;
            }

            for (int i = 0; i < _colaEspera.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_colaEspera[i]}");
            }
        }

        //  Mostrar clientes atendidos
        public void MostrarClientesAtendidos()
        {
            Console.WriteLine("\n--- CLIENTES ATENDIDOS ---");
            if (_clientesAtendidos.Count == 0)
            {
                Console.WriteLine("Aún no se ha atendido a ningún cliente.");
                return;
            }

            foreach (Cliente cliente in _clientesAtendidos)
            {
                Console.WriteLine($"{cliente} | Atendido a las: {cliente.HoraAtencion:HH:mm:ss}");
            }
        }

        // Asignar prioridad
        public bool AsignarPrioridad(string turno, TipoCliente nuevaPrioridad)
        {
            if (string.IsNullOrWhiteSpace(turno)) return false;

            Cliente cliente = _colaEspera.FirstOrDefault(c => c.Turno.Equals(turno.Trim(), StringComparison.OrdinalIgnoreCase));
            if (cliente != null)
            {
                cliente.Tipo = nuevaPrioridad;
                OrdenarColaPorPrioridad();
                return true;
            }
            return false;
        }

        //  Mostrar tiempo estimado
        public void MostrarTiempoEstimado(string turno = null)
        {
            if (string.IsNullOrWhiteSpace(turno))
            {
                int tiempoTotal = _colaEspera.Count * MinutosPorCliente;
                Console.WriteLine($"\nTiempo estimado total para vaciar la fila: {tiempoTotal} minutos.");
                return;
            }

            string turnoLimpio = turno.Trim();
            int indice = _colaEspera.FindIndex(c => c.Turno.Equals(turnoLimpio, StringComparison.OrdinalIgnoreCase));
            if (indice == -1)
            {
                Console.WriteLine($"\nNo se encontró el turno '{turnoLimpio.ToUpper()}' en la fila de espera.");
            }
            else
            {
                int tiempoCliente = indice * MinutosPorCliente;
                string mensajeTiempo = tiempoCliente == 0 ? "Es el siguiente en pasar (0 min)" : $"{tiempoCliente} minutos";
                Console.WriteLine($"\nTiempo estimado de espera para el turno {turnoLimpio.ToUpper()}: {mensajeTiempo}.");
            }
        }

        // Mostrar número de clientes en espera
        public int ObtenerNumeroClientesEnEspera()
        {
            return _colaEspera.Count;
        }
    }
}