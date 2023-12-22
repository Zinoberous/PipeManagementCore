using System.Text;

namespace PipeManagementCore
{
    public static class Passwords
    {
        private static readonly Dictionary<string, string> _passwords = [];

        public static void Set(string key, string value)
        {
            _passwords[key] = value;
        }

        public static string Get(string key)
        {
            return _passwords[key];
        }

        public static string Encrypt(string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        public static string Decrypt(string value)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }
    }
}
