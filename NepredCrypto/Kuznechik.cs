using System;
using System.Runtime.InteropServices;

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
    }

}