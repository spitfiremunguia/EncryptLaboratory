using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EncryptLab
{
    public class EncryptManager
    {
        public static void EncryptFile(string path)
        {
            string allText = File.ReadAllText(path);
            byte[] allBytes = Encoding.Default.GetBytes(path);
            var hexadecimalFile = BitConverter.ToString(allBytes).Replace("-","");//returns the file in hexadecimal
            while(hexadecimalFile.Length%16!=0)
            {
                hexadecimalFile += "0";
            }
            DirectoryInfo f = new DirectoryInfo(path);
            File.Create(f.Name + ".cif").Dispose();
            for(int i=0;i<hexadecimalFile.Length;i+=16)
            {
                var block = hexadecimalFile.Substring(i, 16);
                block = Utilities.getBinaryKey(block);
                var encrypData = Encrypt.Encryption(block);
                File.AppendAllText(f.Name + ".cif", encrypData);
            }


        }
    }
}
