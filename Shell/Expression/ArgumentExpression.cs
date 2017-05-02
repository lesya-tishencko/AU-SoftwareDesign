using System;
using System.Collections.Generic;
using System.Linq;

namespace Shell
{
    /// <summary>
    /// For parsing of command's argument
    /// </summary>
    public class ArgumentExpression: Expression
    {
        public ArgumentExpression(IEnumerable<String> content) : base(content, Expression.EndlessContentsCount) { }

        /// <summary>
        /// Parses arguments
        /// </summary>
        public override IEnumerable<CommandLineObject> Interpret()
        {
            if (base.content.Count() == Expression.NoContents)
            {
                return null;
            }

            var args = new List<Argument>();

            foreach (String arg in base.content)
            {
                var argClean = "";
                if (arg.StartsWith("$"))
                {
                    argClean = arg.Trim('$', '"', '\'');
                    args.Add(ArgumentStorer.Find(argClean));
                    continue;
                }

                // We can check type of argument in future, but now we gas all of this like String
                argClean = arg.StartsWith(" ") ? arg.Remove(0, 1) : arg;
                argClean = argClean.Trim('\'', '"');
                args.Add(new Argument(argClean, TypeCode.String));
            }

            if (args.Count() == 0)
            {
                args = null;
            }

            return args;
        }
    }
}
