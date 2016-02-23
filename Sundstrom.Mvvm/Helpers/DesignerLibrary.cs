using System;

namespace Sundstrom.Mvvm.Helpers
{
    /// <summary>
    ///     From MVVM Light PCL.
    /// </summary>
    internal static class DesignerLibrary
    {
        private static DesignerPlatformLibrary? _detectedDesignerPlatformLibrary;

        internal static DesignerPlatformLibrary DetectedDesignerLibrary
        {
            get
            {
                if (!_detectedDesignerPlatformLibrary.HasValue)
                {
                    _detectedDesignerPlatformLibrary = GetCurrentPlatform();
                }
                return _detectedDesignerPlatformLibrary.Value;
            }
        }

        private static DesignerPlatformLibrary GetCurrentPlatform()
        {
            Type dm =
                Type.GetType(
                    "System.ComponentModel.DesignerProperties, System.Windows, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e");
            if (dm != null)
            {
                return DesignerPlatformLibrary.Silverlight;
            }
            Type cmdm =
                Type.GetType(
                    "System.ComponentModel.DesignerProperties, PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
            if (cmdm != null)
            {
                return DesignerPlatformLibrary.Net;
            }
            Type wadm = Type.GetType("Windows.ApplicationModel.DesignMode, Windows, ContentType=WindowsRuntime");
            if (wadm != null)
            {
                return DesignerPlatformLibrary.WinRT;
            }
            return DesignerPlatformLibrary.Unknown;
        }
    }
}