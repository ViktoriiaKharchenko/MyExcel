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
            string x = "min(2+5div2,4)=(45-37)/2";
            string expected = "True";
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate0(x);
            string actual = res.GetValue();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string x = "True<>True";
            string expected = "False";
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate0(x);
            string actual = res.GetValue();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestMethod3()
        {
            string x = "True < True";
            string expected = "False";
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate0(x);
            string actual = res.GetValue();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestMethod4()
        {
            string x = "min(34,12+2)>max(32,12+2)";
            string expected = "False";
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate0(x);
            string actual = res.GetValue();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestMethod5()
        {
            string x = "True>False";
            string expected = "True";
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate0(x);
            string actual = res.GetValue();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestMethod6()
        {
            string x = "3div2>3*1>4-1";
            string expected = "False";
            Parser2 pasre = new Parser2();
            Result res = pasre.Evaluate0(x);
            string actual = res.GetValue();
            Assert.AreEqual(expected, actual);
        }

    }
}
