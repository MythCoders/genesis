using System.Text.RegularExpressions;

namespace eSIS.Core.Security
{
    public static class PasswordHelper
    {
        public static bool ValidatePasswordComplexity(string password)
        {
            return Regex.IsMatch(password, @"^(?=(.*\d){1})(?=.*[a-zA-Z])[0-9a-zA-Z!@#$%]{8,}");
        }
    }
}