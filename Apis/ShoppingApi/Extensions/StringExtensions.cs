using System;
using System.Collections.Generic;
using System.Text;

namespace StandardLibrary.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsEqualIgnoreCase(this string first, string second)
        {
            return string.Equals(first, second, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsEqualAnyIgnoreCase(this string first, params string[] comparisons)
        {
            foreach (var comparison in comparisons)
            {
                if (first.IsEqualIgnoreCase(comparison))
                    return true;
            }
            return false;
        }
    }
}
