using System.Collections.Generic;

namespace LpFileParser
{
    public enum OptimizationType
    {
        Min,
        Max
    }

    public class LpFile
    {
        public OptimizationType Type { get; set; }
        public Term Objective { get; set; }
        public List<Term> Constraints { get; set; }

        public LpFile()
        {
            Type = OptimizationType.Min;
            Constraints = new List<Term>();
        }

        public override string ToString()
        {

            string result = "// Objective function:\n";

            if (Type == OptimizationType.Min)
                result += "min: ";
            else
                result += "max: ";

            result += Objective.ToString() + ";\n";
            result += "// Constraints:\n";

            foreach (var t in Constraints)
                result += t + ";\n";

            return result;
            
        }
    }
}
