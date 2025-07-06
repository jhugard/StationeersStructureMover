using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeersWorldEditor.Models
{
    internal class Structure
    {
        public string Name { get; set; }
        public List<Room> Rooms = new List<Room>();
        public List<XThing> ThingsInside = new List<XThing>();
        public List<Atmosphere> AtmospheresInside = new List<Atmosphere>();
        public Bounds Bounds;
        double ProximityRange;

        public Structure(string name, double proximityRange = 20)
        {
            ProximityRange = proximityRange;
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }
            Name = name;
            ThingsInside = new List<XThing>();
            AtmospheresInside = new List<Atmosphere>();
        }

        public void Add(XThing thing)
        {
            if (thing == null) throw new ArgumentNullException(nameof(thing));
            ThingsInside.Add(thing);
            if (Bounds == null)
            {
                if (thing.WorldPosition != null)
                {
                    Bounds = new Bounds(thing.WorldPosition, ProximityRange, ProximityRange, ProximityRange);
                    if (thing.RegisteredWorldPosition != null)
                    {
                        Bounds.Encompass(thing.RegisteredWorldPosition);
                    }
                }
                else if (thing.RegisteredWorldPosition != null)
                {
                    Bounds = new Bounds(thing.RegisteredWorldPosition, ProximityRange, ProximityRange, ProximityRange);
                }
                else
                {
                    throw new InvalidOperationException("Thing must have a valid WorldPosition or RegisteredWorldPosition.");
                }
            }
            else
            {
                if (thing.WorldPosition != null)
                    Bounds.Encompass(thing.WorldPosition);
                if (thing.RegisteredWorldPosition != null)
                    Bounds.Encompass(thing.RegisteredWorldPosition);
            }
        }

        public void Add(Atmosphere atmos)
        {
            if (atmos == null) throw new ArgumentNullException(nameof(atmos));
            if (atmos.Position == null) throw new InvalidOperationException("Atmosphere must have a valid Position.");

            AtmospheresInside.Add(atmos);
            if (Bounds == null)
            {
                Bounds = new Bounds(atmos.Position, ProximityRange, ProximityRange, ProximityRange);
            }
            else
            {
                Bounds.Encompass(atmos.Position);
            }
        }

        public void Add(Room room)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));
            Rooms.Add(room);
            foreach (var grid in room.Grids)
            {
                Bounds.Encompass(grid);
            }
        }

        public bool IsWithinProximity(Point3D point)
        {
            var itemBounds = new Bounds(point, ProximityRange, ProximityRange, ProximityRange);
            return Bounds.Intersects(itemBounds);
        }

        public void Translate(double dx, double dy, double dz)
        {
            foreach (var room in Rooms)
            {
                foreach (var grid in room.Grids)
                {
                    grid.Translate(dx, dy, dz);
                }
            }
            foreach (var thing in ThingsInside)
            {
                thing.Translate(dx, dy, dz);
            }
            foreach (var atmosphere in AtmospheresInside)
            {
                atmosphere.Translate(dx, dy, dz);
            }
        }

        public Point3D GetCenter()
        {
            if (Bounds == null)
            {
                throw new InvalidOperationException("Bounds is not defined for this structure.");
            }
            return new Point3D(
                (Bounds.p1.X + Bounds.p2.X) / 2,
                (Bounds.p1.Y + Bounds.p2.Y) / 2,
                (Bounds.p1.Z + Bounds.p2.Z) / 2);
        }

        public override string ToString()
        {
            return $"Structure: {Name}, Center: {GetCenter()}, Rooms: {Rooms.Count}, ThingsInside: {ThingsInside.Count}, AtmospheresInside: {AtmospheresInside.Count}";
        }
    }
}
