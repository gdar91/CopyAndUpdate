using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CopyAndUpdate
{
    internal static class WithModule
    {
        internal static TObject Template<TObject>(
            TObject target,
            params (Expression memberSelector, object newValue)[] changes
        )
        {
            var actions = new List<Action<TObject>>();

            foreach (var (memberSelector, newValue) in changes)
            {
                if (!(
                    memberSelector.NodeType == ExpressionType.Lambda &&
                    memberSelector is LambdaExpression lambdaExpression &&
                    lambdaExpression.Body is MemberExpression body &&
                    body.NodeType == ExpressionType.MemberAccess &&
                    body.Expression.NodeType == ExpressionType.Parameter
                ))
                {
                    throw new Exception("The expression should be a lambda, " +
                        "accessing specific property or field of the target.");
                }

                switch (body.Member)
                {
                    case PropertyInfo propertyInfo:
                        HandlePropertyInfo(newValue, propertyInfo);
                        break;

                    case FieldInfo fieldInfo:
                        HandleFieldInfo(newValue, fieldInfo);
                        break;

                    default:
                        throw new Exception("Something has changed in the reflection library.");
                }
            }

            if (!actions.Any())
            {
                return target;
            }

            var targetClone = Clone(target);

            actions.ForEach(action => action(targetClone));

            return targetClone;


            void HandlePropertyInfo(object newValue, PropertyInfo propertyInfo)
            {
                if (propertyInfo.CanRead)
                {
                    var value = propertyInfo.GetValue(target);

                    if (object.Equals(newValue, value))
                    {
                        return;
                    }

                    if (!propertyInfo.CanWrite)
                    {
                        if (TryFindPropertyBackingField(target, propertyInfo, newValue,
                            out var fieldInfo))
                        {
                            actions.Add(clone => fieldInfo.SetValue(clone, newValue));
                            
                            return;
                        }
                        else
                        {
                            throw new Exception("Can not write to a property " +
                                "with no setter and no backing field.");
                        }
                    }
                }

                actions.Add(clone => propertyInfo.SetValue(clone, newValue));
            }

            void HandleFieldInfo(object newValue, FieldInfo fieldInfo)
            {
                if (!object.Equals(newValue, fieldInfo.GetValue(target)))
                {
                    actions.Add(clone => fieldInfo.SetValue(clone, newValue));
                }
            }
        }

        private static TObject Clone<TObject>(TObject target)
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            var methodInfo = typeof(object).GetMethod(nameof(MemberwiseClone), bindingFlags);
            var targetClone = (TObject) methodInfo.Invoke(target, default);

            return targetClone;
        }

        private static IEnumerable<FieldInfo> AllFields<TObject>()
        {
            var bindingFlags =
                BindingFlags.Instance |
                BindingFlags.DeclaredOnly |
                BindingFlags.Public |
                BindingFlags.NonPublic;

            var type = typeof(TObject);

            while (type != null)
            {
                foreach (var fieldInfo in type.GetFields(bindingFlags))
                {
                    yield return fieldInfo;
                }

                type = type.BaseType;
            }
        }


        private static Dictionary<PropertyInfo, FieldInfo> _propertyBackingFields
            = new Dictionary<PropertyInfo, FieldInfo>();

        private static bool TryFindPropertyBackingField<TObject>(
            TObject target,
            PropertyInfo propertyInfo,
            object newValue,
            out FieldInfo resultFieldInfo
        )
        {
            var foundFieldInfos = _propertyBackingFields.ContainsKey(propertyInfo)
                ? new[] { _propertyBackingFields[propertyInfo] }
                : new FieldInfo[0];

            var propertyType = propertyInfo.PropertyType;

            resultFieldInfo = foundFieldInfos.Concat(AllFields<TObject>())
                .FirstOrDefault(fieldInfo =>
                {
                    if (!fieldInfo.FieldType.IsAssignableFrom(propertyType))
                    {
                        return false;
                    }

                    var targetClone = Clone(target);

                    fieldInfo.SetValue(targetClone, newValue);

                    var testValue = propertyInfo.GetValue(targetClone);

                    return object.Equals(newValue, testValue);
                });
            
            _propertyBackingFields[propertyInfo] = resultFieldInfo;
            
            return resultFieldInfo != default;
        }
    }
}
