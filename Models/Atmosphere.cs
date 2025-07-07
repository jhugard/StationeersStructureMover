using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StationeersStructureMover.Models
{
    internal class Atmosphere
    {
        public int ReferenceId { get; }
        public XPoint3D? Position { get; set; }
        public double Volume { get; }

        public Atmosphere(XElement element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            ReferenceId = int.Parse(element.Element("ReferenceId")?.Value ?? "0");
            var positionElement = element.Element("Position");
            if (positionElement != null)
            {
                Position = new XPoint3D(positionElement);
            }
            Volume = double.Parse(element.Element("Volume")?.Value ?? "0");
        }
        public override string ToString()
        {
            return $"Atmosphere ID: {ReferenceId} @ ({Position}), Volume: {Volume}";
        }

        public void Translate(double dx, double dy, double dz)
        {
            if (Position != null)
            {
                Position.Translate(dx, dy, dz);
            }
        }
    }
}
