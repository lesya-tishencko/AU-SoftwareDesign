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
        public ArgumentExpression(IEnumerable<String> content) : base(content, Expression.EndlessContentsCount) { }

        /// <summary>
        /// Parses arguments
        /// </summary>
        public override IEnumerable<CommandLineObject> Interpret()
        {
            if (base.content.Count() == Expression.NoContents)
                return null;

            List<Argument> args = new List<Argument>();

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
