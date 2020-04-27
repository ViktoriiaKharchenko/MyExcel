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
            string x = "12+(2*3)-3";
            double expected = 15;
            Parser2 parse = new Parser2();
           Result res =  parse.Evaluate(x);
            double actual = res.Value;
            Assert.AreEqual(actual, expected);

        }
        [TestMethod]
        public void TestMethod2()
        {
            string x = "(12mod3/3)*2div3";
            double expected = 0;
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate(x);
            double actual = res.Value;
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void TestMethod3()
        {
            string x = "min(38+6/3*4,48-3*0)";
            double expected = 46;
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate(x);
            double actual = res.Value;
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void TestMethod4()
        {
            string x = "min(38+6/3*4+20,max(48-3*0,56-3*4))";
            double expected = 48;
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate(x);
            double actual = res.Value;
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void TestMethod5()
        {
            string x = "(48-3)mod((56-3)*4)";
            double expected = 45;
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate(x);
            double actual = res.Value;      
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void TestMethod()
        {
            string x = "(48-3)mod(56-3)*4";
            double expected = 180;
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate(x);
            double actual = res.Value;
            Assert.AreEqual(actual, expected);
        }
    }
}
