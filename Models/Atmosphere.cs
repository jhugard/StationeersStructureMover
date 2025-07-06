using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StationeersWorldEditor.Models
{
    internal class Atmosphere
    {
        public int ReferenceId { get; }
        public XPoint3D? Position { get; set; }

        public Atmosphere(XElement element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            ReferenceId = int.Parse(element.Element("ReferenceId")?.Value ?? "0");
            var positionElement = element.Element("Position");
            if (positionElement != null)
            {
                Position = new XPoint3D(positionElement);
            }
        }
        public override string ToString()
        {
            return $"Atmosphere ID: {ReferenceId}, Position: ({Position?.X}, {Position?.Y}, {Position?.Z})";
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
