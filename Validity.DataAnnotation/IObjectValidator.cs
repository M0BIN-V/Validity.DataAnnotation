namespace Validity.DataAnnotation;

public interface IObjectValidator
{
    public List<FieldValidationResult> Validate(object obj, string name);
