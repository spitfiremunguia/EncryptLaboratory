using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptLab
{
    public class Encrypt
    {

        private static string key = "133457799BBCDFF1";
        private string InitialPermutation(string M)
        {
            int[,] Ip = new int[,]{
            {58,50,42,34,26,18,10,2},
            {60,52,44,36,28,20,12,4},
            {62,54,46,38,30,22,14,6},
            {64,56,48,40,32,24,16,8},
            {57,49,41,33,25,17,9,1},
            {59,51,43,35,27,19,11,3},
            {61,53,45,37,29,21,13,5},
            {63,55,47,39,31,23,15,7}};
            return Utilities.permutate(Ip,M);
        }
        private static string Ffunction(string right)
        {
            int[,] EbitTable = new int[,]{
            {32,1,2,3,4,5},
            {4,5,6,7,8,9},
            {8,9,10,11,12,13},
            {12,13,14,15,16,17},
            {16,17,18,19,20,21},
            {20,21,22,23,24,25},
            {24,25,26,27,28,29},
            {28,29,30,31,32,1}};
            string firstPermutate= Utilities.permutate(EbitTable, right);

        }
        private static string XOR(string subkey,string Fright)
        {
            string xor = string.Empty;
            for(int i=0;i<subkey.Length;i++)
            {
                xor += subkey[i] == Fright[i] ? "0" : "1";
            }
            return xor;
        }
        private static string FinalRight(string Ip)
        {
            string firstleft = Ip.Substring(0, Ip.Length / 2);
            string firstRigth = Ip.Substring(Ip.Length / 2);
            var keys = KeyGenerator.GenerateKeys(key);
            string f = Ffunction(firstRigth);
            string firstXOR = XOR(firstleft, f);
            for(int i=0; i<key.Length;i++)
            {

            }
        }

    }
}
