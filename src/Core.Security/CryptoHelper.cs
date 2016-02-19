using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MC.eSIS.Core.Security
{
    public static class CryptoHelper
    {
        public static readonly char[] AvailableCharacters = {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        /// <summary>
        /// Removed i, l, 1, O
        /// </summary>
        public static readonly char[] HumanReadableCharacters = {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M',
            'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm',
            'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '2', '3', '4', '5', '6', '7', '8', '9' };

        public static string GenerateIdentifier(int length, bool humanReadable = false)
        {
            var identifier = new char[length];
            var randomData = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomData);
            }

            var characterSet = humanReadable ? HumanReadableCharacters : AvailableCharacters;

            for (var idx = 0; idx < identifier.Length; idx++)
            {
                var pos = randomData[idx] % characterSet.Length;
                identifier[idx] = characterSet[pos];
            }

            return new string(identifier);
        }

        public static string GenerateRandomSalt()
        {
            const int minSaltSize = 8;
            const int maxSaltSize = 16;
            var random = new Random();
            var saltSize = random.Next(minSaltSize, maxSaltSize);
            var saltBytes = new byte[saltSize];

            var rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(saltBytes);

            var returnValue = Convert.ToBase64String(saltBytes);
            return returnValue;
        }

        public static byte[] GetHashedValue(string plainText, string salt)
        {
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new ArgumentNullException("salt");
            }

            //Algorigthm:
            //convert plain text and salt to byte arrays
            //combine the two arrays
            //hash it
            //convert hashed value to string

            var saltBytes = Encoding.UTF8.GetBytes(salt);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];
            Array.Copy(plainTextBytes, 0, plainTextWithSaltBytes, 0, plainTextBytes.Length);
            Array.Copy(saltBytes, 0, plainTextWithSaltBytes, plainTextBytes.Length, saltBytes.Length);

            var hashedBytes = new SHA512Managed().ComputeHash(plainTextWithSaltBytes);
            return hashedBytes;
        }

        public static byte[] Encrypt(string stringToEncrypt, string encryptionKey)
        {
            //Plain Text to be encrypted
            var plainText = Encoding.Unicode.GetBytes(stringToEncrypt);

            //In live, get the persistant passphrase from other isolated source
            //This example has hardcoded passphrase just for demo purpose
            var sb = new StringBuilder();
            sb.Append(encryptionKey);

            //Generate the Salt, with any custom logic and
            //using the above string
            var sbSalt = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                sbSalt.Append("," + sb.Length);
            }
            var salt = Encoding.ASCII.GetBytes(sbSalt.ToString());

            //Key generation:- default iterations is 1000 
            //and recomended is 10000
            var pwdGen = new Rfc2898DeriveBytes(sb.ToString(), salt, 10000);

            //The default key size for RijndaelManaged is 256 bits, 
            //while the default blocksize is 128 bits.
            var rijndaelManaged = new RijndaelManaged { BlockSize = 256 }; //Increased it to 256 bits- max and more secure

            var key = pwdGen.GetBytes(rijndaelManaged.KeySize / 8);   //This will generate a 256 bits key
            var iv = pwdGen.GetBytes(rijndaelManaged.BlockSize / 8);  //This will generate a 256 bits IV

            //On a given instance of Rfc2898DeriveBytes class,
            //GetBytes() will always return unique byte array.
            rijndaelManaged.Key = key;
            rijndaelManaged.IV = iv;

            //Now encrypt
            byte[] cipherText2;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plainText, 0, plainText.Length);
                }
                cipherText2 = ms.ToArray();
            }
            return cipherText2;
        }

        public static string Decrypt(byte[] cipherText2, string encryptionKey)
        {
            //In live, get the persistant passphrase from other isolated source
            //This example has hardcoded passphrase just for demo purpose, obtained by 
            //adding current user's firstname + DOB + email
            //You may generate this string with any logic you want.
            var sb = new StringBuilder();
            sb.Append(encryptionKey);

            //Generate the Salt, with any custom logic and
            //using the above string
            StringBuilder sbSalt = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                sbSalt.Append("," + sb.Length);
            }
            var salt = Encoding.ASCII.GetBytes(sbSalt.ToString());

            //Key generation:- default iterations is 1000 and recomended is 10000
            var pwdGen = new Rfc2898DeriveBytes(sb.ToString(), salt, 10000);

            //The default key size for RijndaelManaged is 256 bits,
            //while the default blocksize is 128 bits.
            var rijndaelManaged = new RijndaelManaged { BlockSize = 256 }; //Increase it to 256 bits- more secure

            var key = pwdGen.GetBytes(rijndaelManaged.KeySize / 8);   //This will generate a 256 bits key
            var iv = pwdGen.GetBytes(rijndaelManaged.BlockSize / 8);  //This will generate a 256 bits IV

            //On a given instance of Rfc2898DeriveBytes class,
            //GetBytes() will always return unique byte array.
            rijndaelManaged.Key = key;
            rijndaelManaged.IV = iv;

            //Now decrypt
            byte[] plainText2;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherText2, 0, cipherText2.Length);
                }
                plainText2 = ms.ToArray();
            }

            //Decrypted text
            return Encoding.Unicode.GetString(plainText2);
        }
    }
}