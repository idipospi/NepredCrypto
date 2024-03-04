using Org.BouncyCastle.Utilities.Encoders;
namespace NepredCrypto
{
    class Program
    {
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
            string outPath = "";
            string keyString = "";
            string fileToEncPath = "";
            byte[] key = new byte[32];
            
            if (args.Length == 0)
            {
                // Help menu
                HelpMenu();
                needDo = false;
            }
            
            // Проверка чтобы один аргумент не задавался дважды
            HashSet<string> uniqueArguments = new HashSet<string>();
            foreach (string arg in args)
            {
                if (arg.StartsWith("-"))
                {
                    if (!uniqueArguments.Add(arg))
                    {
                        // Аргумент уже задан ранее
                        Console.WriteLine($"Аргумент {arg} задан дважды.");
                        return;
                    }
                }
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
                        if (encArg >= args.Length)
                        {
                            Console.WriteLine("Некоррекно задан аргумент " + args[i]);    
                            return;
                        }
                    }
                    if (args[i] == "-d" || args[i] == "--decrypt")
                    {
                        needDecrypt = true;
                        decArg = i + 1;
                        if (decArg >= args.Length)
                        {
                            Console.WriteLine("Некоррекно задан аргумент " + args[i]);    
                            return;
                        }
                    }
                    // Обязательный параметр
                    if (args[i] == "-k" || args[i] == "--key")
                    {
                        keySet = true;
                        keyArg = i + 1;
                        if (keyArg >= args.Length)
                        {
                            Console.WriteLine("Некоррекно задан аргумент " + args[i]);    
                            return;
                        }
                    }
                    if (args[i] == "-o" || args[i] == "--output")
                    {
                        outSet = true;
                        outArg = i + 1;
                        if (outArg >= args.Length)
                        {
                            Console.WriteLine("Некоррекно задан аргумент " + args[i]);    
                            return;
                        }
                    }
                }
                // FOR TESTING
                Console.WriteLine("Args count: " + args.Length);
                
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
                
                if (outSet)
                {
                    outPath = getAllString(args[outArg], args, outArg);
                    // Если путь - одно слово, то файл будет создан в текущей директории
                    if (outPath.Split(' ').Length == 1 && (!outPath.Contains('\\') && !outPath.Contains('/'))) 
                    {
                        outPath = Directory.GetCurrentDirectory() + "\\" + outPath;
                    }
                    // Создание файла по указанному пути
                    try
                    {
                        if (!File.Exists(outPath))
                            File.Create(outPath).Close();   
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Путь к выходному файлу некорректен");
                        return;
                    }
                    
                    Console.WriteLine($"Путь к выходному файлу: {outPath}");
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
                        keyString = getAllString(args[keyArg], args, keyArg);
                        key = Kuznechik.GetKey(keyString);
                    }
                    else
                    {
                        Console.WriteLine("Должен быть выбран хотя бы один режим - шифрования или расшифрования.");
                        return;
                    }
                }
                // TODO: Реализовать ввод параметров ширфования/расшифрования
                // if (needEncrypt)
                // {
                //     fileToEncPath = getAllString(args[encArg], args, encArg);
                //     Console.WriteLine($"Путь к входному файлу: {fileToEncPath}");
                //     byte[] dataToEnc = File.ReadAllBytes(fileToEncPath);
                //     byte[] ciphertext = Kuznechik.Encrypt(dataToEnc, key);
                //     File.WriteAllBytes(outPath, ciphertext);
                // }
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

        private static string getAllString(string path, string[] args, int arg)
        {
            try
            {
                if (arg >= args.Length) throw new Exception("Некорректно задан аргумент " + args[arg-1]);
                if(args[arg].StartsWith('-')) 
                    throw new Exception("Некорректно задан аргумент " + args[arg-1]);
                int k = 1;
                string result = path;
                if (arg + k >= args.Length || args[arg + k].StartsWith('-')) return result;
                while (true)
                { 
                    result += " " + args[arg + k];
                    k++;
                    if (arg + k >= args.Length || args[arg + k].StartsWith('-')) break;
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
                return null;
            }
        }
    }
}