using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StationeersStructureMover.Models
{
    internal class XGrid : XPoint3D
    {
        Bounds bounds;
        public XGrid(XElement element) : base(element)
        {
            bounds = new Bounds(this, 20, 20, 20);
        }

        public bool Adjacent(XGrid other)
        {
            return bounds.Adjacent(other.bounds);
        }

        public bool Contains(Point3D other)
        {
            return bounds.Contains(other);
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}
