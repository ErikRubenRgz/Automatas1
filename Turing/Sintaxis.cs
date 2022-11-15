using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
Requerimientos 
    Requerimiento 1: Agregar el token >> (flujoSalida) y el toke << (flujo de entrada)
    Requerimiento 2: Documentar los tokens en el archivo de lista {,},(,),>> y <<
    Requerimiento 3: Usar el Garbage Collector o el     IDisposable para ejecutar el destrcutor
    Requerimeiento 4: El constructor Lexico sin argumento debe de compilar prueba.cpp 
                     y validar que exista, y genera prueba.log.
                     El constructor Lexico con archivo debe de compilar un archivo 
                     con extensiones CPP (Path) y validar que exista y que tenga extensiones
                     CPP y generar un LOG con el mismo nombre.

                     area.cpp --> area.log
    Requerimiento 5: Agregar en archivo LOG agregar el nombre del archivo a compilar y                   


*/
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