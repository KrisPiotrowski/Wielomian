using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WielomianNS;

namespace WielomianNS
{
    class Program
    {
        static void Main(string[] args)
        {
            Wielomian w = new Wielomian();
            Console.WriteLine(   w.ToString());

            Wielomian w2 = new Wielomian(-4,-4,2,2,0);
            Console.WriteLine("w2 = "+   w2.ToString());

            Wielomian w4 = new Wielomian(3,3,0);
            Console.WriteLine("w4 = " + w4.ToString());

            Wielomian w6 = w2 + w4;
            Console.WriteLine("Suma w2  i w4: " + w6.ToString());

            w6 = w2 - w4;
            Console.WriteLine("Róznica w2  i w4: " + w6.ToString());

            foreach (int i in w4)
            {
                Console.WriteLine("To jest i: " + i);
            }

            int[] tabwsp = { -9, 8, 5 };
            Wielomian w3 = new Wielomian(0,3, 3, 0, 0, 0, -4, 5, 1, 4);
            Console.WriteLine(w3.ToString());
            Console.WriteLine("Pierwszy współczynnik: " + w3[0]);
            Console.WriteLine("Drugi współczynnik: " + w3[1]);
            Console.WriteLine("Trzeci współczynnik: " + w3[2]);

            foreach (int i in w3)
            {
                Console.WriteLine("To jest i: " + i);
            }
            
            Wielomian w5 = new Wielomian( 0,3, 3, 0, 0, 0, 4, 5, 2, 3, 0);
            Console.WriteLine(w5.ToString());

            if(w3 != w4)
                Console.WriteLine("1");
            else
                Console.WriteLine("0");
        }
    }
}
