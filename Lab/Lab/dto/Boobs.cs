using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.dto
{
    public class Boobs : BaseDto
    {
        public ConsoleColor Color { get; set; }
        public BoobsSize Size { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}. Color: {Color}. BoobsSize: {Size}";
        }
    }

    public enum BoobsSize
    {
        A = 1,
        B = 2,
        C = 3
    }
}
