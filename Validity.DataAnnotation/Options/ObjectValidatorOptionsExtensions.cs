namespace Validity.DataAnnotation.Options;

public static class ObjectValidatorOptionsExtensions
{
    public static ObjectValidatorOptions UseMessageLocalization<TResourceSource>(this ObjectValidatorOptions options)
    {
        options.MessageResource = typeof(TResourceSource);

        return options;
    }

    public static ObjectValidatorOptions UseFieldNameLocalization<TResourceSource>(this ObjectValidatorOptions options)
    {
        options.FieldNameResource = typeof(TResourceSource);

        return options;
    }
}