using System;
using System.Linq;
using System.IO;

namespace Shell
{
    /// <summary>
    /// Command for calculating count of lines, words, bytes
    /// </summary>
    public class WcCommand: Command
    {
        public WcCommand(): base("wc", 1) { }

        /// <summary>
        /// Calculates count of lines, words, bytes in file(or string) and creates string-info
        /// </summary>
        public override void CreateOutput()
        {
            base.output = "";
            if (base.args.Count() != base.argsCount)
            {
                CreateError("Неправильно заданное количество аргументов, требуется " + base.argsCount + ", получено " + base.args.Count());
                return;
            }

            if (base.args.First() != null || base.args.First().Type == TypeCode.String)
            {
                String content = null;
                long bytes = 0;

                /* We know that it may be not file, but just string.
                We can't create fileInfo with invalid name */
                if (!base.args.First().Content.Contains('\n') && new FileInfo(base.args.First().Content).Exists)
                {
                    StreamReader file = new StreamReader(base.args.First().Content);
                    content = file.ReadToEnd();
                    bytes = new FileInfo(base.args.First().Content).Length;
                    file.Close();
                }
                else
                {
                    content = base.args.First().Content;
                    bytes = content.Length;
                }

                long lines = content.Split('\n').Count();
                long words = content.Split(' ', '\n').Count();

                base.output = lines.ToString() + " " + words.ToString() + " " + bytes.ToString();

                base.CreateOutput();
            }
            else {
                CreateError("Некорректный тип аргументов");
            }
        }

        /// <summary>
        /// Prints count of lines, words, bytes to console
        /// </summary>
        public override void Execute()
        {
            CreateOutput();
            Console.WriteLine(base.output);
        }
    }
}
