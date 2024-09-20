using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Interface
{
    public interface IEncryptionService
    {
        public string Encrypt(string textToEncrypt);
        public string Decrypt(string textToDecrypt);
    }
}
