using System;
using System.Collections.Generic;
using System.Linq;

namespace LpFileParser
{
    public class Term
    {
        public string Name { get; set; }
        public List<Token> Tokens { get; private set; }

        public Term()
        {
            Name = string.Empty;
            Tokens = new List<Token>();
        }

        public override string ToString()
        {
            var s = String.Join(" ", Tokens.Select(x => x.ToString()).ToArray()); ;

            if (Name != string.Empty)
                return Name + ": " + s;
            else
                return s;
        }

        public List<string> VariableNames
        {
            get
            {
                return Tokens.Where(x => x is Variable).Select(x => x.ToString()).OrderBy(x => x).ToList();
            }
        }
    }
}
