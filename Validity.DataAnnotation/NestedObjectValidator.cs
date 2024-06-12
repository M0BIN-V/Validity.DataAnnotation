using System.ComponentModel.DataAnnotations;

namespace Validity.DataAnnotation;

public static class NestedObjectValidator
{
    public static List<ValidationResult> Validate(object obj)
    {
        var results = new List<ValidationResult>();

        foreach (var innerObj in ObjectTraverser.GetInnerObjects(obj))
        {
            Validator.TryValidateObject(innerObj, new ValidationContext(innerObj), results, true);
        }

        return results;
    }
}
