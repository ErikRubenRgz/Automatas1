using System;

namespace TURING
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using(Lenguaje l=new Lenguaje())
                {
                    l.Programa();
                }
                Lenguaje L = new Lenguaje();

                // #include <Identificador.h>
 
                while (!L.FinArchivo())
                {
                    L.nextToken();
                }
            }
            catch (Error e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
