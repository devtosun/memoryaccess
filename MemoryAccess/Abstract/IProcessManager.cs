using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryAccess.Abstract
{
    interface IProcessManager
    {
        bool Write(int address, int value, int[] pointer);
        int Read(int address, int[] pointer);
        void MouseClick(int x, int y);
        void SendKeys(int keyAscii);
    }
}
