namespace SerialPortUtility.Services.Stubs
{
    public class StubWindowService : IWindowService
    {
        public static bool StubDialogResult { get; set; }

        public static string StubWindow { get; set; }

        public void Show(string name, bool closeCurrent = false)
        {
            StubWindow = name;
        }

        public bool? ShowDialog(string name)
        {
            StubWindow = name;

            return StubDialogResult;
        }
    }
}