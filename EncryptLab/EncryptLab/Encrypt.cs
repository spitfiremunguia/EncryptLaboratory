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

        public string FFunction(string RightHalf, string Key)
        {
            int[,] E_Matrix = new int[,]{
            {32,1,2,3,4,5},
            {4,5,6,7,8,9},
            {8,9,10,11,12,13},
            {12,13,14,15,16,17},
            {16,17,18,19,20,21},
            {20,21,22,23,24,25},
            {24,25,26,27,28,29},
            {28,29,30,31,32,1}};

            int[,] P_Matrix = new int[,]{
            {16,7,20,21},
            {29,12,28,17},
            {1,15,23,26},
            {5,18,31,10},
            {2,8,24,14},
            {19,13,30,6},
            {22,11,4,25}};

            string E = Utilities.permutate(E_Matrix,RightHalf);
            string xored = XOR(E, Key);

            List<string> addresses = new List<string>();
            for (int i = 0; i < E.Length; i += 6)
            {
                addresses.Add(E.Substring(i, i + 5)); // we have 8 groups, 8 bits each one
            }
            StringBuilder output = new StringBuilder();
            foreach (string s in addresses)
            {
                output.Append(SBoxFunction(s));
            }
            return Utilities.permutate(P_Matrix, output.ToString());
        }

        public string SBoxFunction(string s) {

            int[,] S_Matrix = new int[,]{
            {14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7},
            {0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8},
            {4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0},
            {15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13}};

            string strExtremos = s[0].ToString() + s[5].ToString();
            string strInternos = s.Substring(1, 4);

            int row = Convert.ToInt32(strExtremos, 2);
            int column = Convert.ToInt32(strInternos, 2);
            int number = S_Matrix[row, column];

            return Convert.ToString(number, 2);
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

        public string EncodeData(string M)
        {
            int[,] InerseIP_Matrix = new int[,]{
            {16,7,20,21},
            {29,12,28,17},
            {1,15,23,26},
            {5,18,31,10},
            {2,8,24,14},
            {19,13,30,6},
            {22,11,4,25}};

            List<string> GeneratedKeys = KeyGenerator.GenerateKeys(key);

            List<string> L = new List<string>();
            List<string> R = new List<string>();

            L.Add(M.Substring(0, M.Length / 2));
            R.Add(M.Substring(M.Length / 2));

            for (int i = 1; i < 16; i++)
            {
                L.Add(R.Last());
                R.Add(XOR(L.Last(), FFunction(R.Last(), GeneratedKeys[i])));
            }

            StringBuilder output = new StringBuilder();
            for (int i = 0; i < L.Count; i++)
            {
                output.Append(R[i]);
                output.Append(L[i]);
            }
            return Utilities.permutate(InerseIP_Matrix, output.ToString());
        }          
    }
}
