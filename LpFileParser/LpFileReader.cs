using System;
using System.IO;
using System.Linq;

namespace LpFileParser
{
    enum ParserState
    {
        None,
        Variable,
        Constant,
        Operator
    }

    public class LpFileReader
    {
        public LpFile FromFile(string filename)
        {
            if (!File.Exists(filename))
                throw new Exception("File does not exist!");

            string[] lines = File.ReadAllLines(filename);
            return FromStrings(lines);
        }

        public LpFile FromStrings(string[] strings)
        {
            var result = new LpFile();
            strings = strings.Where(x => !x.StartsWith("//") || x == string.Empty).ToArray();
            ParseObjectiveFuntion(strings[0], result);
            ParseContraints(strings, result);
            return result;
        }

        private void ParseContraints(string[] strings, LpFile result)
        {
            var parser = new TermParser();

            for (int i = 1; i < strings.Length; i++)
            { 
                var constraint = parser.Parse(strings[i]);
                result.Constraints.Add(constraint);
            }
        }

        private void ParseObjectiveFuntion(string line, LpFile result)
        {
            if (line.Contains(':'))
            {
                if (line.Substring(0, line.IndexOf(":")) == "max")
                    result.Type = OptimizationType.Max;

                line = line.Substring(line.IndexOf(":") + 1);
            }

            var parser = new TermParser();
            result.Objective = parser.Parse(line);
        }
    }
}