using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EventPlanner.Server.Extension_Methods
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
              .GetMember(enumValue.ToString())
              .First()
              .GetCustomAttribute<DisplayAttribute>()
              ?.GetName();
        }

        public static T GetValueFromName<T>(this string name) where T : Enum
        {
            Type type = typeof(T);

            foreach (FieldInfo field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute)
                {
                    if (attribute.Name == name)
                    {
                        return (T)field.GetValue(null);
                    }
                }

                if (field.Name == name)
                {
                    return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException($"{name} not found", nameof(name));
        }

        public static string GetDescription(this Enum enumValue)
        {
            string? description = enumValue.ToString();

            FieldInfo? fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                object[] attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);

                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }

        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentException($"{description} not found", nameof(description));
        }
    }
}
