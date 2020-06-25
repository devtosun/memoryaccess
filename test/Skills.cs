using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Skills
    {


        internal static string[] GetSkills(string dataFolder = @"C:\Program Files (x86)\EA Games\UOML")
        {
            string skillIndexFile = Path.Combine(dataFolder, "skills.idx");
            string skillMulFile = Path.Combine(dataFolder, "skills.mul");
            if (!File.Exists(skillIndexFile))
                throw new FileNotFoundException(string.Format("File \"{0}\" not found!", skillIndexFile));
            if (!File.Exists(skillMulFile))
                throw new FileNotFoundException(string.Format("File \"{0}\" not found!", skillMulFile));
            byte[] indexBytes = File.ReadAllBytes(skillIndexFile);
            byte[] mulBytes = File.ReadAllBytes(skillMulFile);
            string[] skillArray = new string[indexBytes.Length / 12];
            int offset = 0;
            for (int x = 0; x < skillArray.Length; x++)
            {
                offset = x * 12;
                int start = BitConverter.ToInt32(indexBytes, offset);
                int length = BitConverter.ToInt32(indexBytes, offset + 4);
                if (length == 0)
                {
                    string[] newArray = new string[x];
                    Array.Copy(skillArray, 0, newArray, 0, x);
                    skillArray = newArray;
                    break;
                }
                skillArray[x] = ASCIIEncoding.ASCII.GetString(mulBytes, start + 1, length - 2);
            }
            return skillArray;
        }




    }
}
