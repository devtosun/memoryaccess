using MemoryAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MemoryAccess
{
    public class ProcessManager : IProcessManager
    {
        Process _process;
        public ProcessManager(Process process)
        {
            _process = process;
        }

        public int Read(int address, int[] pointer )
        {
            int result = Memory.ReadMemory(_process, Calculator.GetPointer(address, pointer));
            return result;
        }

        public bool Write(int address, int value, int[] pointer)
        {
            int bytesWritten;
            bool result = Memory.WriteMemory(_process, Calculator.GetPointer(address, pointer), value, out bytesWritten);
            Console.WriteLine(bytesWritten);
            return result;
        }

        public void SendKeys(int keyAscii)
        {
            Memory.SendKeys(_process, keyAscii);
        }

        public void MouseClick(int x, int y)
        {
            Memory.MouseClick(_process, x, y);
        }

    }
}
