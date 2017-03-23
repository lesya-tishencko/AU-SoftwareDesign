using System;
using System.Collections.Generic;
using System.Linq;

namespace Shell
{
    /// <summary>
    /// For parsing assigment
    /// </summary>
    public class AssigmentExpression: Expression
    {
        public AssigmentExpression(IEnumerable<String> content): base(content, 2) { }

        /// <summary>
        /// Parses assigments
        /// </summary>
        public override IEnumerable<CommandLineObject> Interpret()
        {
            if (base.content.Count() != base.contentCount)
            {
                return null;
            }

            String key = base.content.First();
            String value = base.content.Last();

            /* Try to parse named command */
            IEnumerable<CommandLineObject> commandObj = base.ParseСommandExpression(value);
            /* Try to parse argument */
            if (CommandStorer.Find((commandObj.First() as Command).Name) == null)
            {
                CommandStorer.Delete(key);
                ArgumentStorer.Add(key, new Argument(value, TypeCode.String));
            }
            else {
                ArgumentStorer.Add(key, new Argument((commandObj.First() as Command).Name, TypeCode.String));
                CommandStorer.Add(key, commandObj.First() as Command);
            }

            return commandObj;
        }
    }
}
