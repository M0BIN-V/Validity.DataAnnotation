using System.Collections;
using System.Reflection;

namespace Validity.DataAnnotation;

internal static class ObjectTraverser
{
    public static void ActionOnInnerProperties(object? obj, string name, Action<PropertyInfo, object?, string> action)
    {
        if (obj is null) return;

        var type = obj.GetType();

        if (type.IsPrimitive || obj is string) return;

        if (obj is IEnumerable enumerable)
        {
            var enumerableIndex = 0;
            foreach (var item in enumerable)
            {
                ActionOnInnerProperties(item, name + $"[{enumerableIndex++}]", action);
            }
        }
        else
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);

                var fullName = name + "." + property.Name;

                ActionOnInnerProperties(value, fullName, action);

                action(property, value, fullName);
            }
        }
    }

}
