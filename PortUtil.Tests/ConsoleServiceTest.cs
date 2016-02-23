using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerialPortUtility.Services;

namespace SerialPortUtility.Tests
{
    [TestClass]
    public class ConsoleServiceTest
    {
        [TestMethod]
        public void WriteLine()
        {
            var textBox = new TextBox();
            var consoleService = new ConsoleService();
            consoleService.SetTextBox(textBox);

            consoleService.WriteLine("Hello World");

            var text = consoleService.GetText();

            Assert.IsTrue(text == "Hello World\r");
        }

        [TestMethod]
        public void Write()
        {
            var textBox = new TextBox();
            var consoleService = new ConsoleService();
            consoleService.SetTextBox(textBox);

            consoleService.Write("Hello World");

            var text = consoleService.GetText();

            Assert.IsTrue(text == "Hello World");
        }

        [TestMethod]
        [Ignore]
        public async Task ReadLine()
        {
            var textBox = new TextBox();
            var consoleService = new ConsoleService();
            consoleService.SetTextBox(textBox);

            Task.Run(async () =>
            {
                var str = await consoleService.ReadLineAsync();

                Assert.IsTrue(str == "Foo");
            });
        }

        [TestMethod]
        [Ignore]
        public async Task ReadChar()
        {
            var textBox = new TextBox();
            var consoleService = new ConsoleService();
            consoleService.SetTextBox(textBox);

            Task.Run(async () => {
                var ch = await consoleService.ReadCharAsync();

                Assert.IsTrue(ch == 'a');
            });
        }
    }
}
