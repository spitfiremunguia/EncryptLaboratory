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
            var s = Convert.ToInt64("0000000100100011010001010110011110001001101010111100110111101111", 2).ToString("X");//mensaje original
            var a = Encrypt.EncodeData("0000000100100011010001010110011110001001101010111100110111101111",true);//Encriptado
            var r=Convert.ToInt64(a, 2).ToString("X");//la salida en hexadecimal
            var b = Encrypt.Decrypt(a);//Se desencripta la salida de a
            var test= Convert.ToInt64(b, 2).ToString("X");//esto convierte la salida de bits a hexadecimal

        }
    }
}
