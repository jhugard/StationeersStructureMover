using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StationeersStructureMover.Models
{
    internal class Point3D
    {
        public double X;
        public double Y;
        public double Z;

        public Point3D()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }
        public Point3D(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public virtual void Translate(double dx, double dy, double dz)
        {
            // Translate the point by the specified amounts
            this.X += dx;
            this.Y += dy;
            this.Z += dz;
        }

        public virtual void Scale(double factor)
        {
            // Scale the point by the specified factor
            this.X *= factor;
            this.Y *= factor;
            this.Z *= factor;
        }

        public virtual void MoveTo(double x, double y, double z)
        {
            // Move the point to the specified coordinates
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public double Length()
        {
            // Calculate the length of the point from the origin
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
    }
}
