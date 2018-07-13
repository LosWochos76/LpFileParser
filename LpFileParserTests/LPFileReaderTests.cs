using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LpFileParser.Tests
{
    [TestClass()]
    public class LpFileReaderTests
    {
        [TestMethod()]
        public void FromStringsTest()
        {
            var lp_text = new string[]
            {
                "// Objective function:",
                "min: + 1 * x0 + 3 * x1 + 3 * x2 + 5 * x3 + 2 * x4;",
                "// Constraints:",
                "+ 1.1 * x0 + 1 * x1 + 1 * x2 + 4 * x3 + 5 * x4 >= 5;",
                "+ 4 * x0 + 1 * x1 + 4 * x2 + 3 * x3 + 3 * x4 >= 9;",
                "+ 5 * x0 + 1 * x1 + 1 * x2 + 4 * x3 + 4 * x4 >= 15;"
            };

            var reader = new LpFileReader();
            var lpfile = reader.FromStrings(lp_text);
            var dump = lpfile.ToString().Split('\n');

            Assert.AreEqual(lp_text.Length, dump.Length-1);

            for (int i=0; i<lp_text.Length; i++)
            {
                Assert.AreEqual(lp_text[i], dump[i]);
            }
        }
    }
}