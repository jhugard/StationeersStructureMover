using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeersStructureMover.Models
{
    internal class Room
    {
        public int RoomId;
        public List<XGrid> Grids;

        public override string ToString()
        {
            return $"Room ID: {RoomId}, Grids Count: {Grids.Count}";
        }

        public Bounds GetBounds()
        {
            if (Grids == null || Grids.Count == 0)
            {
                throw new InvalidOperationException("No grids in the room to calculate bounds.");
            }
            Bounds bounds = new Bounds(Grids[0], 20, 20, 20); //todo: make proximity a parameter?
            foreach (var grid in Grids)
            {
                bounds.Encompass(grid);
            }
            return bounds;
        }
    }
}


