using System.Security.Cryptography;

namespace CodePeace.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToFormat(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        public static string ToSha256(this string value)
        {
            var sha = SHA256Managed.Create();

            var hash = sha.ComputeHash(GetBytes(value));

            return GetString(hash);
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
