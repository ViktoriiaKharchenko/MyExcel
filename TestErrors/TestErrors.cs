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
            string x = "34+2/(3*4-12)";
            string expected = "#ERROR";
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate(x);
            string actual = res.GetValue();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string x = "34+2/((3*4-12)";
            string expected = "#ERROR";
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate(x);
            string actual = res.GetValue();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string x = "34+2^(3*4-12)";
            string expected = "#ERROR";
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate(x);
            string actual = res.GetValue();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestMethod4()
        {
            string x = "min(56+5/4div2,)";
            string expected = "#ERROR";
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate(x);
            string actual = res.GetValue();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestMethod5()
        {
            string x = "56*a-67/6";
            string expected = "#ERROR";
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate(x);
            string actual = res.GetValue();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestMethod6()
        {
            string x = "mod4";
            string expected = "#ERROR";
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate(x);
            string actual = res.GetValue();
            Assert.AreEqual(expected, actual);
        }

    }
}
