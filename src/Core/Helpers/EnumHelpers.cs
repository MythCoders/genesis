using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MC.eSIS.Core.Helpers
{
    public static class EnumHelpers
    {
        public static List<T> GetFlags<T>(T input) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an Enum type");
            }

            var value = (Enum)Convert.ChangeType(input, typeof(Enum));

            return Enum.GetValues(value.GetType()).Cast<Enum>().Where(value.HasFlag).Select(r => (T)Convert.ChangeType(r, typeof(T))).ToList();
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", nameof(description));
        }

        public static bool HaveCommonValue<T>(T enum1, T enum2) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an Enum type");
            }

            var value1 = (Enum)Convert.ChangeType(enum1, typeof(Enum));
            var value2 = (Enum)Convert.ChangeType(enum2, typeof(Enum));

            return Enum.GetValues(enum1.GetType()).Cast<Enum>().Any(value => value1.HasFlag(value) && value2.HasFlag(value));
        }

        private static T ConvertToEnum<T>(int enumValue)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type must be an enum");
            }
            return (T)Enum.ToObject(typeof(T), enumValue);
        }

        public static T ConvertToEnum<T>(string enumName, int enumValue) where T : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type must be an enum");
            }
            var type = Type.GetType($"ODPS.BMV.Dealers.Common.Contracts.DataContracts.{enumName}, ODPS.BMV.Dealers.Common.Contracts");
            if (typeof(T) != type)
            {
                throw new ArgumentException("Inconsistent enumName");
            }

            return ConvertToEnum<T>(enumValue);
        }

        public static T AddFlag<T>(this Enum type, T value)
        {
            try
            {
                return (T)(object)((int)(object)type | (int)(object)value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format("Could not add flag to Enum '{0}'.", typeof(T).Name), ex);
            }
        }

        public static T RemoveFlag<T>(this Enum type, T value)
        {
            try
            {
                return (T)(object)((int)(object)type & ~(int)(object)value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format("Could not remove flag to Enum '{0}'.", typeof(T).Name), ex);
            }
        }

        public static IEnumerable<T> MaskToList<T>(Enum mask)
        {
            if (typeof(T).IsSubclassOf(typeof(Enum)) == false)
            {
                throw new ArgumentException();
            }

            return Enum.GetValues(typeof(T)).Cast<Enum>().Where(mask.HasFlag).Cast<T>();
        }

        public static string GetDescription(this Enum value)
        {
            if (value == null) return string.Empty;
            var returnValue = string.Empty;

            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());
                if (fieldInfo != null)
                {
                    var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    returnValue = (attributes.Length > 0) ? attributes[0].Description : value.ToString();
                }
                else        //must be a flags enum with multiple values
                {
                    var type = value.GetType();
                    foreach (Enum item in Enum.GetValues(type))
                    {
                        if (((int)Enum.ToObject(type, item) != 0) && (value.HasFlag(item)))     //skip the None value
                        {
                            fieldInfo = type.GetField(item.ToString());
                            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                            if (!string.IsNullOrEmpty(returnValue))
                            {
                                returnValue += ", ";
                            }
                            returnValue += (attributes.Length > 0) ? attributes[0].Description : item.ToString();
                        }
                    }
                }
            }
            catch
            {
                returnValue = value.ToString();
            }

            return returnValue;
        }
    }
}
