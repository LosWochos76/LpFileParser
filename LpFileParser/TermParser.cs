using System;
using System.Linq;

namespace LpFileParser
{
    public class TermParser
    {
        private ParserState current_state = ParserState.None;
        private Token current_token = null;

        public Term Parse(string text)
        {
            Term term = new Term();

            text = CleanTerm(text);

            if (text.Contains(":"))
            {
                term.Name = text.Substring(0, text.IndexOf(':'));
                text = text.Substring(text.IndexOf(':') + 1);
            }
            
            current_state = ParserState.None;
            var desired_state = ParserState.None;
            current_token = null;

            for (int i = 0; i < text.Length; i++)
            {
                Char c = text[i];

                if (Char.IsNumber(c) || c == '.')
                {
                    desired_state = ParserState.Constant;
                    ParseToken(term, desired_state, c);
                }
                else if (IsOperator(c))
                {
                    desired_state = ParserState.Operator;
                    ParseToken(term, desired_state, c);
                }
                else
                {
                    desired_state = ParserState.Variable;
                    ParseToken(term, desired_state, c);
                }

                current_state = desired_state;
            }

            term.Tokens.Add(current_token);
            return term;
        }

        private void ParseToken(Term term, ParserState desired_state, char c)
        {
            if (HasStateChange(current_state, desired_state))
            {
                term.Tokens.Add(current_token);
                current_token = null;
            }

            if (current_token == null)
                current_token = GetNewToken(desired_state);

            current_token.Add(c);
            current_state = desired_state;
        }

        private string CleanTerm(string s)
        {
            return new string(s.ToCharArray().Where(c => (!Char.IsWhiteSpace(c) && c != ';')).ToArray());
        }

        private bool IsOperator(Char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '>' || c == '<' || c == '=';
        }

        private bool HasStateChange(ParserState current, ParserState desired)
        {
            if (desired == ParserState.Constant)
                return current_state == ParserState.Operator;
            else if (desired == ParserState.Operator)
                return current == ParserState.Variable || current == ParserState.Constant;
            else
                return current == ParserState.Constant || current == ParserState.Operator;
        }

        private Token GetNewToken(ParserState state)
        {
            if (state == ParserState.Constant)
                return new Constant();
            else if (state == ParserState.Operator)
                return new Operator();
            else
                return new Variable();
        }
    }
}
