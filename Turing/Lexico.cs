using System;
using System.IO;

namespace TURING
{
    public class Lexico : Token
    {
        StreamReader archivo;
        protected StreamWriter log;

        const int F = -1;
        const int E = -2;
        protected int Linea;
        protected int NumCaracter;
        string datetime = DateTime.Now.ToString("hh:mm:ss tt");
        string Date = DateTime.Now.ToString("dd/MM/yyyy");
        int[,] TRAND = {

      //WS,EOF,Let, Dig, ., E,  +,  -,  =,  :,  ;,  &,  |,  !,  >,  <,  *,  %,  /,   ?,  "",  ',  La, {,   }, (,  ), >>, <<
       { 0,  F,  1,  2,  29, 1,  20, 21, 8,  10, 12, 13, 15, 16, 17, 19, 23, 23, 23, 25, 26, 27, 29, 30, 31, 32, 33, 34, 35}, // Estado 0
       { F,  F,  1,  1,  F,  1,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 1
       { F,  F,  F,  2,  3,  5,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 2
       { E,  E,  E,  4,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E ,  E,  E}, // Estado 3
       { F,  F,  F,  4,  F,  5,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 4
       { E,  E,  E,  7,  E,  E,  6,  6,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E ,  E,  E}, // Estado 5
       { E,  E,  E,  7,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E,  E ,  E,  E}, // Estado 6
       { F,  F,  F,  7,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 7
       { F,  F,  F,  F,  F,  F,  F,  F,  9,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 8
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 9
       { F,  F,  F,  F,  F,  F,  F,  F,  10, F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 10
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 11
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 12
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  14, F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 13
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 14
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  14, F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 15
       { F,  F,  F,  F,  F,  F,  F,  F,  18, F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 16
       { F,  F,  F,  F,  F,  F,  F,  F,  18, F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 17
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 18
       { F,  F,  F,  F,  F,  F,  F,  F,  18, F,  F,  F,  F,  F,  18, F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 19
       { F,  F,  F,  F,  F,  F,  22, F,  22, F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 20
       { F,  F,  F,  F,  F,  F,  F,  22, 22, F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 21
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 22
       { F,  F,  F,  F,  F,  F,  F,  F,  24, F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 23
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 24
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 25
       { 26, E,  26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 28, 26, 26, 26, 26, 26, 26, 26, 26}, // Estado 26
       { 27, E,  27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 28, 27, 27, 27, 27, 27, 27, 27}, // Estado 27
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 28
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 29
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 30
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 31
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 32
       { F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F,  F ,  F,  F}, // Estado 33

    };
        public Lexico()
        {
            Linea = 1;
            NumCaracter=0;
            log = new StreamWriter("Prueba.log");
            log.AutoFlush = true;
            log.WriteLine("Archivo: prueba.cpp");
            log.WriteLine("Compilado:  "+Date+" "+datetime); //Requerimiento 4
            //Investigar como checar si un archivo existe o no existe
            string path = "Prueba.cpp";
            bool result = File.Exists(path);
            if(result == true)
            {
                archivo = new StreamReader("Prueba.cpp");
                
            }
            else
            {
                throw new Error("Error: El archivo prueba no existe", log);
            }
        }

        public Lexico(string nombre)
        {
            string FileName = nombre;
            string result2;
            result2 = Path.ChangeExtension(FileName, ".log");
            Linea = 1;
            NumCaracter=0;
            log = new StreamWriter(result2);
            log.AutoFlush = true;
            log.WriteLine("Archivo: "+nombre);   
            log.WriteLine("Compilado:  "+Date+" "+datetime); //Requerimiento 4

            //Investigar como checar si un archivo existe o no existe
            string path = nombre;
            bool result = File.Exists(path);
            if(result == true)
            {
                if (Path.GetExtension(path) != ".cpp")
                {
                    throw new Error("Error: El archivo no es de tipo .cpp", log);
                }
                archivo = new StreamReader(nombre);
            }
            else
            {
                throw new Error("Error: El archivo prueba no existe", log);
            }
        }        
        ~Lexico()
        {
            Console.WriteLine("Destructor");
        }
        private void Etiqueta(int Estado)
        {
            switch (Estado)
            {
                case 1: SETClasificacion(Tipos.Identificador); break;
                case 2: SETClasificacion(Tipos.Numero); break;
                case 8: SETClasificacion(Tipos.Asignacion); break;
                case 9:
                case 17:
                case 18:
                case 19: SETClasificacion(Tipos.OperadorRelacional); break;
                case 10:
                case 13:
                case 15:
                case 29: SETClasificacion(Tipos.Caracter); break;
                case 11: SETClasificacion(Tipos.Inicializacion); break;
                case 12: SETClasificacion(Tipos.FinSentencia); break;
                case 14:
                case 16: SETClasificacion(Tipos.OperadorLogico); break;
                case 20:
                case 21: SETClasificacion(Tipos.OperadorTermino); break;
                case 22: SETClasificacion(Tipos.IncrementoTermino); break;
                case 23: SETClasificacion(Tipos.OperadorFactor); break;
                case 24: SETClasificacion(Tipos.IncrementoFactor); break;
                case 25: SETClasificacion(Tipos.Ternario); break;
                case 28: SETClasificacion(Tipos.Cadena); break;
                case 30: SETClasificacion(Tipos.InicioBloque); break;
                case 31: SETClasificacion(Tipos.FinBloque); break;
                case 32: SETClasificacion(Tipos.ParentesisIzquierdo); break;
                case 33: SETClasificacion(Tipos.ParentesisDerecho); break;
                case 34: SETClasificacion(Tipos.FlujoSalida); break;
                case 35: SETClasificacion(Tipos.FlujoEntrada); break;
            }
        }


        private int Columna(Char t)
        {
            if (FinArchivo())
            {
                return 1;
            }
            else if (char.IsWhiteSpace(t))
            {
                return 0;
            }
            else if (char.ToUpper(t) == 'E')
            {
                return 5;
            }
            else if (char.IsLetter(t))
            {
                return 2;
            }
            else if (char.IsLetterOrDigit(t))
            {
                return 3;
            }
            else if (t == '.')
            {
                return 4;
            }
            else if (t == '+')
            {
                return 6;
            }
            else if (t == '-')
            {
                return 7;
            }
            else if (t == '=')
            {
                return 8;
            }
            else if (t == ':')
            {
                return 9;
            }
            else if (t == ';')
            {
                return 10;
            }
            else if (t == '&')
            {
                return 11;
            }
            else if (t == '|')
            {
                return 12;
            }
            else if (t == '!')
            {
                return 13;
            }
            else if (t == '>')
            {
                return 14;
            }
            else if (t == '>'&& t=='>')
            {
                return 27;
            }
            else if (t == '<')
            {
                return 15;
            }
            else if (t == '<'&& t=='<')
            {
                return 28;
            }
            else if (t == '*')
            {
                return 16;
            }
            else if (t == '%')
            {
                return 17;
            }
            else if (t == '/')
            {
                return 18;
            }
            else if (t == '?')
            {
                return 19;
            }
            else if (t == '"')
            {
                return 20;
            }
            else if (t == '\'')
            {
                return 21;
            }
            else if (t == '{')
            {
                return 23;
            }
            else if (t == '}')
            {
                return 24;
            }
            else if (t == '(')
            {
                return 25;
            }
            else if (t == ')')
            {
                return 26;
            }

            else
            {
                return 22;
            }
        }


        public void nextToken()
        {
            string buffer = "";
            char transicion;

            int Estado = 0;

            while (Estado >= 0)
            {
                transicion = (char)archivo.Peek();
                
                Estado = TRAND[Estado, Columna(transicion)];

                Etiqueta(Estado);

                if (Estado >= 0)
                {
                    if(transicion=='\n')
                    {
                        Linea++;
                        NumCaracter=0;
                    }
                    if (Estado > 0)
                    {
                        buffer += transicion;
                    
                    }
                    archivo.Read();
                    NumCaracter++;
                }
            }
            if (Estado == E)
            {
                throw new Error(" Error lexico en la linea "+Linea+" En el caracter "+NumCaracter, log);
            }

            SETContenido(buffer);

            if (GETClasificacion() == Tipos.Identificador)
            {
                switch (GETContenido())
                {
                    case "char":
                    case "int":
                    case "float": SETClasificacion(Tipos.TipoDato); break;

                    case "private":
                    case "protected":
                    case "public": SETClasificacion(Tipos.Zona); break;

                    case "if":
                    case "else":
                    case "switch": SETClasificacion(Tipos.Condicion); break;

                    case "while":
                    case "do":
                    case "for": SETClasificacion(Tipos.Ciclo); break;
                }
            }
            log.WriteLine(GETContenido() + " = " + GETClasificacion());
        }

        protected void cerrarArchivos()
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