using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyMath
{
    public sealed class Wielomian : IEnumerator<int>, IEnumerable<int>, IEquatable<Wielomian>
    {
        private int pozycja = -1;

        public int[] wspolczynniki { get;  }

        public int Stopien { get; }
        
        public static implicit operator Wielomian(int m)
        {
            return new Wielomian(m);
        }

        public static explicit operator int(Wielomian m)
        {
            if (m.wspolczynniki.Length > 1)
                throw new InvalidCastException("wielomian nie jest stopnia zerowego");
            return m.wspolczynniki[0];
        }

        public static explicit operator int[](Wielomian m)
        {
            int[] tab = (int[]) m.wspolczynniki.Clone();
            Array.Reverse(tab);
            return tab;
        }

        public int Current
        {
            get { return wspolczynniki[pozycja]; }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public Wielomian()
        {
            wspolczynniki = new int[1];
            wspolczynniki[0] = 0;
            Stopien = wspolczynniki.Length - 1;
        }
        
        public Wielomian(params int[] wsp)
        {
            if (wsp == null)
                throw new NullReferenceException();
            if (wsp.Length == 0)
                throw new ArgumentException("wielomian nie moze być pusty");

            int dlugosc = SprawdzZera(wsp)[0];
            int indeks = SprawdzZera(wsp)[1];

            wspolczynniki = new int[dlugosc];
            for(int i = 0; i < dlugosc; i++)
            {
                wspolczynniki[i] = wsp[indeks];
                indeks++;
            }

            Stopien = wspolczynniki.Length - 1;
        }

        public override string ToString()
        {
            int stopien = wspolczynniki.Length - 1;
            string str = "";
            for (int i = 0; i < wspolczynniki.Length; i++)
            {
                if((wspolczynniki.Length == 1) && (wspolczynniki[0] == 0))
                {
                    str = str + "0";
                }
                else if (wspolczynniki[i] != 0)
                {
                    if(i == 0)
                    {
                        if (wspolczynniki[i] < 0)
                        {
                            str = str + "-";
                        }
                    }
                    else
                    {
                        if(wspolczynniki[i] > 0)
                        {
                            str = str + " + ";
                        }
                        else
                        {
                            str = str + " - ";
                        }
                    }

                    if (stopien > 1)
                        str = str + SprawdzCzyJeden(Math.Abs(wspolczynniki[i]).ToString()) + "x^" + stopien.ToString();
                    else if (stopien == 1)
                        str = str + SprawdzCzyJeden(Math.Abs(wspolczynniki[i]).ToString()) + "x";
                    else
                        str = str + Math.Abs(wspolczynniki[i]).ToString();
                }
                stopien--;
            }
            return str;
        }
        
        private int[] SprawdzZera(int[] wsp)
        {
            int dlugosc = 0;
            int indeks = 0;
            int[] dlugosc_indeks = new int[2];

            int sprawdzZera = 0;
            foreach (int i in wsp)
            {
                if (i == 0)
                    sprawdzZera++;
            }
            dlugosc = wsp.Length;
            indeks = 0;
            for (int i = 0; i < wsp.Length; i++)
            {
                if (wsp.Length == sprawdzZera)
                {
                    dlugosc = 1;
                    break;
                }
                else if (wsp[i] == 0)   
                {
                    dlugosc--;
                    indeks++;
                }
                else break;
            }
            dlugosc_indeks[0] = dlugosc;
            dlugosc_indeks[1] = indeks;
            return dlugosc_indeks;
        }

        private static string SprawdzCzyJeden(string i) => i == "1" ? "" : i;
        
        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if(pozycja == -1)
            {
                pozycja = wspolczynniki.Length;
            }
            pozycja--;
            return (pozycja >= 0);
        }

        public void Reset() => pozycja = wspolczynniki.Length;

        public IEnumerator<int> GetEnumerator() => (IEnumerator<int>)this;

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

        public bool Equals(Wielomian other)
        {
            if (other == null)
                return false;
            if (this.wspolczynniki.Length != other.wspolczynniki.Length)
                return false;
            for(int i = 0; i < this.wspolczynniki.Length; i++)
            {
                if (this.wspolczynniki[i] != other.wspolczynniki[i])
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Wielomian);
        }

        public static bool operator ==(Wielomian w1, Wielomian w2)
        {
            if (((object)w1) == null || ((object)w2) == null)
                return Object.Equals(w1, w2);

            return w1.Equals(w2);
        }

        public static bool operator !=(Wielomian w1, Wielomian w2)
        {
            if (((object)w1) == null || ((object)w2) == null)
                return !Object.Equals(w1, w2);

            return !(w1.Equals(w2));
        }

        public static Wielomian operator +(Wielomian w1, Wielomian w2)
        {
            Wielomian wieksza = WybierzWiekszaTablice(w1, w2)[0];
            Wielomian mniejsza = WybierzWiekszaTablice(w1, w2)[1];

            int roznica = wieksza.wspolczynniki.Length - mniejsza.wspolczynniki.Length;
            int[] tab = new int[wieksza.wspolczynniki.Length];

            for (int i = 0; i < wieksza.wspolczynniki.Length; i++)
            {
                if (i < mniejsza.wspolczynniki.Length)
                    tab[i] = wieksza[i] + mniejsza[i];
                else
                    tab[i] = wieksza[i] + 0;
            }
            Array.Reverse(tab);
            Wielomian w = new Wielomian(tab);

            return w;
        }

        public static Wielomian operator -(Wielomian w1, Wielomian w2)
        {
            int roznica = 0;
            int[] tab = { 0 };
            Wielomian w;

            if (WybierzWiekszaTablice(w1, w2)[0] == w1)
            {
                roznica = w1.wspolczynniki.Length - w2.wspolczynniki.Length;
                tab = new int[w1.wspolczynniki.Length];

                for (int i = 0; i < w1.wspolczynniki.Length; i++)
                {
                    if (i < w2.wspolczynniki.Length)
                        tab[i] = w1[i] - w2[i];
                    else
                        tab[i] = w1[i] - 0;
                }
            }
            else
            {
                roznica = w2.wspolczynniki.Length - w1.wspolczynniki.Length;
                tab = new int[w2.wspolczynniki.Length];

                for (int i = 0; i < tab.Length; i++)
                {
                    if (i < w1.wspolczynniki.Length)
                        tab[i] = w1[i] - w2[i];
                    else
                        tab[i] = 0 - w2[i];
                }
            }
            Array.Reverse(tab);
            w = new Wielomian(tab);

            return w;
        }

        public int this[int index]
        {
            get{ return wspolczynniki[wspolczynniki.Length - index - 1]; }
        }

        public static Wielomian[] WybierzWiekszaTablice(Wielomian w1, Wielomian w2)
        {
            Wielomian[] tab = new Wielomian[2]; //wieksza tablica na pierwszym miejscu, mneijsza - na drugim


            if (w1.wspolczynniki.Length >= w2.wspolczynniki.Length)
            {
                tab[0] = w1;
                tab[1] = w2;
            }
            else
            {
                tab[0] = w2;
                tab[1] = w1;
            }

            return tab;
        }

        }
}
