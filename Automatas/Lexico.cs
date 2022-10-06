using System;
using System.IO;

namespace Automatas
{
    public class Lexico : Token
    {
        StreamReader archivo;
        StreamWriter log;

        const int F = -1;
        const int E = -2;
        
        public Lexico()
        {
            archivo = new StreamReader("Entrada.txt");
            log = new StreamWriter("Salida.txt");
            log.AutoFlush = true;
        }

        public Lexico(string filename)
        {
            archivo = new StreamReader(filename);
            log = new StreamWriter("Salida.txt");
            log.AutoFlush = true;
        }

        ~Lexico()
        {
            Console.WriteLine("Destructor");
        }

        private int Automata(int Estado, char t)
        {
            int SiguienteEstado = Estado;

            switch (Estado)
            {
                case 0:
                    if (char.IsWhiteSpace(t))
                    {
                        SiguienteEstado = 0;
                    }
                    else if (char.IsLetter(t))
                    {
                        SiguienteEstado = 1;
                    }
                    else if (char.IsDigit(t))
                    {
                        SiguienteEstado = 2;
                    }
                    else
                    {
                        SiguienteEstado = 28;
                    }
                    break;
                case 1:
                    SETClasificacion(Tipos.Identificador);
                    if (char.IsLetterOrDigit(t))
                    {
                        SiguienteEstado = 1;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 2:
                    SETClasificacion(Tipos.Numero);
                    if (char.IsDigit(t))
                    {
                        SiguienteEstado = 2;
                    }
                    else if (t=='.')
                    {
                        SiguienteEstado = 3;
                    }
                    /*
                    else if (char.tolower(t)=='e')
                    {
                        SiguienteEstado = 5;
                    }
                    */
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 3:
                    if (char.IsDigit(t))
                    {
                        SiguienteEstado = 4;
                    }
                    else
                    {
                        SiguienteEstado = E;
                    }
                    break;
                case 4:
                    if (char.IsDigit(t))
                    {
                        SiguienteEstado = 4;
                    }
                    else if(char.tolower(t)=='e'){
                        SiguienteEstado = 5;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                    case 5:
                    if (t=='+'||t=='-')
                    {
                        SiguienteEstado = 6;
                    }
                    else if (char.IsDigit(t))
                    {
                        SiguienteEstado = 6;
                    }
                    else {
                        SiguienteEstado = E;
                    }
                    break;
                    case 6:
                    if (char.IsDigit(t))
                    {
                        SiguienteEstado = 7;
                    }
                    else
                    {
                        SiguienteEstado = E;
                    }
                    break;
                    case 7:
                    if (char.IsDigit(t))
                    {
                        SiguienteEstado = 7;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                    
                case 28:
                    SETClasificacion(Tipos.Caracter);
                    SiguienteEstado = F;
                    break;
            }
            return SiguienteEstado;
        }

        public void nextToken()
        {
            string  buffer = "";
            char    transicion;

            int Estado = 0;

            while (Estado >= 0)
            {
                transicion = (char) archivo.Peek();

                Estado = Automata(Estado, transicion);

                if (Estado >= 0)
                {
                    if (Estado > 0)
                    {
                        buffer += transicion;
                    }
                    archivo.Read();
                }
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