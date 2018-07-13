using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LpFileParser.Tests
{
    [TestClass()]
    public class TermParserTests
    {
        [TestMethod()]
        public void ParseTest_One_Variable()
        {
            var parser = new TermParser();
            var t = parser.Parse("x0");
            Assert.AreEqual(expected: 1, actual: t.Tokens.Count);

            var v = t.Tokens[0] as Variable;
            Assert.IsNotNull(v);
            Assert.AreEqual(expected: "x0", actual: v.Value);
        }

        [TestMethod()]
        public void ParseTest_One_Constant()
        {
            var parser = new TermParser();
            var t = parser.Parse("14.3");
            Assert.AreEqual(expected: 1, actual: t.Tokens.Count);

            var c = t.Tokens[0] as Constant;
            Assert.IsNotNull(c);
            Assert.IsTrue(14.3 - c.Value < 0.0001);
        }

        [TestMethod()]
        public void ParseTest_One_Operator()
        {
            var parser = new TermParser();
            var t = parser.Parse(">=");
            Assert.AreEqual(expected: 1, actual: t.Tokens.Count);

            var c = t.Tokens[0] as Operator;
            Assert.IsNotNull(c);
            Assert.AreEqual(">=", c.Value);
        }

        [TestMethod()]
        public void ParseTest_Full_Line()
        {
            var parser = new TermParser();
            var t = parser.Parse("4*x0+5*x1+6*x2=0;");
            Assert.AreEqual(13, t.Tokens.Count);

            var c = t.Tokens[0] as Constant;
            Assert.AreEqual(4, c.Value);

            c = t.Tokens[4] as Constant;
            Assert.AreEqual(5, c.Value);

            c = t.Tokens[8] as Constant;
            Assert.AreEqual(6, c.Value);

            c = t.Tokens[12] as Constant;
            Assert.AreEqual(0, c.Value);
        }
    }
}