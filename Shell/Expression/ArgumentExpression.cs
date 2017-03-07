using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    /// <summary>
    /// For parsing of command's argument
    /// </summary>
    class ArgumentExpression: Expression
    {
        public ArgumentExpression(IEnumerable<String> content, int count = 2) : base(content, count) { }

        /// <summary>
        /// Parses arguments
        /// </summary>
        public override IEnumerable<CommandLineObject> Interpret()
        {
            if (base.content.Count() == Expression.NoContents)
                return null;

            List<Argument> args = new List<Argument>();

            /* Store named argument */
            if (base.contentCount == 2)
            {
                String key = base.content.First();
                String value = base.content.Last();

                ArgumentStorer.AddArgument(key, new Argument(value, TypeCode.String));
                args.Add(new Argument(value, TypeCode.String));
                return args;
            }

            foreach (String arg in base.content)
            {
                if (arg.StartsWith("$"))
                {
                    var argClean = arg.TrimStart('$', '"', '\'');
                    args.Add(ArgumentStorer.FindArgument(argClean));
                    continue;
                }

                // We can check type of argument in future, but now we gas all of this like String
                args.Add(new Argument(arg, TypeCode.String));
            }

            if (args.Count() == 0)
                args = null;

            return args;
        }
    }
}
