using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerialPortUtility.Command;
using SerialPortUtility.Services.Stubs;

namespace SerialPortUtility.ViewModels.Tests
{
    [TestClass]
    public class ConsoleViewModelTest
    {
        [TestMethod]
        public async Task NewSession()
        {
            IConsoleViewModel consoleViewModel = new ConsoleViewModel(
                new StubConsoleService(),
                new StubSerialPortService(), 
                new StubSettingsService(), 
                new StubClipboardService(), 
                new StubDialogService(),
                new StubApplicationServices(),
                new StubWindowService(),
                new StubPrinterService(),
                new StubTaskHelpers());

            consoleViewModel.Initialize();

            StubWindowService.StubDialogResult = true;

            var newSessionCommand = consoleViewModel.NewSessionCommand
                .AsAsyncRelayCommand();

            await newSessionCommand.ExecuteAsync();

            Assert.IsTrue(StubWindowService.StubWindow == "SessionSetupWindow");
            Assert.IsTrue(consoleViewModel.IsSessionOpen);

            var endSessionCommand = consoleViewModel.EndSessionCommand;
            endSessionCommand.Execute(null);

            Assert.IsFalse(consoleViewModel.IsSessionOpen);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task NewSessionAndCancel()
        {
            IConsoleViewModel consoleViewModel = new ConsoleViewModel(
                new StubConsoleService(),
                new StubSerialPortService(),
                new StubSettingsService(),
                new StubClipboardService(),
                new StubDialogService(),
                new StubApplicationServices(),
                new StubWindowService(),
                new StubPrinterService(),
                new StubTaskHelpers());

            consoleViewModel.Initialize();

            StubWindowService.StubDialogResult = false;

            var newsSessionCommand = consoleViewModel.NewSessionCommand
                .AsAsyncRelayCommand();

            await newsSessionCommand.ExecuteAsync();

            Assert.IsTrue(StubWindowService.StubWindow == "SessionSetupWindow");
            Assert.IsFalse(consoleViewModel.IsSessionOpen);

            var endSessionCommand = consoleViewModel.EndSessionCommand;
            endSessionCommand.Execute(null);
        }
    }
}
