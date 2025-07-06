using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeersWorldEditor.Models
{
    internal class Bounds
    {
        public Point3D p1;
        public Point3D p2;

        public Bounds(Point3D p)
        {
            p1 = p;
            p2 = new Point3D(p.X, p.Y, p.Z);
        }

        public Bounds(Point3D p, double width, double height, double depth)
        {
            p1 = new Point3D(p.X - width / 2, p.Y - height / 2, p.Z - depth / 2);
            p2 = new Point3D(p.X + width / 2, p.Y + height / 2, p.Z + depth / 2);
        }

        public void Encompass(Point3D p)
        {
            if (p.X < p1.X) p1.X = p.X;
            if (p.Y < p1.Y) p1.Y = p.Y;
            if (p.Z < p1.Z) p1.Z = p.Z;
            if (p.X > p2.X) p2.X = p.X;
            if (p.Y > p2.Y) p2.Y = p.Y;
            if (p.Z > p2.Z) p2.Z = p.Z;
        }

        public void Encompass(Bounds b)
        {
            Encompass(b.p1);
            Encompass(b.p2);
        }

        public void Encompass(Point3D o, double width, double height, double depth)
        {
            Point3D newP1 = new Point3D(o.X - width / 2, o.Y - height / 2, o.Z - depth / 2);
            Point3D newP2 = new Point3D(o.X + width / 2, o.Y + height / 2, o.Z + depth / 2);
            Encompass(newP1);
            Encompass(newP2);
        }

        public bool Contains(Point3D p)
        {
            return p.X >= p1.X && p.X <= p2.X &&
                   p.Y >= p1.Y && p.Y <= p2.Y &&
                   p.Z >= p1.Z && p.Z <= p2.Z;
        }

        public bool Contains(Bounds b)
        {
            return (Contains(b.p1) && Contains(b.p2))
                || (b.Contains(p1) && b.Contains(p2));
        }

        public bool Intersects(Bounds b)
        {
            return !(p1.X > b.p2.X || p2.X < b.p1.X ||
          p1.Y > b.p2.Y || p2.Y < b.p1.Y ||
          p1.Z > b.p2.Z || p2.Z < b.p1.Z);
        }

        public bool Adjacent(Bounds b)
        {
            return p1.X == b.p1.X || p1.X == b.p2.X ||
                   p2.X == b.p1.X || p2.X == b.p2.X ||
                   p1.Y == b.p1.Y || p1.Y == b.p2.Y ||
                   p2.Y == b.p1.Y || p2.Y == b.p2.Y ||
                   p1.Z == b.p1.Z || p1.Z == b.p2.Z ||
                   p2.Z == b.p1.Z || p2.Z == b.p2.Z;
        }

        public void Scale(double factor)
        {
            p1.Scale(factor);
            p2.Scale(factor);
        }

        public override string ToString()
        {
            return $"[{p1} - {p2}]";
        }
    }
}
