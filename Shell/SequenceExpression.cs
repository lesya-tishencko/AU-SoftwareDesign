using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class SequenceExpression: Expression
    {
        public SequenceExpression(IEnumerable<String> content): base(content, 2) { }

        public override IEnumerable<CommandLineObject> Interpret()
        {
            if (base.content.Count() != base.contentCount)
                return null;

            String lastPart = base.content.Last().TrimStart().TrimEnd();
            IEnumerable<CommandLineObject> command = ParseСommandExpression(lastPart);

            if (command.Count() != 1 || !command.First().isCommand)
                return null;

            String firstPart = base.content.First().TrimStart().TrimEnd();
            IEnumerable<CommandLineObject> argument = ParseSequenceExpression(firstPart);
            if (argument == null)
                argument = ParseAssigmentExpression(firstPart);
            if (argument == null)
                argument = ParseСommandExpression(firstPart);

            if (argument.Count() != 1)
                return null;

            if (argument.First().isCommand)
            {
                (argument.First() as Command).CreateOutput();
                String output = ArgumentStorer.FindArgument((argument.First() as Command).Name).content;
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
