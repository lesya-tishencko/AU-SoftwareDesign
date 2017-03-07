using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class LiteralExpression: Expression
    {
        public LiteralExpression(IEnumerable<String> content, int count): base(content, count) { }

        public override IEnumerable<CommandLineObject> Interpret()
        {
            if (base.content.Count() != base.contentCount)
                return null;

            String name = base.content.First();
            Command command = CommandStorer.FindCommand(name);
            if (command == null)
                command = new Command(name);

            if (base.contentCount == 2)
            {
                String lastPart = base.content.Last().TrimStart().TrimEnd();
                var args = ParseArgumentExpression(lastPart);
                foreach (Argument arg in args)
                    command.AddArgument(arg);
            }

            return new List<Command> { command };
        }
    }
}
