using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;

namespace StationeersStructureMover.Models
{
    internal class XPoint3D : Point3D
    {
        private XElement node { get; }

        public XPoint3D(XElement node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            var ex = node.Element("x");
            var ey = node.Element("y");
            var ez = node.Element("z");
            if (ex==null || ey==null || ez==null)
            {
                throw new ArgumentNullException(nameof(node) + ": missing x, y, and/or z");
            }
            this.X = double.Parse(ex.Value, CultureInfo.InvariantCulture);
            this.Y = double.Parse(ey.Value, CultureInfo.InvariantCulture);
            this.Z = double.Parse(ez.Value, CultureInfo.InvariantCulture);
            this.node = node;
        }

        public override void Translate(double dx, double dy, double dz)
        {
            base.Translate(dx, dy, dz);
            UpdateXml();
        }

        public override void MoveTo(double x, double y, double z)
        {
            base.MoveTo(x, y, z);
            UpdateXml();
        }

        private void UpdateXml()
        {
            // Update the XML node with the point's position
            if (node != null)
            {
                node.Element("x").Value = this.X.ToString(CultureInfo.InvariantCulture);
                node.Element("y").Value = this.Y.ToString(CultureInfo.InvariantCulture);
                node.Element("z").Value = this.Z.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
