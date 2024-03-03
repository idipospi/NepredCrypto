using System.Runtime.InteropServices;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities.Encoders;

namespace NepredCrypto
{
    class Kuznechik
    {
        [DllImport("kuznyechik.dll")]
        static extern void Kuznechik_Encrypt(byte[] key, byte[] buffer, int length);

        [DllImport("kuznyechik.dll")]
        static extern void Kuznechik_Decrypt(byte[] key, byte[] buffer, int length);

        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            if ((data.Length % 16) != 0)
            {
                int blocks_count = (data.Length / 16) + 1;
                Array.Resize(ref data, blocks_count * 16);
            }

            Kuznechik_Encrypt(key, data, data.Length);
            return data;
        }
        
        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            Kuznechik_Decrypt(key, data, data.Length);
            return data;
        }

        public static byte[] GetKey(string keyString)
        {
            // TODO: Добавить соль
            Console.WriteLine($"Пароль: {keyString}");
            MD5Digest keyDigest = new MD5Digest();
            keyDigest.BlockUpdate(System.Text.Encoding.UTF8.GetBytes(keyString), 0, keyString.Length);
            byte[] key = new byte[keyDigest.GetDigestSize()];
            keyDigest.DoFinal(key, 0);
            keyString = Hex.ToHexString(key);
            Console.WriteLine($"Ключ: {keyString}");
            Console.WriteLine(Program.separator);
            return key;
        }
    }
}