using System.ComponentModel.DataAnnotations;

namespace Validity.DataAnnotation;

public static class NestedObjectValidator
{
    public static List<FieldValidationResult> Validate(object obj, string name)
    {
        var fieldValidationResults = new List<FieldValidationResult>();

        ObjectTraverser.TraverseObjectRecursive(obj, name, field =>
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(field.Value, new ValidationContext(field.Value), results, true);

            foreach (var result in results)
            {
                fieldValidationResults
                .Add(new(field.Name + "." + result.MemberNames.First(), result.ErrorMessage ?? ""));
            }
        });

        return fieldValidationResults;
    }
}
