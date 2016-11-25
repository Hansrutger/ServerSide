using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace RealBusinessPage.App_Start
{
    public class hiddenSecrets
    {
        
        public static string saltPassword(string password)
        {
            int length =password.Length;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        public static string hashPassword(string password)
        {
            MD5 md5 = MD5.Create();

            byte[] inpput = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inpput);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; ++i)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}

