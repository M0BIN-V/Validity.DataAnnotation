using System.Collections;
using System.Reflection;

namespace Validity;

internal static class ObjectTraverser
{
    public static HashSet<object> GetInnerObjects(object? obj)
    {
        HashSet<object> objects = [];

        TraverseObjectRecursive(obj, objects);

        return objects;
    }

    private static void TraverseObjectRecursive(object? obj, HashSet<object> objects)
    {
        if (obj == null || objects.Contains(obj)) return;

        var type = obj.GetType();

        if (type.IsPrimitive || obj is string) return;

        objects.Add(obj);

        if (obj is IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                TraverseObjectRecursive(item, objects);
            }
        }
        else
        {
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanRead)
                {
                    var value = property.GetValue(obj);
                    TraverseObjectRecursive(value, objects);
                }
            }

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = field.GetValue(obj);
                TraverseObjectRecursive(value, objects);
            }
        }
    }
}
