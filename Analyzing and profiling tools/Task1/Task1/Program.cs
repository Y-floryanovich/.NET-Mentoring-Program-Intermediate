using System;
using System.Security.Cryptography;

namespace Task1
{

    //This method(Array.CopyTo) supports the System.Collections.ICollection
    //interface. If implementing System.Collections.ICollection is not explicitly
    //required, use Copy to avoid an extra indirection.


    internal class Program
    {
        private static byte[] Salt = new byte[16] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        private const string Password = "Password!.";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var result = GeneratePasswordHashUsingSalt(Password, Salt);
            Console.WriteLine(result);
        }

        public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {
            var iterate = 10000;

            //Because MD5 or SHA1 are vulnerable to collisions, use SHA256 or higher for the Rfc2898DeriveBytes class.
            var hash = new Rfc2898DeriveBytes(passwordText, salt, iterate, HashAlgorithmName.SHA256).GetBytes(20);

            //Тип Span представляет непрерывную область памяти.
            //Цель данного типа - повысить производительность и эффективность использования памяти.
            //Span позволяет избежать дополнительных выделений памяти при операции с наборами данных. 
            Span<byte> hashBytes = stackalloc byte[36];

            // Array.Copy - static, CopyTo - instance method, better perfomance
            salt.CopyTo(hashBytes[0..16]);
            hash.CopyTo(hashBytes[16..36]);

            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;
        }
    }
}
