using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windows_form_01.Model
{
    public class MineField
    {
        public MineField(int index)
        {
            Index = index;
        }

        public int Index { get; set; }
        public bool Opened { get; set; }
        public bool HasMine { get; set; }
        public int NeighborMineCount { get; set; }
        public bool Marked { get; set; }

        
    }
}
