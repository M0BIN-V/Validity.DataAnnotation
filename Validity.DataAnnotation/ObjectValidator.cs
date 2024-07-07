using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Validity.DataAnnotation.Options;
using static Validity.DataAnnotation.ObjectTraverser;

namespace Validity.DataAnnotation;

public class ObjectValidator
{
    readonly IOptions<ObjectValidatorOptions> _options;

    public ObjectValidator(IOptions<ObjectValidatorOptions> options)
    {
        _options = options;
    }

    public List<FieldValidationResult> Validate(object obj, string name)
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
                    if (_options.Value.MessageResource is not null)
                    {
                        LocalizeValidationAttribute(attribute);
                    }

                    string displayName = GetFieldDisplayName(property);

                    var message = attribute.FormatErrorMessage(displayName);

                    errors.Add(new(fullName, message));
                }
            }
        });

        return errors;
    }

    void LocalizeValidationAttribute(ValidationAttribute attribute)
    {
        attribute.ErrorMessageResourceType = _options.Value.MessageResource;
        attribute.ErrorMessageResourceName = attribute.GetType().Name[..^9];
    }

    string GetFieldDisplayName(PropertyInfo property)
    {
        if (_options.Value.FieldNameResource is { } nameResource &&
            nameResource.GetProperty(property.Name) is { } propertyInfo)
        {
            return ((string)propertyInfo.GetValue(null)!) ??
             throw new NullReferenceException("resource value is null");
        }
        else
        {
            return
                property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ??
                property.GetCustomAttribute<DisplayAttribute>()?.Name ??
                property.Name;
        }
    }
}
