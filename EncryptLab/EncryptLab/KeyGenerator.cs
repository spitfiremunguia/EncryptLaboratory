using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptLab
{
    public class KeyGenerator
    {
        
           


        private static string Kplus56(string hexData)
        {

            string key = Utilities.getBinaryKey(hexData);
            int[,] PC_1 = new int[,]{
            {57,49,41,33,25,17,9},
            {1,58,50,42,34,26,18},
            {10,2,59,51,43,35,27},
            {19,11,3,60,52,44,36},
            {63,55,47,39,31,23,15},
            {7,62,54,46,38,30,22},
            {14,6,61,53,45,37,29},
            {21,13,5,28,20,12,4}};
            return Utilities.permutate(PC_1, key);
           


        }
        private static List<string> CDgenerator(string key)
        {
            string Kplus = Kplus56(key);
            string C = Kplus.Substring(0,Kplus.Length/2);
            string D = Kplus.Substring((Kplus.Length / 2), Kplus.Length / 2);
            string cCurrent = C;
            string dCurrent = D;
            List<string> outPut = new List<string>();
            for(int i=0;i<16;i++)C:\Users\Maynor\source\repos\EncryptLaboratory\EncryptLab\EncryptLab\Encrypt.cs
            {
                string c = cCurrent.Substring(1, cCurrent.Length - 1) + cCurrent.Substring(0, 1);
                string d= dCurrent.Substring(1, dCurrent.Length - 1) + dCurrent.Substring(0, 1);
                outPut.Add(c + d);
                cCurrent = c;
                dCurrent = d;
            }
            return outPut;
        }
        


        public static List<string> GenerateKeys(string key)
        {
            int[,] PC_2 = new int[,]{
            {14,17,11,24,1,5},
            {3,28,15,6,21,10},
            {23,19,12,4,26,8},
            {16,7,27,20,13,2},
            {41,52,31,37,47,55},
            {30,40,51,45,33,48},
            {44,49,39,56,34,53},
            {46,42,50,36,29,32}};
            List<string> ks = new List<string>();
            List<string> cd = CDgenerator(key);
            foreach(string cds in cd)
            {
                string k = string.Empty;

                ks.Add(Utilities.permutate(PC_2, cds));
            }
            return ks;
        }
        
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

            string E = PermutationFunction(RightHalf, E_Matrix);
            string xored = XOR(E, Key);

            List<string> addresses = new List<string>();
            for(int i = 0; i < E.Length; i+=6)
            {
                addresses.Add(E.Substring(i,i+5)); // Now we have 8 groups, of 8 bits each one
            }
            StringBuilder output = new StringBuilder();
            foreach(string s in addresses)
            {
                output.Append(SBoxFunction(s));
            }
            return PermutationFunction(output.ToString(), P_Matrix);
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

        private string XOR(string e, string key)
        {
            throw new NotImplementedException();
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

            List<string> GeneratedKeys = GenerateKeys("añslkdjf");

            List<string> L = new List<string>();
            List<string> R = new List<string>();

            L.Add(M.Substring(0, M.Length / 2));
            R.Add(M.Substring(M.Length / 2));

            for (int i = 1; i < 16;i++) {
                L.Add(R.Last());
                R.Add(XOR(L.Last(), FFunction(R.Last(), GeneratedKeys[i])));
            }

            StringBuilder output = new StringBuilder();
            for(int i = 0; i < L.Count; i++)
            {
                output.Append(R[i]);
                output.Append(L[i]);
            }
            return PermutationFunction(output.ToString(), InerseIP_Matrix);
        }


        private string PermutationFunction(string rightHalf, int[,] Matrix)
        {
            throw new NotImplementedException();
        }
    }
}
