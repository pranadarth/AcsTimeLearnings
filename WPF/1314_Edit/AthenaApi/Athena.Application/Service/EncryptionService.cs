using Athena.Application.Interface;
using Athena.Domain.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service
{
    public class EncryptionService : IEncryptionService
    {
        private readonly AppSettings _appSettings;
        public EncryptionService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        private SymmetricAlgorithm _encodeMethod;
        public string Encrypt(string textToEncrypt)
        {
            _encodeMethod = new TripleDESCryptoServiceProvider();

            if (string.IsNullOrEmpty(textToEncrypt))
            {
                return null;
            }

            byte[] array = new byte[3];
            byte[] bytes = Encoding.ASCII.GetBytes(textToEncrypt);
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(bytes, 0, bytes.Length);
            memoryStream.Position = 0L;
            MemoryStream memoryStream2 = new MemoryStream();
            _encodeMethod.Key = GetValidKey(_appSettings.TripleDESKey);
            _encodeMethod.IV = GetValidIV(_appSettings.TripleDESKey, _encodeMethod.IV.Length);
            CryptoStream cryptoStream = new CryptoStream(memoryStream2, _encodeMethod.CreateEncryptor(), CryptoStreamMode.Write);
            long length = memoryStream.Length;
            int num;
            for (int i = 0; i < length; i += num)
            {
                num = memoryStream.Read(array, 0, array.Length);
                cryptoStream.Write(array, 0, num);
            }

            cryptoStream.Close();
            byte[] inArray = memoryStream2.ToArray();
            return Convert.ToBase64String(inArray);
        }

        public string Decrypt(string textToDecrypt)
        {

            if (string.IsNullOrEmpty(textToDecrypt))
            {
                return null;
            }

            _encodeMethod = new TripleDESCryptoServiceProvider();

            byte[] array = new byte[3];
            byte[] buffer = Convert.FromBase64String(textToDecrypt);
            MemoryStream memoryStream = new MemoryStream(buffer);
            MemoryStream memoryStream2 = new MemoryStream();
            _encodeMethod.Key = GetValidKey(_appSettings.TripleDESKey);
            _encodeMethod.IV = GetValidIV(_appSettings.TripleDESKey, _encodeMethod.IV.Length);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, _encodeMethod.CreateDecryptor(), CryptoStreamMode.Read);
            long length = memoryStream.Length;
            int num;
            for (int i = 0; i < length; i += num)
            {
                num = cryptoStream.Read(array, 0, array.Length);
                if (num == 0)
                {
                    break;
                }

                memoryStream2.Write(array, 0, num);
            }

            cryptoStream.Close();
            byte[] bytes = memoryStream2.ToArray();
            ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
            return aSCIIEncoding.GetString(bytes);
        }

        private byte[] GetValidKey(string Key)
        {
            string s;
            if (_encodeMethod.LegalKeySizes.Length > 0)
            {
                int i;
                for (i = _encodeMethod.LegalKeySizes[0].MinSize; Key.Length * 8 > i; i += _encodeMethod.LegalKeySizes[0].SkipSize)
                {
                    if (_encodeMethod.LegalKeySizes[0].SkipSize <= 0)
                    {
                        break;
                    }

                    if (i >= _encodeMethod.LegalKeySizes[0].MaxSize)
                    {
                        break;
                    }
                }

                s = ((Key.Length * 8 <= i) ? Key.PadRight(i / 8, ' ') : Key.Substring(0, i / 8));
            }
            else
            {
                s = Key;
            }

            return Encoding.ASCII.GetBytes(s);
        }

        private static byte[] GetValidIV(string InitVector, int ValidLength)
        {
            if (InitVector.Length > ValidLength)
            {
                return Encoding.ASCII.GetBytes(InitVector.Substring(0, ValidLength));
            }

            return Encoding.ASCII.GetBytes(InitVector.PadRight(ValidLength, ' '));
        }
    }
}
