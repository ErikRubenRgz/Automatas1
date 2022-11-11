using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TURING
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
            match("void");
            match("main");
            match("(");
            match(")");
            match("{");

            match(Tipos.TipoDato);
            Lista_Identificadores();
            match(";");
            
            Lista_Instrucciones();
            match("}");
        }
        //Lista de Identificadores -> Identificador Lista_Identificadores?
        private void ListaIdentificadores()

        {
        match(Tipos.Identificador);
        if(GETContenido() == ",")
            {
                match(",");
                ListaIdentificadoresP();
            }
         }
            
            
        }
        // Main -> void main { Lista_Instrucciones }
        private void Main()
        {
            // void main { Lista_Instrucciones }
        }
        // Lista_Instrucciones -> printf | scanf Lista_Instrucciones?
        private void Lista_Instrucciones()
        {
            // printf | scanf Lista_Instrucciones?
        }
    }
}