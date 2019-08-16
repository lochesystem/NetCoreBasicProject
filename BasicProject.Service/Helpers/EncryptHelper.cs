using System;
using System.Security.Cryptography;
using System.Text;

namespace BasicProject.Service.Helpers
{
    public static class EncryptHelper
    {
        private const string key = "BasicProject2019";

        public static string EncryptMD5(string toEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.ANSIX923;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock
                (toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string DecryptMD5(string cipherString)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;

            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.ANSIX923;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock
                (toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
