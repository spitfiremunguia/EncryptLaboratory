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
        public static bool Encriptar(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File was not found.");
                return false;
            }
            string allText = File.ReadAllText(path);
            byte[] allBytes = Encoding.Default.GetBytes(path);
            var hexadecimalFile = BitConverter.ToString(allBytes);//returns the file in hexadecimal
            while (hexadecimalFile.Length % 16 != 0)
            {
                hexadecimalFile += "0";
            }
            DirectoryInfo f = new DirectoryInfo(path);
            File.Create(f.Name + ".cif").Dispose();
            for (int i = 0; i < hexadecimalFile.Length; i += 16)
            {
                var block = hexadecimalFile.Substring(i, 16);
                block = Utilities.getBinaryKey(block);
                var encrypData = Encrypt.Encryption(block);
                File.AppendAllText(f.Name + ".cif", encrypData);
            }
            return true;


        }

        /// <summary>
        /// Deciphers a file ciphered by DES algorithm.
        /// </summary>
        /// <param name="inputPath"> File path.</param>
        /// <returns></returns>
        public static bool Decipher(string inputPath)
        {
            StringBuilder s = new StringBuilder();

            // Verifies if file extension is the correct.
            if (Path.GetExtension(inputPath) != ".cif")
            {
                Console.WriteLine("File doesn't have correct format.");
                return false;
            }
            // Verifies is file exists
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("File was not found.");
                return false;
            }
            // Reads file bytes
            byte[] AllFileBytes = File.ReadAllBytes(inputPath);
            // Reads file extension
            int FileExtensionlength = AllFileBytes[AllFileBytes.Length - 1];
            byte[] FileExtension = new byte[FileExtensionlength];
            for (int i = AllFileBytes.Length - 1 - FileExtensionlength; i < AllFileBytes.Length - 1; i++)
            {
                FileExtension[i - (AllFileBytes.Length - 1 - FileExtensionlength)] = AllFileBytes[i];
            }
            StringBuilder strFileExtension = new StringBuilder();
            for (int i = 0; i < FileExtension.Length; i++)
            {
                strFileExtension.Append((char)(FileExtension[i]));
            }


            byte[] FileBytes = new byte[AllFileBytes.Length - 1 - FileExtensionlength];

            for (int i = 0; i < FileBytes.Length; i++)
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
            // Encodes each group of 64 bits in the file.
            for (int i = 0; i < s.Length / 64; i++)
            {
                EncodedMessage.Append(Encrypt.Decrypt(output.Substring(64 * i, 64)));
            }

            List<byte> OutputBytes = new List<byte>();
            output = EncodedMessage.ToString();
            // Builds the bytes that will be written in the output file.
            for (int i = 0; i < EncodedMessage.Length / 8; i++)
            {
                OutputBytes.Add(Utilities.ConvertToByte(output.Substring(i * 8, 8)));
            }
            // Writes the deciphered file
            File.WriteAllBytes((Path.GetDirectoryName(inputPath)+"\\"+Path.GetFileNameWithoutExtension(inputPath) + "." + strFileExtension.ToString()), OutputBytes.ToArray());
            return true;
        }
        /// <summary>
        /// Ciphers a file using DES algorithm.
        /// </summary>
        /// <param name="inputPath"> File path. </param>
        /// <returns></returns>
        public static bool Cipher(string inputPath)
        {
            StringBuilder strBits = new StringBuilder();
            // Verifies if file exists.
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("File was not found.");
                return false;
            }
            string FileExtension = Path.GetExtension(inputPath).Substring(1);
            int FileExtensionLength = FileExtension.Length;
            byte[] FileBytes = File.ReadAllBytes(inputPath);
            // Builds a string with file data in binary.
            for (int i = 0; i < FileBytes.Length; i++)
            {
                strBits.Append(Convert.ToString(FileBytes[i], 2).PadLeft(8, '0'));
            }
            // Makes sure the data is multiple of 64 (Unncessary for files)
            while (strBits.Length % 64 != 0)
                strBits.Append("0");

            StringBuilder EncodedMessage = new StringBuilder();
            string output = strBits.ToString();
            // Encrypts each group of 64 bits.
            for (int i = 0; i < strBits.Length / 64; i++)
            {
                EncodedMessage.Append(Encrypt.Encryption(output.Substring(64 * i, 64)));
            }

            List<byte> OutputBytes = new List<byte>();
            output = EncodedMessage.ToString();
            for (int i = 0; i < EncodedMessage.Length / 8; i++)
            {
                OutputBytes.Add(Utilities.ConvertToByte(output.Substring(i * 8, 8)));
            }

            // Keeps track of file extension for deciphering procces.
            for (int i = 0; i < FileExtension.Length; i++)
            {
                OutputBytes.Add(Convert.ToByte((int)FileExtension[i]));
            }
            OutputBytes.Add(Convert.ToByte(FileExtensionLength));
            // Writes the ciphered file.
            File.WriteAllBytes(Path.GetDirectoryName(inputPath)+"\\"+Path.GetFileNameWithoutExtension(inputPath) + ".cif", OutputBytes.ToArray());
            return true;
        }
        
    }
}
