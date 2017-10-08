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
            if(args.Length != 2)
            {
                Console.WriteLine("No es un comando válido. Utilize -h para ayuda.");
                Console.ReadKey();
                if(args[0]!="-h")
                    return;
            }
            if(args[0] == "-c")
            {
                // Cipher
                if(args[1].Substring(0,2) == "-f")
                {
                    // Path comming 
                    string Path = args[1].Substring(2);
                    if (EncryptManager.Cipher(Path))
                    {
                        Console.WriteLine("File was succesfully ciphered.");
                    }
                    else
                    {
                        Console.WriteLine("An error occured while deciphering file.");
                    }
                }
            }
            else if (args[0] == "-d")
            {
                // Decipher
                if(args[1].Substring(0,2) == "-f")
                {
                    // Path comming
                    string Path = args[1].Substring(2);
                    if (EncryptManager.Decipher(Path))
                    {
                        Console.WriteLine("File was succesfully deciphered");
                    }
                    else
                    {
                        Console.WriteLine("An error occured while deciphering file.");
                    }
                }

            }
            else if(args[0] == "-h")
            {
                Console.WriteLine("Uso: EncryptLab [-c] [-h]\n" +
                                  "                [-d] [-h]");
                Console.WriteLine("Opciones:");
                Console.WriteLine("-c -t[Path] Cifra el archivo con ruta [Path]");
                Console.WriteLine("-d -t[Path] Descifra el archivo con ruta [Path]");

            }
            else
            {
                Console.WriteLine("Command not found, use -h for help.");
            }
            Console.ReadKey();
        }
    }
}
