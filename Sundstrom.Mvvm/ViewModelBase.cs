using System;
using System.Linq;
using System.Reflection;
using Sundstrom.Mvvm.Helpers;

namespace Sundstrom.Mvvm
{
    /// <summary>
    ///     Base class for ViewModels.
    /// </summary>
    public abstract class ViewModelBase : ObservableObject, IViewModel
    {
        #region From MVVM Light PCL

        private static bool? _isInDesignMode;

        /// <summary>
        ///     Is running in design mode?
        /// </summary>
        public bool IsInDesignMode
        {
            get { return IsInDesignModeStatic; }
        }

        /// <summary>
        ///     Is running in design mode?
        /// </summary>
        public static bool IsInDesignModeStatic
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                {
                    _isInDesignMode = IsInDesignModePortable();
                }
                return _isInDesignMode.Value;
            }
        }

        private static bool IsInDesignModeMetro()
        {
            bool result;
            try
            {
                Type dm = Type.GetType("Windows.ApplicationModel.DesignMode, Windows, ContentType=WindowsRuntime");
                PropertyInfo dme = dm.GetProperty("DesignModeEnabled", (BindingFlags) 24);
                result = (bool) dme.GetValue(null, null);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private static bool IsInDesignModeNet()
        {
            bool result;
            try
            {
                Type dm =
                    Type.GetType(
                        "System.ComponentModel.DesignerProperties, PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
                object dmp = dm.GetField("IsInDesignModeProperty", (BindingFlags) 24).GetValue(null);
                Type dpd =
                    Type.GetType(
                        "System.ComponentModel.DependencyPropertyDescriptor, WindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
                Type typeFe =
                    Type.GetType(
                        "System.Windows.FrameworkElement, PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
                MethodInfo[] fromPropertys = dpd.GetMethods((BindingFlags) 24);
                MethodInfo fromProperty =
                    fromPropertys.Single((MethodInfo mi) => mi.Name == "FromProperty" && mi.GetParameters().Length == 2);
                object descriptor = fromProperty.Invoke(null, new[]
                {
                    dmp,
                    typeFe
                });
                PropertyInfo metaProp = dpd.GetProperty("Metadata", (BindingFlags) 20);
                object metadata = metaProp.GetValue(descriptor, null);
                Type tPropMeta =
                    Type.GetType(
                        "System.Windows.PropertyMetadata, WindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
                PropertyInfo dvProp = tPropMeta.GetProperty("DefaultValue", (BindingFlags) 20);
                var dv = (bool) dvProp.GetValue(metadata, null);
                result = dv;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private static bool IsInDesignModePortable()
        {
            DesignerPlatformLibrary platform = DesignerLibrary.DetectedDesignerLibrary;
            if (platform == DesignerPlatformLibrary.WinRT)
            {
                return IsInDesignModeMetro();
            }
            if (platform == DesignerPlatformLibrary.Silverlight)
            {
                bool desMode = IsInDesignModeSilverlight();
                if (!desMode)
                {
                    desMode = IsInDesignModeNet();
                }
                return desMode;
            }
            return platform == DesignerPlatformLibrary.Net && IsInDesignModeNet();
        }

        private static bool IsInDesignModeSilverlight()
        {
            bool result;
            try
            {
                Type dm =
                    Type.GetType(
                        "System.ComponentModel.DesignerProperties, System.Windows, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e");
                PropertyInfo dme = dm.GetProperty("IsInDesignTool", (BindingFlags) 24);
                result = (bool) dme.GetValue(null, null);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        #endregion
    }
}