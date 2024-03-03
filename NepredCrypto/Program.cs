using Org.BouncyCastle.Utilities.Encoders;
namespace NepredCrypto
{
    class Program
    {
        // TODO: Нужно добавить парсер для ввода входного файла и ключа и флаги для выбора режима шифрования/расшифрования
        public static string separator = "--------------------------------------------------------";
        static void Main(string[] args)
        {
            bool needDo = true;
            bool needHelp = false;
            bool needEncrypt = false;
            bool needDecrypt = false;
            bool keySet = false;
            bool outSet = false;
            int encArg = 0;
            int decArg = 0;
            int keyArg = 0;
            int outArg = 0;
            
            if (args.Length == 0)
            {
                // Help menu
                HelpMenu();
                needDo = false;
            }
            
            if (needDo)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    // FOR TESTING
                    Console.WriteLine($"Argument {i + 1}: {args[i]}");
                    if (args[i] == "-h" || args[i] == "--help") needHelp = true;
                    // Один из
                    if (args[i] == "-e" || args[i] == "--encrypt")
                    {
                        needEncrypt = true;
                        encArg = i + 1;
                    }
                    if (args[i] == "-d" || args[i] == "--decrypt")
                    {
                        needDecrypt = true;
                        decArg = i + 1;
                    }
                    // Обязательный параметр
                    if (args[i] == "-k" || args[i] == "--key")
                    {
                        keySet = true;
                        keyArg = i + 1;
                    }
                    if (args[i] == "-o" || args[i] == "--output")
                    {
                        outSet = true;
                        outArg = i + 1;
                    }
                }
                // FOR TESTING
                Console.WriteLine("Args count: " + args.Length);
                // Проверка чтобы один аргумент не задавался дважды!
                
                if (needHelp)
                {
                    HelpMenu();
                    return;
                }

                if (needDecrypt && needEncrypt)
                {
                    Console.WriteLine("Выберите один режим - шифрования или расшифрования.");
                    return;
                }

                if (!outSet)
                {
                    Console.WriteLine("Необходимо указать путь к выходноиму файлу -o или --output.");
                    return;
                }
                
                string fullPath = "";
                int k = 1;
                if (outSet)
                {
                    fullPath = args[outArg];
                    while (true)
                    { 
                        fullPath += " " + args[outArg + k];
                        k++;
                        if (outArg + k >= args.Length || args[outArg + k].StartsWith('-')) break;
                    }
                    Console.WriteLine($"Путь к выходному файлу: {fullPath}");
                }
                
                if (!keySet)
                {
                    Console.WriteLine("Ключ обязателен для шифрования/расшифрования." +
                    " Используйте -k или --key для задания ключа.");
                    return;
                }
                
                if (keySet)
                {
                    if (needEncrypt || needDecrypt)
                    {
                        // TODO: Сделать для ключа то же самое что и для пути к выходному файлу
                        string keyString = args[keyArg];
                        byte[] key = Kuznechik.GetKey(keyString);
                    }
                    else
                    {
                        Console.WriteLine("Должен быть выбран хотя бы один режим - шифрования или расшифрования.");
                        return;
                    }
                }
                // TODO: Реализовать ввод параметров ширфования/расшифрования
            }
            
                
            // Console.WriteLine(Program.separator);
            // // Ключ
            // string keyString = "password";
            // byte[] key = Kuznechik.GetKey(keyString);
            //
            // // Ввод данных
            // string plaintext = "Здесь могут находиться любые данные для шифрования";
            // Console.WriteLine($"Plaintext: {plaintext}");
            // Console.WriteLine(Program.separator);
            //
            // // Шифрование
            // byte[] dataToEnc = System.Text.Encoding.UTF8.GetBytes(plaintext);
            // byte[] ciphertext = Kuznechik.Encrypt(dataToEnc, key);
            // Console.WriteLine($"Encrypted: {Hex.ToHexString(ciphertext)}");
            //
            // // Расшифрование
            // byte[] dataToDec = Kuznechik.Decrypt(ciphertext, key);
            // Console.WriteLine($"Decrypted: {System.Text.Encoding.UTF8.GetString(dataToDec)}");
        }
        
        private static void HelpMenu()
        {
            Console.WriteLine("Меню помощи:");
            Console.WriteLine("-h, --help\tВывести список команд");
            Console.WriteLine("-e, --encrypt\tУказать путь к файлу для шифрования");
            Console.WriteLine("-d, --decrypt\tУказать путь к файлу для расшифрования");
            Console.WriteLine("-k, --key\tЗадать ключ");
            Console.WriteLine("-o, --output\tУказать путь к выходному файлу");
        }
    }
}