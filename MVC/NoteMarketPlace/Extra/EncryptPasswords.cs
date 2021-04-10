using System;
using System.Security.Cryptography;
using System.Text;


namespace NoteMarketPlace.Extra
{
    public class EncryptPasswords
    {
        public static string EncryptPasswordMd5(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] originalBytes = encoder.GetBytes(password);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
            var hashedPassword = BitConverter.ToString(encodedBytes).Replace("-", "");
            var newPassword = hashedPassword.ToLower();

            return newPassword;

        }
    }
}