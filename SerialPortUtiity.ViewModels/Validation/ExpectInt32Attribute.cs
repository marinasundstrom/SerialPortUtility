using System;
using System.ComponentModel.DataAnnotations;

namespace SerialPortUtility.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ExpectInt32Attribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return value is int ? null : new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
        }
    }
}