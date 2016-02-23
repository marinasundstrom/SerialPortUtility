using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SerialPortUtility.Validation
{
    public static class DataAnnotationValidator
    {
        public static bool ValidateProperty(object obj, string propertyName, object value, out List<System.ComponentModel.DataAnnotations.ValidationResult> results)
        {
            results = new List<ValidationResult>(1);
            var result = Validator.TryValidateProperty(
                value,
                new ValidationContext(obj, null)
                {
                    MemberName = propertyName
                },
                results);
            return result;
        }
    }
}