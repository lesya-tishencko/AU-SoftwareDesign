using System;
using System.Collections.Generic;

namespace Shell
{
    /// <summary>
    /// Represents store for named commands
    /// </summary>
    public static class CommandStorer
    {
        private static IDictionary<String, Command> varDict = new Dictionary<String, Command>();

        public static void Add(String key, Command value)
        {
            if (varDict.ContainsKey(key))
                varDict[key] = value;
            else
                varDict.Add(new KeyValuePair<String, Command>(key, value));
        }

        public static Command Find(String key) => varDict.ContainsKey(key) ? varDict[key] : null;

        public static void Delete(String key)
        {
            varDict.Remove(key);
        }
    }
}
