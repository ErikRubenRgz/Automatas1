using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*

    Requirimiento 1: Agregar el token >> (flujoSalida) y el toke << (flujo de Entrada)
    Requerimiento 2: Documentar los tokens en el archiv de Lista: {, }, (, ), >> y <<
                     MOdelar en JFLAP todos los autómatas
    Requerimiento 3: Usar el Garbage Collector o el IDisposable para ejectura el destructor
    Requerimiento 4: El constructor Lexico sin argumento deben de compilar prueba.cpp
                     y validar que exista, y genera prueba.log. 
                     El constructor Lexico con argumentos debe de compilar un archivo
                     con extension CPP (Path) y validar que exista y que tenga extensional
                     CPP y generar un LOG con el mismo nombre

                     area.cpp -> area.log

    Requerimiento 5: Agregar en archivo LOG agregar el nombre del archivo a compilar y
                     la hora de compilación

                     Archvivo: area.cpp
                     Hora de compilación: 14-NOV-2022 15:39
*/

namespace Turing
{
    public class Lenguaje : Sintaxis
    {
        public Lenguaje(string nombre) : base(nombre)
        {

        }
        public Lenguaje()
        {

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
        /* 
         Main -> void main()
         {
            TipoDato ListaIdentificadores; 
            Lista_Instrucciones
         }
         */
        private void Main()
        {
            match("void");
            match("main");
            match("(");
            match(")");
            match("{");
            
            /*
            match(Tipos.TipoDato);
            Lista_Identificadores();
            match(";");
            */

            Lista_Instrucciones();
            match("}");
        }
        // Lista_Identificadores -> Identificador (, Lista_Identificadores)?
        private void Lista_Identificadores()
        {
            match(Tipos.Identificador);
            if (GETContenido() == ",")
            {
                match(",");
                Lista_Identificadores();
            }
        }
        // Lista_Instrucciones -> printf | scanf Lista_Instrucciones?
        private void Lista_Instrucciones()
        {
            if (GETContenido() == "cout")
            {
                Cout();
            }
            else
            {
                Cin();
            }
            if (GETContenido() != "}")
            {
                Lista_Instrucciones();
            }
        }
        // Cout -> cout FlujoSalida Cadena | Identificador ;
        private void Cout()
        {
            match("cout");
            match(">");
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
        // Cint -> cin FlujoEntrada Identificador ;
        private void Cin()
        {
            match("cin");
            match("<");
            match(Tipos.Identificador);
            match(";");
        }

    }
}