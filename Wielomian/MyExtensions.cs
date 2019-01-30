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

            foreach (int x in w.wspolczynniki)
            {
                if (x != 0)
                {
                    double liczbaDoPotegi = Math.Pow(liczba, potega);
                    wynik += (double)x * liczbaDoPotegi;
                }
                potega++;
            }
            return wynik;
        }
    }
    public static class MyMathExtensions
    {
        public static Comparison<Wielomian> WielomianPoprzedzaComparison = new Comparison<Wielomian>(WielomianPoprzedza);

        public static int WielomianPoprzedza(Wielomian w1, Wielomian w2)
        {
            if (w1.Stopien > w2.Stopien) return 1;
            else if (w1.Stopien == w2.Stopien) return StopienPoprzedza(w1, w2);
            else return -1;
        }

        public static int StopienPoprzedza(Wielomian w1, Wielomian w2)
        {
            int czyRowne = 0;
            for (int i = 0; i < w1.wspolczynniki.Length; i++)
            {
                if (w1.wspolczynniki[i] > w2.wspolczynniki[i])
                    return 1;
                if (w1.wspolczynniki[i] == w2.wspolczynniki[i])
                    czyRowne++;
            }
            if (czyRowne < w1.wspolczynniki.Length) return -1;
            else return 0;
        }
    }
}
