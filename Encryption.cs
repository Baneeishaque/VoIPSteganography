using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace VoIPSteganography
{
    class Encryption
    {
        public readonly byte[] SECRET_KEY = Encoding.ASCII.GetBytes("This is the Secret Key Size 32 .");// Size should be power of 2
        public readonly byte[] SECRET_IV = Encoding.ASCII.GetBytes("INITIALIZATI VEC"); // Size Should be 16
        /*
        // For Testing
        public Encryption()
        {
            byte[] test = EncryptStringToBytes("Hello World", SECRET_KEY, SECRET_IV);
            MessageBox.Show(DecryptStringFromBytes(test, SECRET_KEY, SECRET_IV));
        }
        */


        public byte[] Encrypt(string message)
        {
            return EncryptStringToBytes(message, SECRET_KEY,SECRET_IV);
        }

        public string Decrypt(byte[] message)
        {
            return DecryptStringFromBytes(message,SECRET_KEY,SECRET_IV);
        }

        private byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream. 
            return encrypted;

        }

        private string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
