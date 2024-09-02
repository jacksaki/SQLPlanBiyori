using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Common;

[AttributeUsage(AttributeTargets.Field)]
public sealed class EnumTextAttribute : Attribute
{
    public EnumTextAttribute(string text)
    {
        Text = text;
    }
    public string Text
    {
        get;
        private set;
    }
}

public static class EnumExtension
{
    public static string GetEnumText(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        return fieldInfo.GetCustomAttribute<EnumTextAttribute>()?.Text ?? value?.ToString();
    }
}
