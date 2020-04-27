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
            Form1 form = new Form1();
            Cell[,] table = new Cell[100, 100];
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    table[i, j] = new Cell();
                }
            }
            table[0, 0].depends.Add("A0");
            table[0, 0].depends.Add("A1");
            bool expected = true;
            bool actual = form.circle(table[0, 0], 0, 0, 0, 0);
            Assert.AreEqual(expected, actual);
        }
    }
}
