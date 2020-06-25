using MemoryAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace test
{
    class Program
    {
        static void Main(string[] args)
        {

         
            Process process = Process.GetProcessesByName("client_2.0.3").FirstOrDefault();
            var processes = Process.GetProcesses();
            foreach (var item in processes)
            {
                if (item.MainWindowTitle.Contains("Ultima")) 
                    Console.WriteLine($"Process Name = {item.ProcessName}, Process Id = {item.Id} {item.MainWindowTitle} {item.MainWindowHandle}");
            }


            //int result = MemoryAccess.Memory.ReadMemory(process, (int)new Int32Converter().ConvertFromString("0x00cc0458"));
            //Console.WriteLine(result.ToString());

            /////////

            int[] liste = new int[] { 12 };
            ProcessManager processManager = new ProcessManager(process);
            //processManager.Write(0x00cc0458, 1073750003, liste);

            //Console.WriteLine(processManager.Read(0x0019D820, liste));
            ////////

            string[] skill = Skills.GetSkills();

            Console.WriteLine(skill[5]);

            Console.ReadLine();


        }
    }
}
