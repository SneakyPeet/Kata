using System;
using NUnit.Framework;

namespace Kata.StringCalculator
{
    [TestFixture]
    class Tests //http://osherove.com/tdd-kata-1/
    {
        private Calculator calculator;

        [TestFixtureSetUp]
        public void Setup()
        {
            calculator = new Calculator();
        }

        [Test]
        public void GivenEmptyString_ReturnZero()
        {
            var result = calculator.Add("");
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GivenNumberString_ReturnNumber()
        {
            var result = calculator.Add("1");
            Assert.AreEqual(1, result);
        }

        [Test]
        public void GivenCommaSeperatedNumberString_ReturnSumOfNumbers()
        {
            var result = calculator.Add("1,2,3");
            Assert.AreEqual(6, result);
        }

        [Test]
        public void GivenNewLineSeperatedNumberString_ReturnSumOfNumbers()
        {
            var result = calculator.Add("1\n2,3");
            Assert.AreEqual(6, result);
        }

        [Test]
        public void GivenAProvidedDelimiterOnly_ReturnZero()
        {
            var result = calculator.Add("//;\n");
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GivenAProvidedDelimiterWithSingleNumber_ReturnNumber()
        {
            var result = calculator.Add("//;\n2");
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GivenAProvidedDelimiterWithArrayOfNumbers_ReturnTotal()
        {
            var result = calculator.Add("//;\n1;2;3");
            Assert.AreEqual(6, result);
        }

        [Test]
        [TestCase("-1","-1")]
        [TestCase("2\n-5", "-5")]
        [TestCase("//*\n-1*5*-3", "-1,-3")]
        public void GivenNegativeNumbers_ThrowExceptionWithNumber(string testcase, string expectedException)
        {
            try
            {
                calculator.Add(testcase);
            }
            catch(ArgumentException ae)
            {
                Assert.AreEqual(expectedException, ae.Message);
                return;
            }
            Assert.Fail("Exception Was Not Thrown");
        }

        [Test]
        [TestCase("1000", 0)]
        [TestCase("2\n5000", 2)]
        [TestCase("//*\n1*5*3000", 6)]
        public void GivenHighNumbers_IgnoreNumbers(string testcase, int expectedTotal)
        {
            var result = calculator.Add(testcase);
            Assert.AreEqual(expectedTotal, result);
        }

        [Test]
        public void GivenDelimiterOfAnyLength_ReturnSumOfNumbers()
        {
            var result = calculator.Add("//[***]\n1***5***3");
            Assert.AreEqual(9, result);
        }

        [Test]
        [TestCase("//[*][%]\n1*2%3", 6)]
        [TestCase("//[***][%]\n1***2%3", 6)]
        public void GivenMultipleDelimiters_ReturnSumOfNumbers(string testcase, int expectedTotal)
        {
            var result = calculator.Add(testcase);
            Assert.AreEqual(expectedTotal, result);
        }
}
}
