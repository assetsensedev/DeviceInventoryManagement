using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DeviceInventory
{
    public class Encryption
    {
        static byte[] crc8_table = new byte[] {
                    0x00, 0x9B, 0xAD, 0x36, 0xC1, 0x5A, 0x6C, 0xF7,
            0x19, 0x82, 0xB4, 0x2F, 0xD8, 0x43, 0x75, 0xEE,
            0x32, 0xA9, 0x9F, 0x04, 0xF3, 0x68, 0x5E, 0xC5,
            0x2B, 0xB0, 0x86, 0x1D, 0xEA, 0x71, 0x47, 0xDC,
            0x64, 0xFF, 0xC9, 0x52, 0xA5, 0x3E, 0x08, 0x93,
            0x7D, 0xE6, 0xD0, 0x4B, 0xBC, 0x27, 0x11, 0x8A,
            0x56, 0xCD, 0xFB, 0x60, 0x97, 0x0C, 0x3A, 0xA1,
            0x4F, 0xD4, 0xE2, 0x79, 0x8E, 0x15, 0x23, 0xB8,
            0xC8, 0x53, 0x65, 0xFE, 0x09, 0x92, 0xA4, 0x3F,
            0xD1, 0x4A, 0x7C, 0xE7, 0x10, 0x8B, 0xBD, 0x26,
            0xFA, 0x61, 0x57, 0xCC, 0x3B, 0xA0, 0x96, 0x0D,
            0xE3, 0x78, 0x4E, 0xD5, 0x22, 0xB9, 0x8F, 0x14,
            0xAC, 0x37, 0x01, 0x9A, 0x6D, 0xF6, 0xC0, 0x5B,
            0xB5, 0x2E, 0x18, 0x83, 0x74, 0xEF, 0xD9, 0x42,
            0x9E, 0x05, 0x33, 0xA8, 0x5F, 0xC4, 0xF2, 0x69,
            0x87, 0x1C, 0x2A, 0xB1, 0x46, 0xDD, 0xEB, 0x70,
            0x92, 0x09, 0x3F, 0xA4, 0x53, 0xC8, 0xFE, 0x65,
            0x8B, 0x10, 0x26, 0xBD, 0x4A, 0xD1, 0xE7, 0x7C,
            0xA0, 0x3B, 0x0D, 0x96, 0x61, 0xFA, 0xCC, 0x57,
            0xB9, 0x22, 0x14, 0x8F, 0x78, 0xE3, 0xD5, 0x4E,
            0xF6, 0x6D, 0x5B, 0xC0, 0x37, 0xAC, 0x9A, 0x01,
            0xEF, 0x74, 0x42, 0xD9, 0x2E, 0xB5, 0x83, 0x18,
            0xC4, 0x5F, 0x69, 0xF2, 0x05, 0x9E, 0xA8, 0x33,
            0xDD, 0x46, 0x70, 0xEB, 0x1C, 0x87, 0xB1, 0x2A,
            0x5A, 0xC1, 0xF7, 0x6C, 0x9B, 0x00, 0x36, 0xAD,
            0x43, 0xD8, 0xEE, 0x75, 0x82, 0x19, 0x2F, 0xB4,
            0x68, 0xF3, 0xC5, 0x5E, 0xA9, 0x32, 0x04, 0x9F,
            0x71, 0xEA, 0xDC, 0x47, 0xB0, 0x2B, 0x1D, 0x86,
            0x3E, 0xA5, 0x93, 0x08, 0xFF, 0x64, 0x52, 0xC9,
            0x27, 0xBC, 0x8A, 0x11, 0xE6, 0x7D, 0x4B, 0xD0
                };
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

        public static string CRCCalculation(string key)
        {
            byte crc = 0;
            for (int i = 0; i < key.Length; i++)
            {
                try
                {
                    crc = crc8_table[crc ^ Convert.ToByte(key[i])];
                }
                catch
                {
                    crc = 0;
                }
            }
            crc = crc8_table[crc ^ 00];
            return key+crc.ToString("X2");
        }

        public static string CRCCalculation(string key, int length)
        {
            const byte CRC8_POLY = 0x9B;
            byte crc = 0xFF;
            for (int i = 0; i < length; i++)
            {
                crc ^= Convert.ToByte(key[i]);
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x80) != 0)
                    {
                        crc = (byte)((crc << Convert.ToByte(1)) ^ CRC8_POLY);
                    }
                    else
                    {
                        crc <<= Convert.ToByte(1);
                    }
                }
            }

            crc ^= 0x00;
            DeviceLogger.MainLogger.Debug("crc tabe sbyte " + crc.ToString("X2"));
            return key + crc.ToString("X2");
        }

    }
}
