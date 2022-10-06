using System;
using System.Collections.Generic;

StreamWriter sw = new StreamWriter("C:\\Users\\erixk\\Documents\\Automatas1\\prueba\\numero.csv");
sw.AutoFlush = true;
List<double> l = new List<double>();

int x0 = 6, a = 61, c = 8191, m = 8192;
double suma = 0.0;

void lineal(int x, int a, int c, int m, List<double> l)
{
    if (l.Count != m)
    {
        int r = ((a * x) + c) % m;
        l.Add((double)r / (m - 1));
        lineal(r, a, c, m, l);
    }
}
/*Boolean pattern(List<double> l)
{
    int m = (l.Count - 1) / 2;
    Boolean continua = true, r = true;
    for (int i = 0; i != m && continua; i++)
    {
        if (l.ElementAt(i) == l.ElementAt(i + m))
            continua = true;
        else
        {
            continua = false;
            r = false;
        }
    }
    return r;
}*/

lineal(x0, a, c, m, l);
//int x = 0;
l.Insert(0, ((double)x0 / (m - 1)));
l.RemoveAt(m);
foreach (double i in l)
{
    suma = suma + i;
    sw.WriteLine(i);
    //if (++x == m - 1)
    //    sw.WriteLine();
}

/*if (!pattern(l))
{
    Console.WriteLine("No se encontro patron.");
}
else
{
    Console.WriteLine("Se encontro patron.");
}*/
sw.Close();
suma = suma / m;
//Console.WriteLine(suma);

void pIndependencia(List<double> ri, double zt)
{
    int[] corridas = new int[ri.Count];
    int c0 = 0, n = ri.Count, n0 = 0, n1 = 0;
    double valorEsperado = 0.0;
    double varianza = 0.0;
    double z0 = 0.0;

    foreach (float i in ri)
    {
        if (i >= 0.5)
        {
            corridas[c0] = 1;
            n1++;
        }
        else
        {
            corridas[c0] = 0;
            n0++;
        }
        c0++;
    }

    c0 = 1;
    int actual = corridas[0];
    for (int i = 0; i < ri.Count; i++)
    {
        if (corridas[i] != actual)
        {
            c0++;
            actual = corridas[i];
        }
    }

    valorEsperado = ((2.0 * n0 * n1) / n) + (1.0 / 2.0);
    varianza = (2.0 * n0 * n1 * (2.0 * n0 * n1 - n)) / (Math.Pow(n, 2) * (n - 1.0));
    z0 = (c0 - valorEsperado) / (Math.Sqrt(varianza));

    if (-zt < z0 && z0 < zt)
        Console.WriteLine("No se puede rechazar que el conjunto de ri. es independiente.");
    else
        Console.WriteLine("Los números del conjunto ri no son independientes.");
    Console.WriteLine(z0);
}

pIndependencia(l, 1.96);