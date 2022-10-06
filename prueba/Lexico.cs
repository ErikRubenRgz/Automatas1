using System;
using System.IO;

/*
1.	Identificador: 
2.	Números:
3.	Asignación: =
4.	Inicialización: := 
5.	Fin de sentencia: ;
6.	Operador lógicos: &&, ||, !
7.	Operador relacional: >, >=, <, <=, ==, =, !=, <>
8.	Operador Termino: +, -
9.	Operador de Factor: *, /, %
10.	Incremento Termino: ++, --, +=, -=
11.	Incremento Factor: *=, /=, %=
12.	Carácter:
13. Operador Ternario: ?

Requerimientos para evaluar la Unidad 2

3. Asignación                                               20%
6. Operador Logico ( & = Caracter, && = Operador Logico)    30%
7. Operador relacional: >, >=, <, <=, ==, =, !=, <>         50%

*/

namespace Prueba
{
    public class Lexico : Token
    {
        StreamReader archivo;
        StreamWriter log;
        
        public Lexico()
        {
            archivo = new StreamReader("C://Users//erixk//Documents//Automatas1//prueba//Entrada.txt");
            log = new StreamWriter("C://Users//erixk//Documents//Automatas1//prueba//Salida.txt");
            log.AutoFlush = true;
        }

        public Lexico(string filename)
        {
            archivo = new StreamReader(filename);
            log = new StreamWriter("C://Users//erixk//Documents//Automatas1//prueba//Salida.txt");
            log.AutoFlush = true;
        }

        ~Lexico()
        {
            Console.WriteLine("Destructor");
        }

        public void nextToken()
        {
            string  buffer = "";
            char c;

            while (char.IsWhiteSpace(c = (char) archivo.Read()))
            {

            }
            buffer += c;
            
            if (char.IsLetter(c))
            {
                SETClasificacion(Tipos.Identificador);
                while (char.IsLetterOrDigit(c = (char) archivo.Peek()))
                {
                    buffer += c;
                    archivo.Read();
                }
            }
            else if (char.IsDigit(c))
            {
                SETClasificacion(Tipos.Numero);
                while (char.IsDigit(c = (char) archivo.Peek()))
                {
                    buffer += c;
                    archivo.Read();
                }
                if (c == '.')
                {
                    buffer += c;
                    archivo.Read();
                    if (char.IsDigit(c = (char) archivo.Peek()))
                    {
                        while (char.IsDigit(c = (char) archivo.Peek()))
                        {
                            buffer += c;
                            archivo.Read();
                        }
                    }
                    else
                    {
                        throw new Error("Error Lexico: Se espera un digito (" + buffer + ")",log);
                    }
                }
                if (char.ToLower(c) == 'e')
                {
                    buffer += c;
                    archivo.Read();
                    c = (char) archivo.Peek();
                    if (c == '+' || c == '-')
                    {
                        buffer += c;
                        archivo.Read();
                    }
                    if (char.IsDigit(c = (char) archivo.Peek()))
                    {
                        while (char.IsDigit(c = (char) archivo.Peek()))
                        {
                            buffer += c;
                            archivo.Read();
                        }
                    }
                    else
                    {
                        throw new Error("Error Lexico: Se espera un digito (" + buffer + ")",log);
                    }
                }
            }
            else if (c == '=')
            {
                SETClasificacion(Tipos.Asignacion);
                if ((c = (char) archivo.Peek()) == '=')
                {
                    buffer += c;
                    archivo.Read();
                    SETClasificacion(Tipos.OperadorRelacional);
                }
            }
            else if (c == ';')
            {
                SETClasificacion(Tipos.FinSentencia);
            }
            else if (c == '?')
            {
                SETClasificacion(Tipos.Ternario);
            }
            else if (c == '+')
            {
                SETClasificacion(Tipos.OperadorTermino);
                if ((c = (char) archivo.Peek()) == '+' || c == '=')
                {
                    buffer += c;
                    archivo.Read();
                    SETClasificacion(Tipos.IncrementoTermino);
                }
            }
            else if (c == '-')
            {
                SETClasificacion(Tipos.OperadorTermino);
                if ((c = (char) archivo.Peek()) == '-' || c == '=')
                {
                    buffer += c;
                    archivo.Read();
                    SETClasificacion(Tipos.IncrementoTermino);
                }
            }
            else if (c == '*' || c == '/' || c == '%')
            {
                SETClasificacion(Tipos.OperadorFactor);
                if ((c = (char) archivo.Peek()) == '=')
                {
                    buffer += c;
                    archivo.Read();
                    SETClasificacion(Tipos.IncrementoFactor);
                }
            }
            else if(c==':'){
                SETClasificacion(Tipos.Caracter);
                if ((c = (char) archivo.Peek()) == ':'|| c== '=')
                {
                    buffer += c;
                    archivo.Read();
                    SETClasificacion(Tipos.Inicialización);
                }
                
            }
            else
            {
                SETClasificacion(Tipos.Caracter);
            }
            SETContenido(buffer);
            log.WriteLine(GETContenido() + " = " + GETClasificacion());
            
        }	

        public void cerrarArchivos()
        {
            log.Close();
            archivo.Close();
        }

        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}