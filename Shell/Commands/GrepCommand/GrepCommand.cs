using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Shell
{
    /// <summary>
    /// Command for matching regex in files or texts
    /// </summary>
    public class GrepCommand: Command
    {
        public GrepCommand(): base("grep", 2) { }

        public override void CreateOutput()
        {
            base.output = "";

            if (base.args.Count() == Command.NoArgsCount)
            {
                CreateError("Неправильно заданное количество аргументов, требуется " + base.argsCount + ", получено " + base.args.Count());
                return;
            }

            if (args.First() == null || args.First().Type != TypeCode.String) {
                CreateError("Неправильно заданный первый аргумент");
                return;
            }

            String pattern = base.args.First().Content;
            pattern = additionStart + pattern;
            pattern += additionEnd;

            if (args.Last() == null || args.Last().Type != TypeCode.String)
            {
                CreateError("Неправильно заданный последний аргумент");
                return;
            }

            String content = "";
            /* We know that it may be not file, but just string.
               We can't create fileInfo with invalid name */
            if (!base.args.Last().Content.Contains('\n') && new FileInfo(base.args.Last().Content).Exists)
            {
                StreamReader file = new StreamReader(base.args.Last().Content);
                content = file.ReadToEnd();
                file.Close();
            }
            else {
                content = base.args.Last().Content;
            }

            List<String> text = content.Split('\n').ToList();
            Regex regex = new Regex(pattern, RegexOptions.None);
            HashSet<String> setOfMatch = new HashSet<String>();
            foreach (Match match in regex.Matches(content))
            {
                int startInd = content.LastIndexOf('\n', match.Index) + 1;
                int endInd = content.IndexOf('\n', match.Index) - 1;
                if (startInd == 0 && endInd == -2)
                {
                    setOfMatch.Add(content);
                }
                else {
                    int index = endInd != -2
                        ? text.IndexOf(content.Substring(startInd, endInd - startInd + 1))
                        : text.IndexOf(content.Substring(startInd));
                    for (int i = 0; i <= countPrintedString; i++)
                        setOfMatch.Add(text[index + i]);
                }
            }

            if (setOfMatch.Count == 0)
            {
                CreateError("Подходящее выражение заматчить не удалось");
            }
            else {
                base.output = String.Join("\n", setOfMatch);
                base.CreateOutput();
            }
        }

        public override void Execute()
        {
            CreateOutput();
            Console.WriteLine(base.output);
        }

        public String AdditionStart
        {
            get { return additionStart; }
            set { additionStart = value; }
        }

        public String AdditionEnd
        {
            get { return additionEnd; }
            set { additionEnd = value; }
        }

        public int CountPrintedString
        {
            get { return countPrintedString; }
            set { countPrintedString = value; }
        }

        private String additionStart = "";
        private String additionEnd = "";
        private int countPrintedString = 0;
    }
}
