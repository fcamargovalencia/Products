namespace Products.Helpers
{
    using System;
    using System.Text.RegularExpressions;

    public class RegexUtilities
    {
        public static bool IsValidEmail(string email)
        {
            var expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                return (Regex.Replace(email, expresion, String.Empty).Length == 0);

            }
            else
            {
                return false;
            }
        }

    }
}
