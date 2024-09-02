using System;
using System.Linq;

namespace SQLPlanBiyori.Common;

public static class Extensions
{
    public static bool Between(this int value, int from, int to)
    {
        return value >= from && value <= to;
    }
    public static int GetCharCount(this string value, char searchChar)
    {
        if (string.IsNullOrEmpty(value))
        {
            return 0;
        }
        return value.Where(x => x == searchChar).Count();
    }

    public static int? ToIntN(this object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        int ret;
        return int.TryParse(value.ToString(), out ret) ? ret : null;
    }

    public static int ToInt32(this object value, int defaultValue)
    {
        return value.ToIntN() ?? defaultValue;
    }

    public static DateTime? ToDateTime(this object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        if (value is DateTime)
        {
            return (DateTime)value;
        }
        if (value is DateTime?)
        {
            return (DateTime?)value;
        }
        return null;
    }

    public static DateTime? ToDateTime(this object value, string dateFormat)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        DateTime d;
        return DateTime.TryParseExact(value.ToString(), dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d) ? d : null;
    }

    public static decimal? ToDecimalN(this object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        decimal ret;
        return decimal.TryParse(value.ToString(), out ret) ? ret : null;
    }

    public static decimal ToDecimal(this object value, decimal defaultValue)
    {
        return value.ToDecimalN() ?? defaultValue;
    }
    public static float? ToFloatN(this object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        float ret;
        return float.TryParse(value.ToString(), out ret) ? ret : null;
    }

    public static float ToFloat(this object value, float defaultValue)
    {
        return value.ToFloatN() ?? defaultValue;
    }

    public static double? ToDoubleN(this object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        double ret;
        return double.TryParse(value.ToString(), out ret) ? ret : null;
    }

    public static double ToDouble(this object value, double defaultValue)
    {
        return value.ToDoubleN() ?? defaultValue;
    }
    public static long? ToLongN(this object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        long ret;
        return long.TryParse(value.ToString(), out ret) ? ret : null;
    }

    public static long ToLong(this object value, long defaultValue)
    {
        return value.ToLongN() ?? defaultValue;
    }
}
