using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server_Launcher
{
    public struct MemorySize
    {
        public int Number { get; set; }
        public string Unit { get; set; }

        public MemorySize(int number, string unit)
        {
            if(number < 0)
            {
                throw new ArgumentException("Can't be negative", "number");
            }
            else
            {
                Number = number;
            }

            if(unit == "M" || unit == "G")
            {
                Unit = unit;
            }
            else
            {
                throw new ArgumentException("Must be M for Megabytes or G for Gigabytes", "unit");
            }
        }
    }
}
