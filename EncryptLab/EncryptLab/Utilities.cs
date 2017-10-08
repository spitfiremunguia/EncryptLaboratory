using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptLab
{
    public class Utilities
    {
        private static BitArray ConvertHexToBitArray(string hexData)
        {
            if (hexData == null)
                return null;

            BitArray ba = new BitArray(4 * hexData.Length);
            for (int i = 0; i < hexData.Length; i++)
            {
                byte b = byte.Parse(hexData[i].ToString(), NumberStyles.HexNumber);
                for (int j = 0; j < 4; j++)
                {
                    ba.Set(i * 4 + j, (b & (1 << (3 - j))) != 0);
                }
            }
            return ba;
        }
        public static string getBinaryKey(string hexData)
        {
            var bitKey = ConvertHexToBitArray(hexData);
            string key = string.Empty;
            for (int i = 0; i < bitKey.Count; i++)
            {
                key += bitKey[i] ? "1" : "0";

            }
            return key;

        }
        public static string permutate(int[,] matrix, string input)
        {
            string output = string.Empty;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int position = matrix[i, j];
                    output += input[position - 1];
                }
            }
            return output;
        }
        public static bool GetBool(char c)
        {
            return c == '1';
        }
        public static BitArray Reverse(BitArray array)
        {
            int length = array.Length;
            int mid = (length / 2);
            for (int i = 0; i < mid; i++)
            {
                bool bit = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = bit;
            }
            return array;
        }
        public static byte ConvertToByte(String s)
        {
            List<bool> BooleanValues = new List<bool>();
            for (int i = 0; i < s.Length; i++)
            {
                BooleanValues.Add(GetBool(s[i]));
            }
            BitArray array = new BitArray(BooleanValues.ToArray());

            array = Reverse(array);
            byte[] bytes = new byte[1];
            array.CopyTo(bytes, 0);
            return bytes[0];
        }
        
    }
}
