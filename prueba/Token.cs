using System;

namespace Prueba
{
    public class Token
    {

        public enum Tipos
        {
            Identificador, Numero, Asignacion, FinSentencia, Ternario, 
            OperadorRelacional, OperadorTermino, OperadorFactor, 
            IncrementoTermino, IncrementoFactor, Caracter,Inicialización,
            OperadorLogico, TipoDato, Zona, Condicion,Ciclo, Cadena
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