
using System;
using System.Globalization;

namespace Proyecto4
{
    internal class Program
    {
        static int Prioridad(char operador)
        {
            if (operador == '+' || operador == '-')
                return 1;
            if (operador == '*' || operador == '/')
                return 2;
            if (operador == '^')
                return 3;
            return 0;
        }

        static bool EsOperador(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/' || c == '^';
        }

    
        static bool ValidarExpresion(string expresion, out string mensajeError)
        {
            mensajeError = "";
            if (string.IsNullOrWhiteSpace(expresion))
            {
                mensajeError = "La expresión está vacía.";
                return false;
            }

            Pila pilaParentesis = new Pila();
            string limpia = expresion.Replace(" ", "");
            char anterior = '\0';

            for (int i = 0; i < limpia.Length; i++)
            {
                char actual = limpia[i];

               
                if (!char.IsDigit(actual) && actual != '.' && !EsOperador(actual) && actual != '(' && actual != ')')
                {
                    mensajeError = $"Carácter no válido encontrado: '{actual}'.";
                    return false;
                }

               
                if (actual == '(')
                {
                    if (char.IsDigit(anterior) || anterior == ')')
                    {
                        mensajeError = "Falta un operador antes de '('.";
                        return false;
                    }
                    pilaParentesis.Apilar(actual);
                }
                else if (actual == ')')
                {
                    if (pilaParentesis.Vacia())
                    {
                        mensajeError = "Paréntesis de cierre ')' sin apertura.";
                        return false;
                    }
                    if (anterior == '(' || EsOperador(anterior))
                    {
                        mensajeError = "Expresión incompleta antes de ')'.";
                        return false;
                    }
                    pilaParentesis.Desapilar();
                }
               
                else if (EsOperador(actual))
                {
                    if (i == 0 || i == limpia.Length - 1)
                    {
                        mensajeError = $"El operador '{actual}' no puede ir al inicio ni al final.";
                        return false;
                    }
                    if (EsOperador(anterior) || anterior == '(')
                    {
                        mensajeError = $"Operadores consecutivos o inválidos: '{anterior}{actual}'.";
                        return false;
                    }
                }

                anterior = actual;
            }

            if (!pilaParentesis.Vacia())
            {
                mensajeError = "Falta cerrar uno o más paréntesis '('.";
                return false;
            }

            return true;
        }

        
        static string InfijaAPostfija(string expresion)
        {
            Pila pila = new Pila();
            string salida = "";

            for (int i = 0; i < expresion.Length; i++)
            {
                char caracter = expresion[i];

                if (char.IsWhiteSpace(caracter))
                    continue;

                // Leer números completos (enteros o con punto decimal)
                if (char.IsDigit(caracter) || caracter == '.')
                {
                    while (i < expresion.Length && (char.IsDigit(expresion[i]) || expresion[i] == '.'))
                    {
                        salida += expresion[i];
                        i++;
                    }
                    salida += " ";
                    i--;
                }
                else if (caracter == '(')
                {
                    pila.Apilar(caracter);
                }
                else if (caracter == ')')
                {
                    while (!pila.Vacia() && pila.Tope() != '(')
                    {
                        salida += pila.Desapilar() + " ";
                    }
                    if (!pila.Vacia() && pila.Tope() == '(')
                    {
                        pila.Desapilar();
                    }
                }
                else if (EsOperador(caracter))
                {
                    while (!pila.Vacia() && pila.Tope() != '(' &&
                          (Prioridad(pila.Tope()) > Prioridad(caracter) ||
                          (Prioridad(pila.Tope()) == Prioridad(caracter) && caracter != '^')))
                    {
                        salida += pila.Desapilar() + " ";
                    }
                    pila.Apilar(caracter);
                }
            }

            while (!pila.Vacia())
            {
                salida += pila.Desapilar() + " ";
            }

            return salida.Trim();
        }

     
        static double EvaluarPostfija(string postfija)
        {
            PilaNumeros pila = new PilaNumeros();
            
            string[] tokens = postfija.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in tokens)
            {
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double numero))
                {
                    pila.Apilar(numero);
                }
                else if (token.Length == 1 && EsOperador(token[0]))
                {
                    double b = pila.Desapilar(); // Segundo operando
                    double a = pila.Desapilar(); // Primer operando
                    double resultado = 0;

                    switch (token[0])
                    {
                        case '+':
                            resultado = a + b;
                            break;
                        case '-':
                            resultado = a - b;
                            break;
                        case '*':
                            resultado = a * b;
                            break;
                        case '/':
                            if (b == 0)
                                throw new DivideByZeroException("Error matemático: División entre cero.");
                            resultado = a / b;
                            break;
                        case '^':
                            if (a == 0 && b < 0)
                                throw new ArithmeticException("Error matemático: Cero elevado a exponente negativo (indefinido).");
                            if (a < 0 && b % 1 != 0)
                                throw new ArithmeticException("Error matemático: Raíz par de número negativo (número imaginario).");
                            resultado = Math.Pow(a, b);
                            break;
                    }

                    if (double.IsInfinity(resultado) || double.IsNaN(resultado))
                        throw new ArithmeticException("Error matemático: Resultado fuera de rango o indefinido.");

                    pila.Apilar(resultado);
                }
                else
                {
                    throw new FormatException($"Número mal formado: '{token}'.");
                }
            }

            double final = pila.Desapilar();
            if (!pila.Vacia())
                throw new InvalidOperationException("Error de sintaxis: sobran operandos.");

            return final;
        }

        static void Main()
        {
            Console.WriteLine("=== CALCULADORA ===");
            Console.Write("Expresión: ");
            string expresion = Console.ReadLine();

            if (!ValidarExpresion(expresion, out string error))
            {
                Console.WriteLine($"Error de validación: {error}");
            }
            else
            {
                try
                {
                    string postfija = InfijaAPostfija(expresion);
                    Console.WriteLine($"Postfija: {postfija}");

                    double resultado = EvaluarPostfija(postfija);
                    Console.WriteLine($"Resultado: {resultado}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.ReadKey();
        }
    }
}




