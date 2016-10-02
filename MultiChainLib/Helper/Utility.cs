using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MultiChainLib.Helper
{
    public static class Utility
    {
        public class HexadecimalEncoding
        {
            public static string ToHexString(string str)
            {
                var sb = new StringBuilder();

                var bytes = Encoding.UTF8.GetBytes(str);
                foreach (var t in bytes)
                {
                    sb.Append(t.ToString("X2"));
                }

                return sb.ToString(); // returns: "48656C6C6F20776F726C64" for "Hello world"
            }

            public static string FromHexString(string hexString)
            {
                var bytes = new byte[hexString.Length / 2];
                for (var i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                }

                return Encoding.UTF8.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
            }
        }

        public static string GetHash(Stream stream)
        {
            using (var md5 = MD5.Create())
            {
                return Encoding.UTF8.GetString(md5.ComputeHash(stream));
            }
        }

        public static string GetHash(string fileRelativePath)
        {
            try
            {
                string filePath = Path.Combine(HttpContext.Current.Server.MapPath(fileRelativePath));
                using (var stream = File.OpenRead(filePath))
                {
                    return GetHash(stream);
                }
            }
            catch
            {
                return string.Empty;
            }            
        }
    }
}
