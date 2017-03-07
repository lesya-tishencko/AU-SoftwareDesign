using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class ArgumentExpression: Expression
    {
        public ArgumentExpression(IEnumerable<String> content, int count = 2) : base(content, count) { }

        public override IEnumerable<CommandLineObject> Interpret()
        {
            if (base.content.Count() == Expression.NoContents)
                return null;

            List<Argument> args = new List<Argument>();

            if (base.contentCount == 2)
            {
                String key = base.content.First().TrimStart().TrimEnd();
                String value = base.content.Last().TrimStart(' ', '"', '\'').TrimEnd('"', ' ', '\'');

                ArgumentStorer.AddArgument(key, new Argument(value, TypeCode.String));
                args.Add(new Argument(value, TypeCode.String));
                return args;
            }

            foreach (String arg in base.content)
            {
                var argClean = arg.TrimStart('"', ' ', '\'').TrimEnd('"', ' ', '\'');
                if (argClean.StartsWith("$"))
                {
                    argClean = argClean.TrimStart('$', '"', '\'');
                    args.Add(ArgumentStorer.FindArgument(argClean));
                    continue;
                }

                // we can check type of argument in future, but now we gas all of this like String
                args.Add(new Argument(argClean, TypeCode.String));
            }

            if (args.Count() == 0)
                args = null;

            return args;
        }
    }
}
