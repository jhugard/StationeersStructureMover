namespace StationeersWorldEditor
{
    using StationeersWorldEditor.Models;
    using System;
    using System.IO.Compression;
    using System.Xml.Linq;
    using System.Globalization;

    public partial class MainForm : Form
    {
        private string? saveFilePath;
        private XDocument? worldXml;

        private List<Room>? allRooms;
        private List<XThing>? allThings;
        private List<Atmosphere>? allAtmos;

        private List<Structure>? structures;
        private Structure? outsideWorld;

        public MainForm()
        {
            InitializeComponent();
        }

        private void openSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Stationeers Save Files (*.save)|*.save",
                Title = "Select a Stationeers Save File",
                InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "My Games", "Stationeers", "saves")
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    saveFilePath = openFileDialog.FileName;
                    LoadWorldXml();
                    ParseWorldXml();
                    BuildStructures();
                    structures = structures.OrderBy(p => p.GetCenter().Length()).ToList();
                    //PopulateUI();
                }
            }
        }


        private void LoadWorldXml()
        {
            string tempPath = Path.GetTempPath();
            string extractPath = Path.Combine(tempPath, "world.xml");

            using (ZipArchive archive = ZipFile.OpenRead(saveFilePath))
            {
                var entry = archive.GetEntry("world.xml");
                if (entry != null)
                {
                    entry.ExtractToFile(extractPath, true);
                    worldXml = XDocument.Load(extractPath);
                }
                else
                {
                    throw new Exception("world.xml not found in save file.");
                }
            }
        }

        private void ParseWorldXml()
        {
            allRooms = ParseRooms();
            allThings = ParseThings();
            allAtmos = ParseAtmos();
        }

        private List<Room> ParseRooms()
        {
            var rooms = new List<Room>();
            var roomElements = worldXml.Descendants("Room");

            foreach (var roomElement in roomElements)
            {
                int roomId = int.Parse(roomElement.Element("RoomId")?.Value ?? "0");
                var grids = roomElement.Descendants("Grid").Select(
                    global => new XGrid(global)
                    ).ToList();
                rooms.Add(new Room
                {
                    RoomId = roomId,
                    Grids = grids
                });
            }
            return rooms;
        }

        private List<XThing> ParseThings()
        {
            var things = new List<XThing>();
            var thingElements = worldXml.Descendants("ThingSaveData");
            if (thingElements != null)
            {
                foreach (var thingElement in thingElements)
                {
                    things.Add(new XThing(thingElement));
                }
            }
            return things;
        }

        private List<Atmosphere> ParseAtmos()
        {
            var atmospheres = new List<Atmosphere>();
            var atmosphereElements = worldXml.Descendants("AtmosphereSaveData");
            if (atmosphereElements != null)
            {
                foreach (var atmosphereElement in atmosphereElements)
                {
                    atmospheres.Add(new Atmosphere(atmosphereElement));
                }
            }
            return atmospheres;
        }

        private void BuildStructures()
        {
            int structureId = 1;
            structures = new List<Structure>();
            outsideWorld = new Structure("Outside");
            var unassignedThings = new List<XThing>(allThings);
            var unassignedAtmos = new List<Atmosphere>(allAtmos);
            var unassignedRooms = new List<Room>(allRooms);

            while (BuildStructure(structureId, unassignedThings, unassignedAtmos, unassignedRooms))
            {
                structureId++;
            }
            foreach (var structure in new List<Structure>(structures))
            {
                if (structure.ThingsInside.Count == 1)
                {
                    unassignedThings.AddRange(structure.ThingsInside);
                    unassignedAtmos.AddRange(structure.AtmospheresInside);
                    unassignedRooms.AddRange(structure.Rooms);
                    structures.Remove(structure);
                }
            }
            foreach (var thing in unassignedThings)
            {
                outsideWorld.Add(thing);
            }
            foreach (var atmos in unassignedAtmos)
            {
                outsideWorld.Add(atmos);
            }
            foreach (var room in unassignedRooms)
            {
                outsideWorld.Add(room);
            }
        }

        private bool BuildStructure(
            int structureId,
            List<XThing> unassignedThings,
            List<Atmosphere> unassignedAtmos,
            List<Room> unassignedRooms)
        {
            if (unassignedThings.Count == 0)
                return false;

            var structure = new Structure($"Structure #{structureId}");
            var firstThing = unassignedThings.First();
            unassignedThings.Remove(firstThing);
            structure.Add(firstThing);

            bool dirty = true;
            while (dirty)
            {
                dirty = false;
                foreach (var thing in new List<XThing>(unassignedThings))
                {
                    bool assigned = false;
                    var wp = thing.WorldPosition;
                    var rwp = thing.RegisteredWorldPosition;
                    if (wp != null && structure.IsWithinProximity(wp))
                    {
                        structure.Add(thing);
                        assigned = true;
                    }
                    else if (rwp != null && structure.IsWithinProximity(rwp))
                    {
                        structure.Add(thing);
                        assigned = true;
                    }
                    if (assigned)
                    {
                        unassignedThings.Remove(thing);
                        dirty = true;
                    }
                }
            }

            dirty = true;
            while (dirty)
            {
                dirty = false;
                foreach (var atmos in new List<Atmosphere>(unassignedAtmos))
                {
                    if (structure.IsWithinProximity(atmos.Position))
                    {
                        structure.Add(atmos);
                        unassignedAtmos.Remove(atmos);
                        dirty = true;
                    }
                }
            }

            dirty = true;
            while (dirty)
            {
                dirty = false;
                foreach (var room in new List<Room>(unassignedRooms))
                {
                    var roomBounds = room.GetBounds();
                    roomBounds.Scale(0.10);
                    if (structure.Bounds.Intersects(roomBounds))
                    {
                        structure.Add(room);
                        unassignedRooms.Remove(room);
                        dirty = true;
                    }
                }
            }
            
            structures.Add(structure);
            return true;
        }
    }
}
