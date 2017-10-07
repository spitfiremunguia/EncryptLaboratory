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
            var a = Encrypt.EncodeData("0000000100100011010001010110011110001001101010111100110111101111",true);
            string m = a.PadLeft(64, '0');
            var b = Encrypt.Decrypt(m);
            
        }
    }
}
