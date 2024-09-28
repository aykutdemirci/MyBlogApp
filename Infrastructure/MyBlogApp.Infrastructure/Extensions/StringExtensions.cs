using System.Security.Cryptography;
using System.Text;

namespace MyBlogApp.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToMD5(this string text)
        {
            byte[] btr = text.GetMD5Hash();
            StringBuilder sb = new();
            foreach (byte bt in btr)
            {
                sb.Append(bt.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        private static byte[] GetMD5Hash(this string input)
        {
            return MD5.HashData(Encoding.UTF8.GetBytes(input));
        }
    }
}
