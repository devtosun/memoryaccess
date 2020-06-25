using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoryAccess.WindowsApi
{
    public class Keyboard
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        //while (true)
        //{
        //    if (MemoryAccess.WindowsApi.Keyboard.GetAsyncKeyState(123) != 0)
        //    {
        //        Console.WriteLine("keys a");
        //        return;
        //    }
        //}
    }
}

