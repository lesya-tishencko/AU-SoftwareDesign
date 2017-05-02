using System;
using System.Collections.Generic;
using System.Linq;

namespace Shell
{
    /// <summary>
    /// For parsing sequence of command connected by pipes
    /// </summary>
    public class SequenceExpression: Expression
    {
        public SequenceExpression(IEnumerable<String> content): base(content, 2) { }

        /// <summary>
        /// Parses command sequences to one main command 
        /// </summary>
        public override IEnumerable<CommandLineObject> Interpret()
        {
            if (base.content.Count() != base.contentCount)
            {
                return null;
            }

            /* Get main command */
            String lastPart = base.content.Last();
            IEnumerable<CommandLineObject> command = ParseСommandExpression(lastPart);

            if (command.Count() != 1 || !command.First().IsCommand)
            {
                return null;
            }

            var firstPart = base.content.First();
            /* Try to get pipe's command */
            IEnumerable<CommandLineObject> argument = ParseSequenceExpression(firstPart);
            /* Try to parse assigment */
            if (argument == null)
            {
                argument = ParseAssigmentExpression(firstPart);
            }
            /* Get command */
            if (argument == null)
            {
                argument = ParseСommandExpression(firstPart);
            }

            if (argument.Count() != 1)
            {
                return null;
            }

            /* If we got Command, create it's output for argement of main command */
            if (argument.First().IsCommand)
            {
                (argument.First() as Command).CreateOutput();
                /* Find output of concrete command in storer */
                var output = (ArgumentStorer.Find((argument.First() as Command).Name) as Argument).Content;

                /* Check execution of command, if we got error, we stop parsing */
                if (output.StartsWith("Error "))
                {
                    output.Replace("Error ", "");
                    return argument;
                }
                argument = new List<Argument> { new Argument(output, TypeCode.String) };
            }

            (command.First() as Command).AddArgument(argument.First() as Argument);
            return command;
        }
    }
}
