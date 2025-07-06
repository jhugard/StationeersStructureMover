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
                    //IdentifyStructures();
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
            while (BuildStructure(structureId, unassignedThings))
            {
                structureId++;
            }
            foreach (var structure in new List<Structure>(structures))
            {
                if (structure.ThingsInside.Count == 1)
                {
                    unassignedThings.AddRange(structure.ThingsInside);
                    structures.Remove(structure);
                }
            }
            foreach (var thing in unassignedThings)
            {
                outsideWorld.Add(thing);
            }

            var unassignedAtmos = new List<Atmosphere>(allAtmos);

        }

        private bool BuildStructure(int structureId, List<XThing> unassignedThings)
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
            structures.Add(structure);
            return true;
        }

        //private void IdentifyStructures()
        //{
        //    var graph = BuildRoomGraph();
        //    var connectedComponents = FindConnectedComponents(graph);

        //    structures = new List<Structure>();
        //    int structureId = 1;
        //    foreach (var component in connectedComponents)
        //    {
        //        var structureRooms = component.Select(roomId => allRooms.First(r => r.RoomId == roomId)).ToList();
        //        structures.Add(new Structure($"Structure #{structureId++}", structureRooms));
        //    }

        //    outsideWorld = new Structure("Outside World", new List<Room>());
        //    AssignThingsAndAtmospheres();
        //}

        //private Dictionary<int, List<int>> BuildRoomGraph()
        //{
        //    var graph = allRooms.ToDictionary(r => r.RoomId, r => new List<int>());
        //    var gridToRooms = new Dictionary<(double, double, double), List<int>>();

        //    foreach (var room in allRooms)
        //    {
        //        foreach (var grid in room.Grids)
        //        {
        //            var key = (grid.X, grid.Y, grid.Z);
        //            if (!gridToRooms.ContainsKey(key))
        //                gridToRooms[key] = new List<int>();
        //            gridToRooms[key].Add(room.RoomId);
        //        }
        //    }

        //    foreach (var room in allRooms)
        //    {
        //        var adjacentRooms = new HashSet<int>();
        //        foreach (var grid in room.Grids)
        //        {
        //            var adjacentPositions = new[]
        //            {
        //                (grid.X + 20, grid.Y, grid.Z),
        //                (grid.X - 20, grid.Y, grid.Z),
        //                (grid.X, grid.Y + 20, grid.Z),
        //                (grid.X, grid.Y - 20, grid.Z),
        //                (grid.X, grid.Y, grid.Z + 20),
        //                (grid.X, grid.Y, grid.Z - 20)
        //            };
        //            foreach (var adjPos in adjacentPositions)
        //            {
        //                if (gridToRooms.TryGetValue(adjPos, out var adjRooms))
        //                {
        //                    foreach (var adjRoomId in adjRooms)
        //                    {
        //                        if (adjRoomId != room.RoomId)
        //                            adjacentRooms.Add(adjRoomId);
        //                    }
        //                }
        //            }
        //        }
        //        graph[room.RoomId].AddRange(adjacentRooms);
        //    }
        //    return graph;
        //}

        //private List<List<int>> FindConnectedComponents(Dictionary<int, List<int>> graph)
        //{
        //    var visited = new HashSet<int>();
        //    var components = new List<List<int>>();

        //    foreach (var node in graph.Keys)
        //    {
        //        if (!visited.Contains(node))
        //        {
        //            var component = new List<int>();
        //            DFS(node, graph, visited, component);
        //            components.Add(component);
        //        }
        //    }
        //    return components;
        //}

        //private void DFS(int node, Dictionary<int, List<int>> graph, HashSet<int> visited, List<int> component)
        //{
        //    visited.Add(node);
        //    component.Add(node);
        //    foreach (var neighbor in graph[node])
        //    {
        //        if (!visited.Contains(neighbor))
        //        {
        //            DFS(neighbor, graph, visited, component);
        //        }
        //    }
        //}

        //private void AssignThingsAndAtmospheres()
        //{
        //    foreach (var thing in allThings)
        //    {
        //        bool assigned = false;
        //        foreach (var structure in structures)
        //        {
        //            var wp = thing.WorldPosition;
        //            var rwp = thing.RegisteredWorldPosition;
        //            if (wp != null && structure.Contains(wp))
        //            {
        //                structure.Add(thing);
        //                assigned = true;
        //                break;
        //            }
        //            else if (rwp != null && structure.Contains(rwp)
        //            {
        //                structure.Add(thing);
        //                assigned = true;
        //                break;
        //            }
        //        }
        //        if (!assigned)
        //        {
        //            outsideWorld.Add(thing);
        //        }
        //    }

        //    foreach (var atmosphere in allAtmos)
        //    {
        //        bool assigned = false;
        //        foreach (var structure in structures)
        //        {
        //            if (IsInsideStructure(atmosphere.Position, structure))
        //            {
        //                structure.AtmospheresInside.Add(atmosphere);
        //                assigned = true;
        //                break;
        //            }
        //        }
        //        if (!assigned)
        //        {
        //            outsideWorld.AtmospheresInside.Add(atmosphere);
        //        }
        //    }
        //}

        //private bool IsInsideStructure(XPoint3D pos, Structure structure)
        //{
        //    return structure.IsWithinProximity(pos);
        //}



    }
}
