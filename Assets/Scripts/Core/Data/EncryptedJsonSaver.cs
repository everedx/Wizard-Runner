using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Core.Data
{
    public class EncryptedJsonSaver<T> : JsonSaver<T> where T: IDataStore
    {
        const int k_InitializationVectorLength = 16;
        const int k_KeyLength = 32;
        static readonly byte[] s_Salt =
        {
            0x6b, 0xb0, 0xa1, 0x65, 0x08, 0xf8, 0xe6, 0xe8, 0x4d, 0x9e, 0x2f, 0x19, 0x97, 0xec, 0x0d, 0x6e,
            0xe7, 0xec, 0xe2, 0x0a, 0xd9, 0x47, 0xa7, 0x8d, 0xff, 0x3d, 0xe1, 0x65, 0x4f, 0x46, 0x00, 0x22
        };

        /// <summary>
		/// Get device bytes to prevent copying save file to different device
		/// </summary>
        static byte[] GetUniqueDeviceBytes()
        {
            byte[] deviceIdentifier = Encoding.ASCII.GetBytes(SystemInfo.deviceUniqueIdentifier);

            return deviceIdentifier;
        }
        public EncryptedJsonSaver(string filename): base(filename)
        {
        }

        protected override StreamWriter GetWriteStream()
        {
            var underlyingStream = new FileStream(m_Filename, FileMode.Create);

            var byteGenerator = new Rfc2898DeriveBytes(GetUniqueDeviceBytes(), s_Salt, 1000);
            var random = new RNGCryptoServiceProvider();
            byte[] key = byteGenerator.GetBytes(k_KeyLength);
            byte[] iv = new byte[k_InitializationVectorLength];
            random.GetBytes(iv);

            Rijndael rijndael = Rijndael.Create();
            rijndael.Key = key;
            rijndael.IV = iv;

            underlyingStream.Write(iv, 0, k_InitializationVectorLength);
            var encryptedStream = new CryptoStream(underlyingStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);

            return new StreamWriter(encryptedStream);
        }

        protected override StreamReader GetReadStream()
        {
            var underlyingStream = new FileStream(m_Filename, FileMode.Open);

            var byteGenerator = new Rfc2898DeriveBytes(GetUniqueDeviceBytes(), s_Salt, 1000);
            byte[] key = byteGenerator.GetBytes(k_KeyLength);
            byte[] iv = new byte[k_InitializationVectorLength];

            underlyingStream.Read(iv, 0, k_InitializationVectorLength);

            Rijndael rijndael = Rijndael.Create();
            rijndael.Key = key;
            rijndael.IV = iv;

            var encryptedStream = new CryptoStream(underlyingStream, rijndael.CreateDecryptor(), CryptoStreamMode.Read);

            return new StreamReader(encryptedStream);
        }
    }

}
