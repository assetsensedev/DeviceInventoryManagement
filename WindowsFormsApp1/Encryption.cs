using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Encryption
    {
        public static byte[] EncryptAesManaged(string data)
        {
            try
            {
                // Create Aes that generates a new key and initialization vector (IV).
                // Same key must be used in encryption and decryption
                using (AesManaged aes = new AesManaged())
                {
                    // Encrypt string
                    byte[] encrypted = Encrypt(data, aes.Key, aes.IV);
                    // Print encrypted string
                    Console.WriteLine($"Encrypted data: {System.Text.Encoding.UTF8.GetString(encrypted)}");
                    return encrypted;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            return null;
        }

        public static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream
                    // to encrypt
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data
            return encrypted;
        }

        public static string EncryptDataWithAes(string plainText,  out string vectorBase64)
        {
            string key = "CA604C04B30818138BF0DB283C6F468C93571608EC6903487D8D68BA9FE84F87";
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(key);
                aesAlgorithm.GenerateIV();
                Console.WriteLine($"Aes Cipher Mode : {aesAlgorithm.Mode}");
                Console.WriteLine($"Aes Padding Mode: {aesAlgorithm.Padding}");
                Console.WriteLine($"Aes Key Size : {aesAlgorithm.KeySize}");

                //set the parameters with out keyword
                vectorBase64 = Convert.ToBase64String(aesAlgorithm.IV);

                // Create encryptor object
                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                byte[] encryptedData;

                //Encryption will be done in a memory stream through a CryptoStream object
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        encryptedData = ms.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedData);
            }
        }

        public static string  EncryptHexString(string hexKey)
        {
            string key2 = "CA604C04B30818138BF0DB283C6F468C93571608EC6903487D8D68BA9FE84F87";
            string hexIpAddress = key2; // 10.1.2.72 => "0A010248"
            byte[] bytes = new byte[hexIpAddress.Length / 2];

            for (int i = 0; i < hexIpAddress.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(hexIpAddress.Substring(i, 2), 16);

            Aes aes = Aes.Create();
            aes.KeySize = 256;
            aes.Mode = CipherMode.ECB;
            aes.Key = bytes;
            aes.Padding = PaddingMode.None;
            //aes.IV = null;

            string key3 = hexKey;
            string hexIpAddress3 = key3; // 10.1.2.72 => "0A010248"
            byte[] bytes3 = new byte[hexIpAddress3.Length / 2];

            for (int i = 0; i < hexIpAddress3.Length; i += 2)
                bytes3[i / 2] = Convert.ToByte(hexIpAddress3.Substring(i, 2), 16);


            
            byte[] ciphertext;

            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            {
                byte[] plaintextBytes = bytes3;
                ciphertext = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
            }


            return BitConverter.ToString(ciphertext).Replace("-", "");
        }

    }
}
