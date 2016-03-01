using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using SerialPortUtility.Validation;

namespace SerialPortUtility.ViewModels
{
    /// <summary>
    /// Based on code from http://blog.micic.ch/net/easy-mvvm-example-with-inotifypropertychanged-and-inotifydataerrorinfo
    /// with small modifications: Added CallerMemberNameAttribute. Changed access modifiers.
    /// </summary>
    public abstract class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase, IViewModel
    {
        #region Notify data error

        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public bool IsValid
        {
            get { return !HasErrors; }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        // get errors by property
        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                return _errors[propertyName];
            return null;
        }

        // has errors
        public bool HasErrors
        {
            get { return (_errors.Count > 0); }
        }

        // object is valid

        protected void AddError(string error, [CallerMemberName] string propertyName = null)
        {
            // Add error to list
            _errors[propertyName] = new List<string> {error};
            NotifyErrorsChanged(propertyName);

            RaisePropertyChanged("IsValid");
            RaisePropertyChanged("HasErrors");
        }

        protected void RemoveError([CallerMemberName] string propertyName = null)
        {
            // remove error
            if (_errors.ContainsKey(propertyName))
                _errors.Remove(propertyName);
            NotifyErrorsChanged(propertyName);

            RaisePropertyChanged("IsValid");
            RaisePropertyChanged("HasErrors");
        }

        protected void NotifyErrorsChanged(string propertyName)
        {
            // Notify
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion

        protected void ValidateProperty([CallerMemberName] string propertyName = null)
        {
            var value = GetType().GetProperty(propertyName).GetValue(this, null);
            var results = new List<ValidationResult>();
            
            RemoveError(propertyName);

            if (!DataAnnotationValidator.ValidateProperty(this, propertyName, value, out results))
            {
                foreach (var validationResult in results)
                {
                     AddError(
                         validationResult.ErrorMessage, 
                         propertyName);
                }               
            }
        }
    }
}