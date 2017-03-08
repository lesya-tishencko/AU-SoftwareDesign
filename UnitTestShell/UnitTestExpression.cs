using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shell;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace UnitTestShell
{
    [TestClass]
    public class UnitTestExpression
    {
        public UnitTestExpression()
        {
            Shell.Shell.InitDictionary();
        }

        [TestMethod]
        public void TestMethodPipes()
        {
            Command pipes = new Expression("cat example.txt | wc").Interpret().First() as Command;
            pipes.Execute();
            String text = new StreamReader("example.txt").ReadToEnd();
            String result = text.Split('\n').Length.ToString() + " " + text.Split(' ', '\n').Length.ToString() + " " + new FileInfo("example.txt").Length;
            Assert.AreEqual(ArgumentStorer.FindArgument("wc").Content, result);

            pipes = new Expression("cat example.txt | wc | echo 'Result of wc '").Interpret().First() as Command;
            pipes.Execute();
            Assert.AreEqual(ArgumentStorer.FindArgument("echo").Content, "Result of wc " + result);

            pipes = new Expression("cat 1.txt | wc").Interpret().First() as Command;
            pipes.Execute();
            Assert.IsTrue(ArgumentStorer.FindArgument("cat").Content.StartsWith("Error "));

            pipes = new Expression("wc | echo").Interpret().First() as Command;
            pipes.Execute();
            Assert.IsTrue(ArgumentStorer.FindArgument("wc").Content.StartsWith("Error "));
        }

        [TestMethod]
        public void TestMethodAssigments()
        {
            var assigment = new Expression("File='example.txt'").Interpret();
            Command command = new Expression("cat $File").Interpret().First() as Command;
            command.Execute();
            String result = new StreamReader("example.txt").ReadToEnd();
            Assert.AreEqual(ArgumentStorer.FindArgument("cat").Content, result);

            command = new Expression("cat '$File'").Interpret().First() as Command;
            command.Execute();
            Assert.AreEqual(ArgumentStorer.FindArgument("cat").Content, result);

            assigment = new Expression("com = cat '$File'").Interpret();
            command = new Expression("'$com' '$File'").Interpret().First() as Command;
            command.Execute();
            Assert.AreEqual(ArgumentStorer.FindArgument("cat").Content, result + result);
        }

        [TestMethod]
        public void TestMethodUnknown()
        {
            Command unknown = new Expression("notepad").Interpret().First() as Command;
            unknown.Execute();
            Process[] notepad = Process.GetProcessesByName("notepad");
            Assert.IsNotNull(notepad[0]);
            notepad[0].Kill();

            unknown = new Expression("notepad example.txt").Interpret().First() as Command;
            unknown.Execute();
            notepad = Process.GetProcessesByName("notepad");
            Assert.IsNotNull(notepad[0]);
            notepad[0].Kill();

            unknown = new Expression("some_name").Interpret().First() as Command;
            unknown.Execute();
            Assert.IsTrue(ArgumentStorer.FindArgument("some_name").Content.StartsWith("Error "));
        }
    }
}
