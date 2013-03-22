using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starcraft_2_Fusion_Build_Order_Minimizer
{
    class Command
    {
        public string supply = null, command = null;
        public bool isUnit = false, sameAsLastSupply = false;
        public int timesRepeated = 1;

        public Command(string line)
        {
            try
            {
                string[] split = line.Split('/');
                if (split.Length <= 1) return;
                supply = split[0].Substring(split[0].Length - 2).Trim();
                command = split[1].Split('-')[1].Trim();
                if (command.Contains("Build SCV") ||
                    command.Contains("Build Marine") ||
                    command.Contains("Build Marauder") ||
                    command.Contains("Build Medivac") ||
                    command.Contains("Build Siege Tank") ||
                    command.Contains("Build Reaper") ||
                    command.Contains("Build Ghost") ||
                    command.Contains("Build Hellion") ||
                    command.Contains("Build Hellbat") ||
                    command.Contains("Build Widow") ||
                    command.Contains("Build Thor") ||
                    command.Contains("Build Viking") ||
                    command.Contains("Build Medivac") ||
                    command.Contains("Build Raven") ||
                    command.Contains("Build Banshee") ||
                    command.Contains("Build Battlecruiser"))
                {
                    isUnit = true;
                }
            }
            catch { }

        }

        public string ToString()
        {
            string ret = "";
            if (isUnit)
                ret += "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ";
            else
            {
                if (sameAsLastSupply == false)
                {
                    ret += supply + " - ";
                }
                else
                {
                    for (int i = 0; i < supply.Length; i++)
                    {
                        ret += " ";
                    }
                    ret += "     ";
                }
            }
            ret += (timesRepeated > 1 ? timesRepeated.ToString() + " * " : "") + command ;
            return ret;
        }
    }
}
