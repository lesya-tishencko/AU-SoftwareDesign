using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class Expression
    {
        public static int EndlessContentsCount = -1;
        public static int NoContents = 0;

        protected IEnumerable<String> content;
        protected int contentCount;

        public Expression(String content)
        {
            this.content = new List<String> { content };
            contentCount = 1;
        }

        public Expression(IEnumerable<String> content = null, int count = 0)
        {
            this.content = content;
            contentCount = count;
        }

        protected virtual IEnumerable<CommandLineObject> ParseArgumentExpression(string input)
        {
            if (input.StartsWith("\""))
            {
                return new ArgumentExpression(input).Interpret();
            }
            
            return new ArgumentExpression(input.Split(' '), Expression.EndlessContentsCount).Interpret();
        }

        protected virtual IEnumerable<CommandLineObject> ParseСommandExpression(String input)
        {
            int indexDelim = input.IndexOf(' ');
            List<String> tokens = null;
            int count = 1;
            if (indexDelim == -1)
                tokens = new List<string> { input };
            else {
                tokens = new List<string> { input.Substring(0, indexDelim), input.Substring(indexDelim + 1) };
                count = 2;
            }
            return new LiteralExpression(tokens, count).Interpret();
        }

        protected virtual IEnumerable<CommandLineObject> ParseSequenceExpression(String input)
        {
            int indexDelim = input.LastIndexOf('|');
            IEnumerable<CommandLineObject> argument = null;
            if (indexDelim != -1)
            {
                List<String> tokens = new List<string> { input.Substring(0, indexDelim), input.Substring(indexDelim + 1) };
                argument = new SequenceExpression(tokens).Interpret();
            }

            return argument;
        }

        protected virtual IEnumerable<CommandLineObject> ParseAssigmentExpression(String input)
        {
            int indexDelim = input.IndexOf('=');
            IEnumerable<CommandLineObject> comlinObj = null;
            if (indexDelim != -1)
            {
                String key = input.Substring(0, indexDelim);
                String value = input.Substring(indexDelim + 1);
                Command command = CommandStorer.FindCommand(value);
                if (command == null)
                {
                    List<String> tokens = new List<string> { key, value };
                    comlinObj = new ArgumentExpression(tokens).Interpret();
                }
                else
                {
                    comlinObj = ParseСommandExpression(value);
                    CommandStorer.AddCommand(key, comlinObj.First() as Command);
                }
            }

            return comlinObj; 
        }

        public virtual IEnumerable<CommandLineObject> Interpret()
        {
            if (content.Count() != contentCount)
                return null;

            String input = content.First().TrimStart().TrimEnd();
            IEnumerable<CommandLineObject> command = ParseSequenceExpression(input);
            if (command == null)
            {
                command = ParseAssigmentExpression(input);
                if (command != null)
                    return null;
                else
                    command = ParseСommandExpression(input);
            }

            if (command.Count() != 1 || !command.First().isCommand)
                return null;

            return command;
        }
    }
}
