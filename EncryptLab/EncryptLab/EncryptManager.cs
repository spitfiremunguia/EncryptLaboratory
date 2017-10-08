using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace EncryptLab
{
    public class EncryptManager
    {
        public static void EncryptFile(string path)
        {
            string allText = File.ReadAllText(path);
            byte[] allBytes = Encoding.Default.GetBytes(path);
            var hexadecimalFile = BitConverter.ToString(allBytes);//returns the file in hexadecimal
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

        public static void DecryptFile2(string inputPath)
        {

            byte[] AllFileBytes = File.ReadAllBytes(inputPath);
            int FileExtensionlength = AllFileBytes[AllFileBytes.Length - 1];
            byte[] FileExtension = new byte[FileExtensionlength];
            for(int i = AllFileBytes.Length-1-FileExtensionlength; i < AllFileBytes.Length-1; i++)
            {
                FileExtension[i - (AllFileBytes.Length -1- FileExtensionlength)] = AllFileBytes[i];
            }
            StringBuilder strFileExtension = new StringBuilder();
            for(int i = 0; i < FileExtension.Length; i++)
            {
                strFileExtension.Append((char)(FileExtension[i]));
            }

            StringBuilder s = new StringBuilder();

            byte[] FileBytes = new byte[AllFileBytes.Length-1-FileExtensionlength];
            
            for(int i = 0; i < FileBytes.Length; i++)
            {
                FileBytes[i] = AllFileBytes[i];
            }

            for (int i = 0; i < FileBytes.Length; i++)
            {
                s.Append(Convert.ToString(FileBytes[i], 2).PadLeft(8, '0'));
            }
            while (s.Length % 64 != 0)
                s.Append("0");

            StringBuilder EncodedMessage = new StringBuilder();
            string output = s.ToString();
            for (int i = 0; i < s.Length / 64; i++)
            {
                EncodedMessage.Append(Encrypt.Decrypt(output.Substring(64 * i, 64)));
            }

            List<byte> OutputBytes = new List<byte>();
            output = EncodedMessage.ToString();
            for (int i = 0; i < EncodedMessage.Length / 8; i++)
            {
                OutputBytes.Add(Utilities.ConvertToByte(output.Substring(i * 8, 8)));
            }
            File.WriteAllBytes((Path.GetFileNameWithoutExtension(inputPath)+"."+strFileExtension.ToString()), OutputBytes.ToArray());
        }

        public static void EncryptFile2(string inputPath)
        {
            string FileExtension = Path.GetExtension(inputPath).Substring(1);
            int FileExtensionLength = FileExtension.Length;

            byte[] FileBytes = File.ReadAllBytes(inputPath);
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < FileBytes.Length; i++)
            {
                s.Append(Convert.ToString(FileBytes[i], 2).PadLeft(8, '0'));
            }
            while (s.Length % 64 != 0)
                s.Append("0");

            StringBuilder EncodedMessage = new StringBuilder();
            string output = s.ToString();
            for (int i = 0; i < s.Length / 64; i++)
            {
                EncodedMessage.Append(Encrypt.Encryption(output.Substring(64 * i, 64)));
            }

            List<byte> OutputBytes = new List<byte>();
            output = EncodedMessage.ToString();
            for (int i = 0; i < EncodedMessage.Length / 8; i++)
            {
                OutputBytes.Add(Utilities.ConvertToByte(output.Substring(i * 8, 8)));
            }

            for(int i = 0; i < FileExtension.Length; i++)
            {
                OutputBytes.Add(Convert.ToByte((int)FileExtension[i]));
            }
            OutputBytes.Add(Convert.ToByte(FileExtensionLength));

            File.WriteAllBytes(Path.GetFileNameWithoutExtension(inputPath)+".cif", OutputBytes.ToArray());
        }



    }
}
