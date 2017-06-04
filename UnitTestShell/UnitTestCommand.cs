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
            Assert.AreEqual( Directory.GetCurrentDirectory(), ArgumentStorer.Find("pwd").Content);
        }

        [TestMethod]
        public void TestMethodCat()
        {
            CatCommand cat = new CatCommand();
            string file1 = @"../../../example.txt";
            string file2 = @"../../../synmaster.txt";
            cat.AddArgument(new Argument(file1, TypeCode.String));
            cat.AddArgument(new Argument(file2, TypeCode.String));
            cat.Execute();
            String result = new StreamReader(file1).ReadToEnd() + new StreamReader(file2).ReadToEnd();
            Assert.AreEqual(result, ArgumentStorer.Find("cat").Content);

            cat.AddArgument(new Argument("1.txt", TypeCode.String));
            cat.Execute();
            Assert.IsTrue(ArgumentStorer.Find("cat").Content.StartsWith("Error "));
        }

        [TestMethod]
        public void TestMethodWc()
        {
            WcCommand wc = new WcCommand();
            string file = @"../../../example.txt";
            wc.AddArgument(new Argument(file, TypeCode.String));
            wc.Execute();
            String text = new StreamReader(file).ReadToEnd();
            String result = text.Split('\n').Length.ToString() + " " + text.Split(' ', '\n').Length.ToString() + " " + new FileInfo(file).Length;
            Assert.AreEqual(result, ArgumentStorer.Find("wc").Content);

            wc.AddArgument(new Argument("123", TypeCode.String));
            wc.Execute();
            Assert.AreEqual("1 1 3", ArgumentStorer.Find("wc").Content);
        }

        [TestMethod]
        public void TestMethodEcho()
        {
            EchoCommand echo = new EchoCommand();
            echo.AddArgument(new Argument("Hello, World", TypeCode.String));
            echo.AddArgument(new Argument("!!!", TypeCode.String));
            echo.Execute();
            Assert.AreEqual("Hello, World!!!", ArgumentStorer.Find("echo").Content);

            echo.Execute();
            Assert.IsTrue(ArgumentStorer.Find("echo").Content.StartsWith("Error "));
        }

        [TestMethod]
        public void TestMethodGrep()
        {
            GrepCommand grep = new GrepCommand();
            string file = @"../../../example.txt";
            grep.AddArgument(new Argument("\\w:\\(\\($", TypeCode.String));
            grep.AddArgument(new Argument(file, TypeCode.String));
            grep.Execute();
            Assert.AreEqual("To much cold outside:((", ArgumentStorer.Find("grep").Content);

            IGrepCommand iGrep = new IGrepCommand(grep);
            grep.AddArgument(new Argument("w", TypeCode.String));
            grep.AddArgument(new Argument(file, TypeCode.String));
            grep.Execute();
            Assert.AreEqual("Awful day\r\nWhy you are so serious?\r", ArgumentStorer.Find("grep").Content);

            iGrep = new IGrepCommand(grep);
            var wGrep = new WGrepCommand(iGrep);
            grep.AddArgument(new Argument("che", TypeCode.String));
            grep.AddArgument(new Argument(file, TypeCode.String));
            grep.Execute();
            Assert.IsTrue(ArgumentStorer.Find("grep").Content.EndsWith("не удалось"));
        }
    }
}
