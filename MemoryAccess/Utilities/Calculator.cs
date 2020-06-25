using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;

namespace MemoryAccess
{
    public class Calculator
    {
        public static int HexToInt(string value)
        {
            return (int)new Int32Converter().ConvertFromString(value);
        }

        public static string IntToHex(int value)
        {
            return value.ToString("X");
        }

        public static int SumofPointers(int[] pointer)
        {
            int result = 0;
            foreach (var item in pointer)
            {
                result = result + (int)item;
            }
            return result;
        }

        /// <summary>
        /// BaseAddress + Pointer
        /// </summary>
        /// <param name="address"></param>
        /// <param name="sumofPointer"></param>
        /// <returns></returns>
        public static int GetPointer(int address, int[] sumofPointer) {
            int result = address + SumofPointers(sumofPointer);
            return result;
        }


    }
}
