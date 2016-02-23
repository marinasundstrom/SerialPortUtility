namespace SerialPortUtility.Services
{
    public interface IWindow
    {
        string Title { get; set; }

        bool IsActive { get; }

        bool IsMaximized { get; set; }

        bool IsMinimized { get; set; }

        bool IsNormal { get; }
    }
}