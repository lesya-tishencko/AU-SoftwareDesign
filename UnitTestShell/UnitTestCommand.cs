using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shell;
using System.IO;

namespace UnitTestShell
{
    [TestClass]
    public class UnitTestCommand
    {
        [TestMethod]
        public void TestMethodPwd()
        {
            PwdCommand pwd = new PwdCommand();
            pwd.Execute();
            Assert.AreEqual(ArgumentStorer.FindArgument("pwd").Content, Directory.GetCurrentDirectory());
        }

        [TestMethod]
        public void TestMethodCat()
        {
            CatCommand cat = new CatCommand();
            cat.AddArgument(new Argument("example.txt", TypeCode.String));
            cat.AddArgument(new Argument("synmaster.txt", TypeCode.String));
            cat.Execute();
            String result = new StreamReader("example.txt").ReadToEnd() + new StreamReader("synmaster.txt").ReadToEnd();
            Assert.AreEqual(ArgumentStorer.FindArgument("cat").Content, result);

            cat.AddArgument(new Argument("1.txt", TypeCode.String));
            cat.Execute();
            Assert.IsTrue(ArgumentStorer.FindArgument("cat").Content.StartsWith("Error "));
        }

        [TestMethod]
        public void TestMethodWc()
        {
            WcCommand wc = new WcCommand();
            wc.AddArgument(new Argument("example.txt", TypeCode.String));
            wc.Execute();
            String text = new StreamReader("example.txt").ReadToEnd();
            String result = text.Split('\n').Length.ToString() + " " + text.Split(' ', '\n').Length.ToString() + " " + new FileInfo("example.txt").Length;
            Assert.AreEqual(ArgumentStorer.FindArgument("wc").Content, result);

            wc.AddArgument(new Argument("123", TypeCode.String));
            wc.Execute();
            Assert.AreEqual(ArgumentStorer.FindArgument("wc").Content, "1 1 3");
        }

        [TestMethod]
        public void TestMethodEcho()
        {
            EchoCommand echo = new EchoCommand();
            echo.AddArgument(new Argument("Hello, World", TypeCode.String));
            echo.AddArgument(new Argument("!!!", TypeCode.String));
            echo.Execute();
            Assert.AreEqual(ArgumentStorer.FindArgument("echo").Content, "Hello, World!!!");

            echo.Execute();
            Assert.IsTrue(ArgumentStorer.FindArgument("echo").Content.StartsWith("Error "));
        }
    }
}
