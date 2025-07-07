using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StationeersStructureMover.Models
{
    internal class XThing
    {
        public int ReferenceId { get; }
        public string PrefabName { get; }
        public string CustomName { get; }
        public XPoint3D? WorldPosition { get; set; }
        public XPoint3D? RegisteredWorldPosition { get; set; }

        private XElement Element { get; }

        public XThing(XElement element)
        {
            Element = element ?? throw new ArgumentNullException(nameof(element));
            ReferenceId = int.Parse(element.Element("ReferenceId")?.Value ?? "0");
            PrefabName = element.Element("PrefabName")?.Value ?? string.Empty;
            CustomName = element.Element("CustomName")?.Value ?? string.Empty;
            var wpe = element.Element("WorldPosition");
            if (wpe != null)
            {
                WorldPosition = new XPoint3D(wpe);
            }
            var rwpe = element.Element("RegisteredWorldPosition");
            if (rwpe != null)
            {
                RegisteredWorldPosition = new XPoint3D(rwpe);
            }
        }
        public override string ToString()
        {
            return $"{CustomName}({ReferenceId}) {PrefabName} @ ({WorldPosition})";
        }

        public void Translate(double dx, double dy, double dz)
        {
            if (WorldPosition != null)
            {
                WorldPosition.Translate(dx, dy, dz);
            }
            if (RegisteredWorldPosition != null)
            {
                RegisteredWorldPosition.Translate(dx, dy, dz);
            }
        }
    }
}