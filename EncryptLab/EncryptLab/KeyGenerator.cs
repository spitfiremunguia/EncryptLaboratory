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
            for(int i=0;i<16;i++)
            {
                string c = string.Empty;
                string d = string.Empty;
                if(i==0||i==1||i==8||i==15)//just once
                {
                    c = cCurrent.Substring(1, cCurrent.Length - 1) + cCurrent.Substring(0, 1);
                    d = dCurrent.Substring(1, dCurrent.Length - 1) + dCurrent.Substring(0, 1);
                    outPut.Add(c + d);
                    cCurrent = c;
                    dCurrent = d;
                }
                else //two times
                {
                    for(int j=0;j<2;j++)
                    {
                        c = cCurrent.Substring(1, cCurrent.Length - 1) + cCurrent.Substring(0, 1);
                        d = dCurrent.Substring(1, dCurrent.Length - 1) + dCurrent.Substring(0, 1);
                        cCurrent = c;
                        dCurrent = d;
                    }
                    outPut.Add(c + d);
                }
                
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
    }
}
