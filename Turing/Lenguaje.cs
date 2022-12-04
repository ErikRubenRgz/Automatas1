using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*

    Requirimiento 1: Agregar el token >> (flujoSalida) y el toke << (flujo de Entrada)
    Requerimiento 2: Documentar los tokens en el archiv de Lista: {, }, (, ), >> y <<
                     Modelar en JFLAP todos los autómatas
    Requerimiento 3: El constructor Lexico con argumentos debe validar que tenga extension     (Listo)
                     CPP y generar un LOG con el mismo nombre

                     area.cpp -> area.log

    Requerimiento 4: Agregar en archivo LOG agregar el nombre del archivo a compilar y        (Listo)
                     la hora de compilación

                     Archvivo: area.cpp
                     Hora de compilación: 14-NOV-2022 15:39
    Requerimiento 5: Agregar a la consdicion el else (optativo)                                (Listo)
    Requerimiento 6: Agregar la produccion del For                                            (Listo)
    Requerimiento 7: Agregar el número de caracter en el error lexico o sintáctico           (Listo)
    Requerimiento 8: Considerar la asignacion en el for                                       (Listo)
    Requerimiento 9: If -> if (Condicion) BloqueInstricciones | Instruccion (else BloqueInstricciones | Instruccion)? (listo)
                     Condicion -> Expresion OperadorRelacional Expresion
*/

namespace TURING
{
    public class Lenguaje : Sintaxis, IDisposable
    {
        public Lenguaje(string nombre) : base(nombre)
        {

        }
        public Lenguaje()
        {

        }
        public void Dispose()
        {
            Console.WriteLine("Cerrando Archivos");
            cerrarArchivos();
        }
        // Programa -> Librerias Main
        public void Programa()
        {
            Librerias();
            Main();
        }
        //Librerias -> #include <Identificador.h> Librerias?
        private void Librerias()
        {
            match("#");
            match("include");
            match("<");
            match(Tipos.Identificador);
            match(".");
            match("h");
            match(">");
            if (GETContenido() == "#")
            {
                Librerias();
            }
            
        }
        // Main -> void main() BloqueInstricciones
        private void Main()
        {
            match("void");
            match("main");
            match("(");
            match(")");
            BloqueInstricciones();
        }
        // BloqueInstricciones -> { Lista_Instrucciones }
        private void BloqueInstricciones()
        {
            match("{");
            Lista_Instrucciones();
            match("}");
        }
        //Lista_Instrucciones -> Instruccion Lista_Instrucciones?
        private void Lista_Instrucciones()
        {
            Instruccion();
            if (GETContenido() != "}")
            {
                Lista_Instrucciones();
            }
        }
        // Instruccion -> Cout | Cin | If 
        private void Instruccion()
        {   
            if(GETContenido()=="Librerias")
            {
                Librerias();
            }
            else if (GETContenido() == "cout")
            {
                Cout();
            }
            else if (GETContenido() == "cin")
            {
                Cin();
            }
            else if (GETContenido() == "if")
            {
                If();
            }
            else if (GETContenido() == "for")
            {
                For();
            }
            else if (GETContenido() == "while")
            {
                While();
            }
            else if (GETContenido() == "do")
            {
                Do();
            
            }
            else
            {
                Asignacion();
            }
        }

        // Asignacion -> Identificador = Expresion ;
        private void Asignacion()
        {
            match(Tipos.Identificador);
            if(GETContenido() == "++" || GETContenido() == "--")
            {
                match(GETContenido());
            }
            else
            {
                match(Tipos.Asignacion);
                Expresion();
            }
            match(Tipos.FinSentencia);
        }

