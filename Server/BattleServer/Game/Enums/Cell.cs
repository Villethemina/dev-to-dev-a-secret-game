using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Enums
{
    public enum Cell
    {
        Empty = 0,
        Untried,
        Fired,
        Hit,
        Sunked,
        Boat = 5,
    }
}
