namespace LpFileParser
{
    public class Token
    {
        protected string text = "";

        public void Add(char c)
        {
            text += c;
        }

        public override string ToString()
        {
            return text;
        }
    }
}
