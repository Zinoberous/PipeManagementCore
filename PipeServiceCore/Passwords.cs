using System.Text;

namespace PipeServiceCore
{
    public static class Passwords
    {
        private static readonly Dictionary<string, string> _passwords = [];

        public static void Set(string key, string value)
        {
            _passwords[key] = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        public static string Get(string key)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(_passwords[key]));
        }
    }
}
