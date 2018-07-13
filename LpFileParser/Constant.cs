using System;

namespace LpFileParser
{
    public class Constant : Token
    {
        public double Value
        {
            get
            {
                return Convert.ToDouble(text);
            }
        }
    }
}
