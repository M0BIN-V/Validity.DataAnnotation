using System.Collections;
using System.Reflection;

namespace Validity.DataAnnotation;

internal static class ObjectTraverser
{
    public static void TraverseObjectRecursive(object? obj, string name, Action<Field> action)
    {
        if (obj == null) return;

        var type = obj.GetType();

        if (type.IsPrimitive || obj is string) return;

        action(new(name, obj));

        if (obj is IEnumerable enumerable)
        {
            var enumerableIndex = 0;
            foreach (var item in enumerable)
            {
                TraverseObjectRecursive(item, name + $"[{enumerableIndex++}]", action);
            }
        }
        else
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                TraverseObjectRecursive(value, name + "." + property.Name, action);
            }
        }
    }
}
