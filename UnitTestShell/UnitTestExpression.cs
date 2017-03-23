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

            string file = @"../../../example.txt";
            Command pipes = new Expression("cat " + file + " | wc").Interpret().First() as Command;
            pipes.Execute();
            String text = new StreamReader(file).ReadToEnd();
            String result = text.Split('\n').Length.ToString() + " " + text.Split(' ', '\n').Length.ToString() + " " + new FileInfo("example.txt").Length;
            Assert.AreEqual(result, ArgumentStorer.Find("wc").Content);

            pipes = new Expression("cat " + file + " | wc | echo 'Result of wc '").Interpret().First() as Command;
            pipes.Execute();
            Assert.AreEqual("Result of wc " + result, ArgumentStorer.Find("echo").Content);

            pipes = new Expression("cat 1.txt | wc").Interpret().First() as Command;
            pipes.Execute();
            Assert.IsTrue(ArgumentStorer.Find("cat").Content.StartsWith("Error "));

            pipes = new Expression("wc | echo").Interpret().First() as Command;
            pipes.Execute();
            Assert.IsTrue(ArgumentStorer.Find("wc").Content.StartsWith("Error "));
        }

        [TestMethod]
        public void TestMethodAssigments()
        {
            string file = @"../../../example.txt";
            var assigment = new Expression("File=\"" + file + "\"").Interpret();
            Command command = new Expression("cat $File").Interpret().First() as Command;
            command.Execute();
            String result = new StreamReader(file).ReadToEnd();
            Assert.AreEqual(result, ArgumentStorer.Find("cat").Content);

            command = new Expression("cat \"$File\"").Interpret().First() as Command;
            command.Execute();
            Assert.AreEqual(result, ArgumentStorer.Find("cat").Content);

            assigment = new Expression("com = cat $File").Interpret();
            command = new Expression("$com $File").Interpret().First() as Command;
            command.Execute();
            Assert.AreEqual(result + result, ArgumentStorer.Find("cat").Content);
        }

        [TestMethod]
        public void TestMethodUnknown()
        {
            Command unknown = new Expression("notepad").Interpret().First() as Command;
            unknown.Execute();
            Process[] notepad = Process.GetProcessesByName("notepad");
            Assert.IsNotNull(notepad[0]);
            notepad[0].Kill();

            string file = @"../../../example.txt";
            unknown = new Expression("notepad " + file).Interpret().First() as Command;
            unknown.Execute();
            notepad = Process.GetProcessesByName("notepad");
            Assert.IsNotNull(notepad[0]);
            notepad[0].Kill();

            unknown = new Expression("some_name").Interpret().First() as Command;
            unknown.Execute();
            Assert.IsTrue(ArgumentStorer.Find("some_name").Content.StartsWith("Error "));
        }

        [TestMethod]
        public void TestMethodQuote()
        {
            var assigment = new Expression("x=1").Interpret();
            Command command = new Expression("echo $x").Interpret().First() as Command;
            command.Execute();
            Assert.AreEqual("1", ArgumentStorer.Find("echo").Content);

            assigment = new Expression("x=echo").Interpret();
            command = new Expression("$x 1").Interpret().First() as Command;
            command.Execute();
            Assert.AreEqual("1", ArgumentStorer.Find("echo").Content);

            command = new Expression("$x $x").Interpret().First() as Command;
            command.Execute();
            Assert.AreEqual("echo", ArgumentStorer.Find("echo").Content);

            assigment = new Expression("x=1").Interpret();
            command = new Expression("echo \"123$x\"").Interpret().First() as Command;
            command.Execute();
            Assert.AreEqual("1231", ArgumentStorer.Find("echo").Content);
        }
    }
}
