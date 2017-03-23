using System;
using System.Collections.Generic;
using System.Linq;

namespace Shell
{
    /// <summary>
    /// For parsing of named command
    /// </summary>
    public class LiteralExpression: Expression
    {
        public LiteralExpression(IEnumerable<String> content, int count): base(content, count) { }

        /// <summary>
        /// Parses named command with arguments
        /// </summary>
        public override IEnumerable<CommandLineObject> Interpret()
        {
            if (base.content.Count() != base.contentCount)
            {
                return null;
            }

            /* Get command (or create if it not found) */
            String name = base.content.First().TrimStart('$', ' ', '"');
            Command command = CommandStorer.Find(name);
            if (command == null)
            {
                command = new Command(name);
            }

            if (base.contentCount == 2)
            {
                /* Get list of arguments */
                String lastPart = base.content.Last();
                var args = ParseArgumentExpression(lastPart);
                foreach (Argument arg in args)
                    command.AddArgument(arg);
            }

            return new List<Command> { command };
        }
    }
}
