using System;
using System.ComponentModel.DataAnnotations;

namespace SerialPortUtility.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class NotEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var v = value as string;

            if (v != null)
            {
                if (string.IsNullOrEmpty(v) || string.IsNullOrWhiteSpace(v))
                    return new ValidationResult(ErrorMessage, new[] {validationContext.MemberName});
            }
            else if (value == null)
            {
                return new ValidationResult(ErrorMessage, new[] {validationContext.MemberName});
            }

            return null;
        }
    }
}