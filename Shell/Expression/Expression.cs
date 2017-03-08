using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    /// <summary>
    /// Represents entity for parsing to command
    /// </summary>
    public class Expression
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

        /// <summary>
        /// Tries to parse expression to argument
        /// </summary>
        protected virtual IEnumerable<CommandLineObject> ParseArgumentExpression(string input)
        {
            IEnumerable<String> tokens = null;
            if (input.StartsWith("\"") || input.StartsWith("'"))
                tokens = input.Split('\'', '"').Where(str => str != "" && str != " ");
            else
                tokens = input.Split(' ').Where(str => str != "" && str != " ");
            return new ArgumentExpression(tokens).Interpret();
        }

        /// <summary>
        /// Tries to parse expression to command with parameters
        /// </summary>
        protected virtual IEnumerable<CommandLineObject> ParseСommandExpression(String input)
        {
            input = input.TrimStart().TrimEnd();
            int indexDelim = input.IndexOf(' ');
            List<String> tokens = null;
            int count = 1;
            if (indexDelim == -1) 
                /* Parse command without args */
                tokens = new List<string> { input.TrimStart('\'', '"').TrimEnd('\'', '"') };
            else {
                /* Parse command with args */
                tokens = new List<string> { input.Substring(0, indexDelim).TrimStart('\'', '"').TrimEnd('\'', '"'), input.Substring(indexDelim + 1)};
                count = 2;
            }
            return new LiteralExpression(tokens, count).Interpret();
        }

        /// <summary>
        /// Tries to parse expression with pipe
        /// </summary>
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

        /// <summary>
        /// Tries to parse expression with assigment (has two different type of results)
        /// </summary>
        protected virtual IEnumerable<CommandLineObject> ParseAssigmentExpression(String input)
        {
            int indexDelim = input.IndexOf('=');
            IEnumerable<CommandLineObject> comlinObj = null;
            if (indexDelim != -1)
            {
                String key = input.Substring(0, indexDelim).TrimStart(' ', '"', '\'').TrimEnd(' ', '"', '\'');
                String value = input.Substring(indexDelim + 1).TrimStart(' ', '"', '\'').TrimEnd('"', ' ', '\'');

                comlinObj = (new AssigmentExpression(new List<String> { key, value })).Interpret();
            }

            return comlinObj; 
        }

        /// <summary>
        /// Parses string to Command
        /// </summary>
        public virtual IEnumerable<CommandLineObject> Interpret()
        {
            if (content.Count() != contentCount)
                return null;

            String input = content.First().TrimStart().TrimEnd();
            /* Try to get pipe's command */
            IEnumerable<CommandLineObject> command = ParseSequenceExpression(input);
            if (command == null)
            {
                /* Try to parse assigment */
                command = ParseAssigmentExpression(input);
                if (command != null)
                    return null;
                else
                    /* Get command */
                    command = ParseСommandExpression(input);
            }

            if (command.Count() != 1 || !command.First().isCommand)
                return null;

            return command;
        }
    }
}
