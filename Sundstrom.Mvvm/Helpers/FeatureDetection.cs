using System.Reflection;

namespace Sundstrom.Mvvm.Helpers
{
    /// <summary>
    ///     From MVVM Light PCL.
    /// </summary>
    internal static class FeatureDetection
    {
        private static bool? _isPrivateReflectionSupported;

        public static bool IsPrivateReflectionSupported
        {
            get
            {
                if (!_isPrivateReflectionSupported.HasValue)
                {
                    _isPrivateReflectionSupported = ResolveIsPrivateReflectionSupported();
                }
                return _isPrivateReflectionSupported.Value;
            }
        }

        private static bool ResolveIsPrivateReflectionSupported()
        {
            var inst = new ReflectionDetectionClass();
            try
            {
                MethodInfo method = typeof (ReflectionDetectionClass).GetMethod("Method", (BindingFlags) 36);
                method.Invoke(inst, null);
            }
            catch
            {
                return false;
            }
            return true;
        }

        #region Nested type: ReflectionDetectionClass

        private class ReflectionDetectionClass
        {
            private void Method()
            {
            }
        }

        #endregion
    }
}