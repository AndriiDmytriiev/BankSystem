namespace BankSystem.Common.Utils
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public static class CryptographyExtensions
    {
        public static string[] GenerateKey()
        {
            using (AesManaged aes = new AesManaged())
            {
                aes.GenerateKey();
                aes.GenerateIV();

                return new[] {Convert.ToBase64String(aes.Key), Convert.ToBase64String(aes.IV)};
            }
        }

        public static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Erstellen Sie ein neues AesManaged-Objekt.   
            using (AesManaged aes = new AesManaged())
            {
                // Verschlüsselung erstellen    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // MemoryStream erstellen   
                using (MemoryStream ms = new MemoryStream())
                {
                   /* Erstellen Sie einen Kryptostream mithilfe der Klasse `CryptoStream`. 
                    * Diese Klasse ist der Schlüssel zur Verschlüsselung und verschlüsselt 
                    * und entschlüsselt Daten aus beliebigen Streams.In diesem Fall übergeben 
                    * wir einen Speicherstream zur Verschlüsselung.*/
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Erstellen Sie einen StreamWriter und schreiben Sie Daten in einen Stream.    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }

            // Verschlüsselte Daten zurückgeben   
            return encrypted;
        }

        public static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            // Erstellen Sie AesManaged   
            using (AesManaged aes = new AesManaged())
            {
                // Erstelle einen Entschlüsseler   
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Erstelle die für die Entschlüsselung verwendeten Datenströme.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Kryptostream erstellen 
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Lesen Sie den Krypto-Stream   
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }

            return plaintext;
        }
    }
}


