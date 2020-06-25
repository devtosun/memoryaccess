using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace MemoryAccess
{
    internal class Memory
    {

        [Flags]
        private enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        [Flags]
        private enum WMessages : uint
        {
            //Key
            WM_KEYDOWN = 0x104,
            WM_KEYUP = 0x105,
            //Mouse
            WM_LBUTTONDOWN = 0x201, //Left mousebutton down
            WM_LBUTTONUP = 0x202, //Left mousebutton up
            WM_RBUTTONDOWN = 0x204, //Right mousebutton down
            WM_RBUTTONUP = 0x205 //Right mousebutton up
        }


        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, UInt32 nSize, ref UInt32 lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern Int32 CloseHandle(IntPtr hProcess);

        [DllImport("user32.dll")]
        private static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);




        public static bool WriteMemory(Process process, int address, long value, out int bytesWritten)
        {
            IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, process.Id);
            byte[] val = BitConverter.GetBytes(value);
            bool result = WriteProcessMemory(hProc, new IntPtr(address), val, (UInt32)val.LongLength, out bytesWritten);
            CloseHandle(hProc);
            return result;
        }

        public static int ReadMemory(Process process, int address, uint nSize = 4)
        {
            IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, process.Id);
            byte[] buffer = new byte[4];
            UInt32 num = 0;
            bool result = ReadProcessMemory(hProc, (IntPtr)address, buffer, nSize, ref num);      /// nSize > byte boyutu
            CloseHandle(hProc);
            return (BitConverter.ToInt32(buffer, 0));
        }


        public static void MouseClick(Process process, int x, int y)
        {
            IntPtr hProc = Memory.OpenProcess(Memory.ProcessAccessFlags.All, false, process.Id);
            IntPtr lParam = (IntPtr)((x & 0xFFFF) | ((y & 0xFFFF) << 16));
            PostMessage(hProc, (uint)WMessages.WM_LBUTTONDOWN, IntPtr.Zero, lParam);
            PostMessage(hProc, (uint)WMessages.WM_LBUTTONUP, IntPtr.Zero, lParam);
        }

        public static void SendKeys(Process process, int keyAscii)
        {
            IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, process.Id);
            PostMessage(hProc, (uint)WMessages.WM_KEYDOWN, (IntPtr)keyAscii, IntPtr.Zero);   //123 F12
            PostMessage(hProc, (uint)WMessages.WM_KEYUP, (IntPtr)keyAscii, IntPtr.Zero);
        }


    }

}
