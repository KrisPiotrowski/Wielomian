using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WielomianNS;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_SprawdzStopien()
        {
            Wielomian w = new Wielomian(0,0,2,3,6,0);
            int stopienSpodziewany = 3;
            int stopienOtrzymany = w.Stopien;
            Assert.AreEqual(stopienOtrzymany, stopienSpodziewany);
        }

        [TestMethod]
        public void Test_SprawdzDodawanie()
        {
            Wielomian w1 = new Wielomian(1, 2, 0, 3);
            Wielomian w2 = new Wielomian(5, 0, -1, 1, 1);
            Wielomian w3 = w1 + w2;
            int[] tab = { 5,1,1,1,4};
            bool expected = true;

            for(int i = 0; i < w3.wspolczynniki.Length; i++)
            {
                if(w3[i] != tab[i])
                {
                    expected = false;
                    break;
                }
            }
            Assert.AreEqual(expected, true);
        }

    }
}
