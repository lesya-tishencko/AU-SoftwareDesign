using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    /// <summary>
    /// For parsing assigment
    /// </summary>
    class AssigmentExpression: Expression
    {
        public AssigmentExpression(IEnumerable<String> content): base(content, 2) { }

        /// <summary>
        /// Parses assigments
        /// </summary>
        public override IEnumerable<CommandLineObject> Interpret()
        {
            if (base.content.Count() != base.contentCount)
                return null;

            String key = base.content.First();
            String value = base.content.Last();

            /* Try to parse named command */
            IEnumerable<CommandLineObject> commandObj = base.ParseСommandExpression(value);
            /* Try to parse argument */
            if (CommandStorer.FindCommand((commandObj.First() as Command).Name) == null)
                ArgumentStorer.AddArgument(key, new Argument(value, TypeCode.String));
            else
                CommandStorer.AddCommand(key, commandObj.First() as Command);

            return commandObj;
        }
    }
}
