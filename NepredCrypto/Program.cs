using Org.BouncyCastle.Utilities.Encoders;
namespace NepredCrypto
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is will be crypto program. It will be added soon.");
            
            // Ключ
            string keyString = "helloitskeyphrase123qwertyasdfg!";
            char[] keyChars = keyString.ToCharArray();
            byte[] key = System.Text.Encoding.UTF8.GetBytes(keyChars);
            
            // Шифрование
            string plaintext = "Hello, this is secret data. It may be anything you want. Проверка русского текста.";
            Console.WriteLine($"Plaintext: {plaintext}");
            byte[] dataToEnc = System.Text.Encoding.UTF8.GetBytes(plaintext);
            byte[] ciphertext = Kuznechik.Encrypt(dataToEnc, key);
            Console.WriteLine($"Encrypted: {Hex.ToHexString(ciphertext)}");
            
            // Расшифрование
            byte[] dataToDec = Kuznechik.Decrypt(ciphertext, key);
            Console.WriteLine($"Decrypted: {System.Text.Encoding.UTF8.GetString(dataToDec)}");
        }
    }
}

