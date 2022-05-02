/*
 * Author: Gautam Sharma
 * Date: 05-05-2021
 * Desc: Methods to Encrypt and Decrypt variables, values, paths etc.
 */
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DiseaseMIS.BAL.Core
{
    public static class Encrypter
    {
        static readonly string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Methods to encrypt string
        /// </summary>
        /// <param name="strInputString">string to encrypt</param>
        /// <returns></returns>
        public static string EncryptString(string strInputString)
        {
            string strOutputString = "";
            try
            {
                byte[] clearBytes = Encoding.Unicode.GetBytes(strInputString);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                    encryptor.Key = rfc.GetBytes(32);
                    encryptor.IV = rfc.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        strOutputString = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return strOutputString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Methods to decrypt string
        /// </summary>
        /// <param name="strInputString">Encrypted string.</param>
        /// <returns></returns>
        public static string DecryptString(string strInputString)
        {
            string strOutputString = "";
            try
            {
                strInputString = strInputString.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(strInputString);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                    encryptor.Key = rfc.GetBytes(32);
                    encryptor.IV = rfc.GetBytes(16);
                    using MemoryStream ms = new MemoryStream();
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    strOutputString = Encoding.Unicode.GetString(ms.ToArray());
                }
                return strOutputString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
