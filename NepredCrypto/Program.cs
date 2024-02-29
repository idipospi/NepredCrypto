using Org.BouncyCastle.Utilities.Encoders;
namespace NepredCrypto
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is will be crypto program. It will be added soon.");
            char[] keyChars = new char[]{'1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', 
            '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2'};
            byte[] key = System.Text.Encoding.UTF8.GetBytes(keyChars);
            string plaintext = "Hello, World!";
            byte[] dataToEnc = System.Text.Encoding.UTF8.GetBytes(plaintext);
            byte[] ciphertext = Kuznechik.Encrypt(dataToEnc, key);
            Console.WriteLine(Hex.ToHexString(ciphertext));
        }
    }
}

