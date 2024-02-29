using Org.BouncyCastle.Utilities.Encoders;
namespace NepredCrypto
{
    class Program
    {
        // TODO: Нужно добавить парсер для ввода входного файла и ключа и флаги для выбора режима шифрования/расшифрования
        public static string separator = "--------------------------------------------------------";
        static void Main(string[] args)
        {
            Console.WriteLine(Program.separator);
            // Ключ
            string keyString = "password";
            byte[] key = Kuznechik.GetKey(keyString);
            
            // Ввод данных
            string plaintext = "Здесь могут находиться любые данные для шифрования";
            Console.WriteLine($"Plaintext: {plaintext}");
            Console.WriteLine(Program.separator);
            
            // Шифрование
            byte[] dataToEnc = System.Text.Encoding.UTF8.GetBytes(plaintext);
            byte[] ciphertext = Kuznechik.Encrypt(dataToEnc, key);
            Console.WriteLine($"Encrypted: {Hex.ToHexString(ciphertext)}");
            
            // Расшифрование
            byte[] dataToDec = Kuznechik.Decrypt(ciphertext, key);
            Console.WriteLine($"Decrypted: {System.Text.Encoding.UTF8.GetString(dataToDec)}");
        }
    }
}