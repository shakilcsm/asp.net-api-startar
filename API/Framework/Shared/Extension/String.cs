using System.Security.Cryptography;
using System.Text;

namespace API.Framework.Shared.Extension;

public static class StringExtensions
{
    public static string ToMD5Hash(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
