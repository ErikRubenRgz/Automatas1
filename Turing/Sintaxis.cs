using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TURING
{
    public class Sintaxis : Lexico
    {
        public Sintaxis(string nombre) : base(nombre)
        {
            nextToken();
        }
        public Sintaxis()
        {
            nextToken();
        }

        public void match(string espera)
        {
            if (GETContenido() == espera)
            {
                nextToken();
            }
            else
            {
                throw new Error("Error de Sintaxis: Se espara un "+espera,log);
            }
        }

        public void match(Tipos espera)
        {
            if (GETClasificacion() == espera)
            {
                nextToken();
            }
            else
            {
                throw new Error("Error de Sintaxis: Se espara un "+espera,log);
            }
        }

    }
}