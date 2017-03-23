using System;
using System.Collections.Generic;

namespace Shell
{
    /// <summary>
    /// Represents store of enviroment variables and named command
    /// </summary>
    public static class ArgumentStorer
    {
        private static IDictionary<String, Argument> varDict = new Dictionary<String, Argument>();

        public static void Add(String key, Argument value)
        {
            if (varDict.ContainsKey(key))
                varDict[key] = value;
            else
                varDict.Add(new KeyValuePair<String, Argument>(key, value));
        }

        public static Argument Find(String key) => varDict.ContainsKey(key) ? varDict[key] : null;
    }
}
