using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;

namespace MyExtensions
{
    public static class MyExtensions
    {
        public static double Eval(this Wielomian w, double liczba)
        {
            double wynik = 0;
            int potega = 0;

            foreach(int x in w.wspolczynniki)
            {
                if (x != 0)
                {
                    double liczbaDoPotegi = Math.Pow(liczba, potega);
                    wynik += (double)x*liczbaDoPotegi;
                }
                potega++;
            }
            return wynik;
        }
    }
}
