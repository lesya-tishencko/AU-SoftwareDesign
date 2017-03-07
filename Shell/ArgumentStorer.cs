using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    static class ArgumentStorer
    {
        private static IDictionary<String, Argument> argumentsDict = new Dictionary<String, Argument>();

        public static void AddArgument(String key, Argument value)
        {
            if (argumentsDict.ContainsKey(key))
                argumentsDict[key] = value;
            else
                argumentsDict.Add(new KeyValuePair<String, Argument>(key, value));
        }

        public static Argument FindArgument(String key)
        {
            if (!argumentsDict.ContainsKey(key))
                return null;

            return argumentsDict[key];
        }
    }
}
