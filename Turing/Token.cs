using System;

namespace TURING
{
    public class Token
    {

        public enum Tipos
        {
            Identificador, Numero, Asignacion, FinSentencia, Ternario, 
            OperadorRelacional, OperadorTermino, OperadorFactor, 
            IncrementoTermino, IncrementoFactor, OperadorLogico, 
            Inicializacion, TipoDato, Zona, Condicion, Ciclo, Caracter,Cadena, InicioBloque, FinBloque,
            ParentesisDerecho,ParentesisIzquierdo, FlujoEntrada, FlujoSalida
        }
        
        private string Contenido;
        private Tipos Clasificacion;

        public Token()
        {
            Contenido = "";
        }

        public void SETContenido( string Contenido )
        {
            this.Contenido = Contenido;
        }

        public void SETClasificacion( Tipos Clasificacion )
        {
            this.Clasificacion = Clasificacion;
        }

        public string GETContenido()
        {
            return this.Contenido;
        }

        public Tipos GETClasificacion()
        {
            return this.Clasificacion;
        }

    }
}