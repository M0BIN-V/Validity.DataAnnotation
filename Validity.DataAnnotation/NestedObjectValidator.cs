using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using static Validity.DataAnnotation.ObjectTraverser;

namespace Validity.DataAnnotation;

public static class NestedObjectValidator
{
    public static List<FieldValidationResult> Validate(object obj, string name)
    {
        List<FieldValidationResult> errors = [];

        ActionOnInnerProperties(obj, name, (property, value, fullName) =>
        {
            var validationAttributes = property.GetCustomAttributes<ValidationAttribute>();

            if (value is null) return;

            var context = new ValidationContext(value);

            foreach (var attribute in validationAttributes)
            {
                if (!attribute.IsValid(value))
                {
                    var displayName = property.GetCustomAttribute<DisplayAttribute>()?.Name ??
                    property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ??
                    property.Name;

                    var message = attribute.FormatErrorMessage(displayName);

                    errors.Add(new(fullName, message));
                }
            }
        });

        return errors;
    }

}
