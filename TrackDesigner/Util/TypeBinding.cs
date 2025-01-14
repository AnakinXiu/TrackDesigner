using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace TrackDesigner.Util;

/// <summary>
/// Provides a new way to define the property name by using lambda expressions.
/// </summary>
public static class TypedBinding
{
    public static string GetPropName(LambdaExpression expression)
    {
        // Get property name
        var memberExpression = GetMemberExpression(expression);
        var propertyInfo = memberExpression?.Member as PropertyInfo;
        return propertyInfo?.Name;
    }

    private static MemberExpression GetMemberExpression(LambdaExpression lambda)
    {
        var unaryExpression = lambda.Body as UnaryExpression;
        return (unaryExpression?.Operand ?? lambda.Body) as MemberExpression;
    }

    public static void Raise(this PropertyChangedEventHandler handler, object sender, string propertyName,
        params string[] propertyNames)
    {
        Raise(handler, sender, new[] { propertyName }.Concat(propertyNames));
    }

    private static void Raise(PropertyChangedEventHandler handler, object sender, IEnumerable<string> propertyNames)
    {
        if (handler == null)
        {
            return;
        }

        var setOfPropertyNames = propertyNames.ToArray();

        if (setOfPropertyNames.Any(string.IsNullOrEmpty))
        {
            throw new ArgumentNullException(nameof(propertyNames),
                @"One of the specified property names is null or empty.");
        }

        var args = setOfPropertyNames
            .Select(name => new PropertyChangedEventArgs(name));

        foreach (var arg in args)
            handler(sender, arg);
    }

    public static void Raise(this PropertyChangedEventHandler handler, object sender, Expression<Func<object>> prop)
    {
        var propertyName = GetPropName(prop);
        if (!string.IsNullOrEmpty(propertyName))
            Raise(handler, sender, propertyName);
    }

    public static bool RaiseIfChanged(this PropertyChangedEventHandler handler, object sender, ref bool? current,
        bool? value, string propertyName, params string[] propertyNames)
    {
        return RaiseIfChanged(handler, sender, ref current, value, NullableEquals,
            new[] { propertyName }.Concat(propertyNames));
    }

    public static bool RaiseIfChanged(this PropertyChangedEventHandler handler, object sender, ref string current,
        string value, string propertyName, params string[] propertyNames)
    {
        return RaiseIfChanged(handler, sender, ref current, value, (r, l) => r == l,
            new[] { propertyName }.Concat(propertyNames));
    }

    public static bool RaiseIfChanged<T>(this PropertyChangedEventHandler handler, object sender, ref T current,
        T value, string propertyName, params string[] propertyNames) where T : struct
    {
        return RaiseIfChanged(handler, sender, ref current, value, (r, l) => Equals(r, l),
            new[] { propertyName }.Concat(propertyNames));
    }

    public static bool RaiseIfChanged<T>(this PropertyChangedEventHandler handler, object sender, ref T current,
        T value, Func<T, T, bool> equal, string propertyName, params string[] propertyNames)
    {
        return RaiseIfChanged(handler, sender, ref current, value, equal, new[] { propertyName }.Concat(propertyNames));
    }

    public static bool NullableEqual<T>(T? left, T? right) where T : struct
    {
        if (left.HasValue != right.HasValue)
            return false;
        if (!left.HasValue && !right.HasValue)
            return true;

        return Equals(left.Value, right.Value);
    }

    private static bool RaiseIfChanged<T>(PropertyChangedEventHandler handler, object sender, ref T current, T value,
        Func<T, T, bool> equal, IEnumerable<string> propertyNames)
    {
        if (equal(current, value))
        {
            return false;
        }

        current = value;

        Raise(handler, sender, propertyNames);

        return true;
    }

    private static bool NullableEquals(bool? left, bool? right)
    {
        if (left.HasValue && right.HasValue)
            return Equals(left.Value, right.Value);

        return !left.HasValue && !right.HasValue;
    }
}