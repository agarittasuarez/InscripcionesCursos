using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;

namespace InscripcionesCursos
{
    public static class Cryptography
    {
        #region Constants

        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        static readonly string initVector = ConfigurationManager.AppSettings["InitVector"];
        static readonly string passCrypto = ConfigurationManager.AppSettings["CryptoPass"];

        #endregion

        #region Methods

        /// <summary>
        /// Method to encrypt a string
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="passPhrase"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = hashProvider.ComputeHash(utf8.GetBytes(passCrypto));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] dataToEncrypt = utf8.GetBytes(plainText.ToUpper());
            try
            {
                results = TDESAlgorithm.CreateEncryptor().TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                hashProvider.Clear();
            }
            return Convert.ToBase64String(results);
        }

        /// <summary>
        /// Metho to decrypt a string
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="passPhrase"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            byte[] results;
            System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
            System.Security.Cryptography.MD5CryptoServiceProvider hashProvider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] TDESKey = hashProvider.ComputeHash(utf8.GetBytes(passCrypto));
            System.Security.Cryptography.TripleDESCryptoServiceProvider TDESAlgorithm = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            cipherText = cipherText.Replace(" ", "+");
            if (cipherText.Length % 4 > 0)
                cipherText = cipherText.PadRight(cipherText.Length + 4 - cipherText.Length % 4, '=');
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = System.Security.Cryptography.CipherMode.ECB;
            TDESAlgorithm.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            byte[] dataToDecrypt = Convert.FromBase64String(cipherText);

            try
            {
                System.Security.Cryptography.ICryptoTransform decryptor = TDESAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                hashProvider.Clear();
            }
            return utf8.GetString(results);
        }

        #endregion
    }
}