        //Expresion  -> Termino MasTermino
        private void Expresion()
        {
            Termino();
            MasTermino();
        }
        //MasTermino -> (OperadorTermino Termino)?
        private void MasTermino()
        {
            if (GETClasificacion() == Tipos.OperadorTermino)
            {
                match(Tipos.OperadorTermino);
                Termino();
            }
        }
        //Termino    -> Factor PorFactor
        private void Termino()
        {
            Factor();
            PorFactor();
        }
        //PorFactor  -> (OperadorFactor Factor)?
        private void PorFactor()
        {
            if (GETClasificacion() == Tipos.OperadorFactor)
            {
                match(Tipos.OperadorFactor);
                Factor();
            }
        }
        //Factor     -> Numero | Identificador | (Expresion)
        private void Factor()
        {
            if (GETClasificacion() == Tipos.Numero)
            {
                match(Tipos.Numero);
            }
            else if (GETClasificacion() == Tipos.Identificador)
            {
                match(Tipos.Identificador);
            }
            else
            {
                match("(");
                Expresion();
                match(")");
            }
        }

        //For -> for (identificador Asignacion Numero; Condicion; Ientificador Incremnto Factor)
        //BloqueInstricciones | Instruccion
        //Requerimiento 6: Agregar la produccion del For  
        //Requerimiento 8: Considerar la asignacion en el for
           private void For()
        {
            match("for");
            match("(");
            Asignacion();
            Condicion();
            match(";");
            IncrementoTermino();
            match(")");
            if(GETContenido() == "{")
            {
                BloqueInstricciones();
            }
            else
            {
                Instruccion();
            }
        }
        // While -> while (Condicion) BloqueInstricciones | Instruccion ;
        private void While()
        {
            match("while");
            match("(");
            Condicion();
            match(")");
            if (GETContenido() == "{")
            {
                BloqueInstricciones();
            }
            else
            {
                Instruccion();
            }
        }

        // Do -> do BloqueInstricciones | Instruccion while (Condicion) ;
        private void Do()
        {
            match("do");
            if (GETContenido() == "{")
            {
                BloqueInstricciones();
            }
            else
            {
                Instruccion();
            }
            match("while");
            match("(");
            Condicion();
            match(")");
            match(";");
        }
        // If -> if (condicion) BloqueInstricciones | Instruccion
        // Instruccion (else BloqueInstricciones | Instruccion)?
        private void If()
        {
            match("if");
            match("(");
            Condicion();
            match(")");
            if(GETContenido() == "{")
            {
                BloqueInstricciones();
            }
            else
            {
                Instruccion();
            }
            //Else (optativo) Requerimiento 5
            if(GETContenido() == "else")
            {
                match("else");
                if(GETContenido()== "{")
                {
                    BloqueInstricciones();
                }
                else
                {
                    Instruccion();
                }
            }
        }
        //Condicion -> Expresion OperadorRelacional Expresion
        private void Condicion()
        {
            if (GETClasificacion() == Tipos.Numero)
            {
                match(Tipos.Numero);
            }
            else if(GETClasificacion()==Tipos.Identificador)
            {
                match(Tipos.Identificador);
            }
            else
            {
                match(Tipos.Identificador);
            }
            match(Tipos.OperadorRelacional);
            if (GETClasificacion() == Tipos.Numero)
            {
                match(Tipos.Numero);
            }
            else if (GETClasificacion() == Tipos.Identificador)
            {
                match(Tipos.Identificador);
            }
            else
            {
                match(Tipos.Identificador);
            }
        }
        // Cout -> cout FlujoSalida Cadena | Identificador ;
        private void Cout()
        {
            match("cout");
            FlujoSalida();
            if (GETClasificacion() == Tipos.Cadena)
            {
                match(Tipos.Cadena);
            }
            else
            {
                match(Tipos.Identificador);
            }
            match(";");
        }
         //Incremento -> identificador ++ | --                                                                      
        private void IncrementoTermino()
        {
            match(Tipos.Identificador);
            if(GETContenido()[0]=='+')
            {
                match("++");
            }
            else if(GETContenido()[0]=='-')
            {
                match("--");
            }
        }
        // Cin -> cin FlujoEntrada Identificador ;
        private void Cin()
        {
            match("cin");
            FlujoEntrada();
            match(Tipos.Identificador);
            match(";");
        }
        // FlujoSalida -> >>
        private void FlujoSalida()
        {
            if(GETContenido()[0]=='>')
            {
                match(">>");
            }
        }
        private void FlujoEntrada()
        {
            if(GETContenido()[0]=='<')
            {
                match("<<");
            }
        }
    }
}

