using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace laba2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Cell[,] table = new Cell[500, 500];
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    table[i, j] = new Cell();
                }
            }
           string actual= table[1, 1].getName(1, 1);
            string expected = "B1";
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void TestMethod2()
        {
            Cell[,] table = new Cell[500, 500];
            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    table[i, j] = new Cell();
                }
            }
            string actual = table[123, 4].getName(4, 123);
            string expected = "E123";
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void TestMethod3()
        {
            Cell[,] table = new Cell[500, 500];
            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    table[i, j] = new Cell();
                }
            }
            string actual = table[123, 26].getName(26, 123);
            string expected = "ZB123";
            Assert.AreEqual(expected, actual);

        }
       
        
        [TestMethod]
        public void TestMethod4()
        {
            Cell[,] table = new Cell[500, 500];
            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    table[i, j] = new Cell();
                }
            }
            string actual = table[0, 0].getName(-1, 0);
            string expected = "ERROR";
            Assert.AreEqual(expected, actual);

        }



    }


}
