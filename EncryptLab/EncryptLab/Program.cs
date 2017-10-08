using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = Encrypt.Encryption("0000000100100011010001010110011110001001101010111100110111101111");//Encriptado

            var r = Convert.ToInt64(a, 2).ToString("X");//la salida en hexadecimal

            var b = Encrypt.Decrypt(a);//Se desencripta la salida de a

            var test = Convert.ToInt64(b, 2).ToString("X");//esto convierte la salida de bits a hexadecimal

            //EncryptManager.EncryptFile2("test.png");

            EncryptManager.DecryptFile2("test.cif");

        }
    }
}
