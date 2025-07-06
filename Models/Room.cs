using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeersWorldEditor.Models
{
    internal class Room
    {
        public int RoomId;
        public List<XGrid> Grids;

        public override string ToString()
        {
            return $"Room ID: {RoomId}, Grids Count: {Grids.Count}";
        }
    }
}


