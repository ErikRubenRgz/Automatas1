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
                    else if (t=='=')
                    {
                        SiguienteEstado = 8;
                    }
                    else if (t==':')
                    {
                        SiguienteEstado = 10;
                    }
                    else if (t==';')
                    {
                        SiguienteEstado = 12;
                    }
                    else if (t=='&')
                    {
                        SiguienteEstado = 13;
                    }
                    else if (t=='|')
                    {
                        SiguienteEstado = 15;
                    }
                    else if (t=='!')
                    {
                        SiguienteEstado = 16;
                    }
                    else if (t=='>')
                    {
                        SiguienteEstado = 17;
                    }
                    else if (t=='<')
                    {
                        SiguienteEstado = 19;
                    }
                    else if (t=='+')
                    {
                        SiguienteEstado = 20;
                    }
                    else if (t=='-')
                    {
                        SiguienteEstado = 21;
                    }
                    else if (t=='%'||t=='/'||t=='*')
                    {
                        SiguienteEstado = 23;
                    }
                    else if (t=='?')
                    {
                        SiguienteEstado = 25;
                    }
                    
                    else if (t=='"')
                    {
                        SiguienteEstado = 28;
                    }
                    
                    else if (t=='\'')
                    {
                        SiguienteEstado = 28;
                    }
                    
                    else
                    {
                        SiguienteEstado = 29;
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
                    
                    else if (char.ToLower(t)=='e')
                    {
                        SiguienteEstado = 5;
                    }
                    
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 3:
                SETClasificacion(Tipos.Asignacion);
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
                    else if(char.ToLower(t)=='e')
                    {

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
                    case 8:
                    if (t=='=')
                    {
                        SiguienteEstado = 9;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                    case 9:
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.OperadorRelacional);
                    break;
                    case 10:
                    if (t=='=')
                    {
                        SiguienteEstado = 11;
                        
                    }
                    else
                    {
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.Caracter);
                    }
                    break;
                    case 11:
            
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.Inicializacion);
                    break;
                    case 12:
            
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.FinSentencia);
                    break;
                    case 13:
                    if (t=='&')
                    {
                        SiguienteEstado = 14;
                        
                    }
                    else
                    {
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.Caracter);
                    }
                    break;
                    case 14:
                    SiguienteEstado = F;
                        SETClasificacion(Tipos.OperadorLogico);
                    
                    break;
                    case 15:
                    if (t=='|')
                    {
                        SiguienteEstado = 14;
                        
                    }
                    else
                    {
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.Caracter);
                    }
                    break;
                    case 16:
                    if (t=='=')
                    {
                        SiguienteEstado = 18;
                        
                    }
                    else
                    {
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.OperadorLogico);
                    }
                    break;
                    case 18:
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.OperadorRelacional);
                        break;
                    case 17:
                    if (t=='=')
                    {
                        SiguienteEstado = 18;
                        
                    }
                    else
                    {
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.OperadorRelacional);
                    }
                    break;
                    case 19:
                    if (t=='='||t=='>')
                    {
                        SiguienteEstado = 18;
                    
                    }
                    else
                    {
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.OperadorRelacional);
                    }
                    break;
                    case 20:
                    if (t=='=' || t == '+')
                    {
                        SiguienteEstado = 22;
                        SETClasificacion(Tipos.OperadorTermino);
                    }
                    else
                    {
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.OperadorTermino);
                    }
                    break;
                    case 21:
                    if (t=='=' || t == '-')
                    {
                        SiguienteEstado = 22;
                        SETClasificacion(Tipos.IncrementoTermino);
                    }
                    else
                    {
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.IncrementoTermino);
                    }
                    break;
                    case 22:
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.IncrementoTermino);
                    break;
                    case 23:
                    if (t=='=')
                    {
                        SiguienteEstado = 24;
                        SETClasificacion(Tipos.OperadorFactor);
                    }
                    else
                    {
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.OperadorFactor);
                    }
                    break;
                    case 24:
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.IncrementoTermino);
                    break;
                    case 25:
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.Ternario);
                    break;
                    
                    case 26:
                    if (t=='"')
                    {
                        SiguienteEstado = 28;
                        SETClasificacion(Tipos.Cadena);
                    }
                    else
                    {
                        SiguienteEstado = E;
                        SETClasificacion(Tipos.EOF);
                    }
                    break;
                    
                    case 27:
                    if (t=='\'')
                    {
                        SiguienteEstado = 28;
                        SETClasificacion(Tipos.Cadena);
                    }
                    else
                    {
                        SiguienteEstado = E;
                        SETClasificacion(Tipos.EOF);
                    }
                    break;
                    
                    case 28:
                    
                        SiguienteEstado = F;
                        SETClasificacion(Tipos.Cadena);
                    
                    break;
                    
                case 29:
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