using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerialPortUtility.Services;
using SerialPortUtility.Services.Stubs;

namespace SerialPortUtility.ViewModels.Tests
{
    [TestClass]
    public class SessionSetupViewModelTest
    {
        [TestMethod]
        public void MakeDefaultCommand()
        {
            ISessionSetupViewModel sessionSetupViewModel = new SessionSetupViewModel(
                new StubConsoleService(),
                new StubSettingsService(),
                new StubSerialPortService(),
                new ConsoleFormatService(), 
                new StubDialogService());

            sessionSetupViewModel.Initialize();

            sessionSetupViewModel.MakeDefaultCommand.Execute(null);
        }

        [TestMethod]
        public void StartCommand()
        {
            var dialogService = new StubDialogService();

            ISessionSetupViewModel sessionSetupViewModel = new SessionSetupViewModel(
                new StubConsoleService(),
                new StubSettingsService(),
                new StubSerialPortService(),
                new ConsoleFormatService(),
                dialogService);

            sessionSetupViewModel.Initialize();

            sessionSetupViewModel.StartCommand.Execute(null);

            Assert.IsFalse(dialogService.StubDialogShown);
            Assert.IsTrue(sessionSetupViewModel.IsValid);
        }

        [TestMethod]
        public void CancelCommand()
        {
            ISessionSetupViewModel sessionSetupViewModel = new SessionSetupViewModel(
                new StubConsoleService(),
                new StubSettingsService(),
                new StubSerialPortService(),
                new ConsoleFormatService(),
                new StubDialogService());

            sessionSetupViewModel.Initialize();

            sessionSetupViewModel.CancelCommand.Execute(null);
        }
    }
}