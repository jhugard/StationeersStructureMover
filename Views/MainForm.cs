namespace StationeersStructureMover.Views
{
    using StationeersStructureMover.Models;
    using System;
    using System.IO.Compression;
    using System.Xml.Linq;
    using System.Globalization;
    using System.Security.AccessControl;

    public partial class MainForm : Form
    {
        private string? sourceFilePath;
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
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
                    sourceFilePath = openFileDialog.FileName;
                    saveFilePath = sourceFilePath;
                    LoadWorldXml();
                    ParseWorldXml();
                    BuildStructures();
                    if (structures != null)
                        structures = structures.OrderBy(p => p.GetCenter().Length()).ToList();
                    PopulateUI();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sourceFilePath == null)
            {
                MessageBox.Show("No save file loaded. Please open a save file first.");
                return;
            }

            // Rename existing file to back it up
            if (File.Exists(saveFilePath))
            {
                var ix = 0;
                while (File.Exists(saveFilePath + ".bak" + (ix == 0 ? "" : $"_{ix}")))
                {
                    ix++;
                }
                var backupFilePath = saveFilePath + ".bak" + (ix == 0 ? "" : $"_{ix}");
                File.Copy(saveFilePath, backupFilePath, false);
            }
            try
            {
                if (saveFilePath != sourceFilePath)
                {
                    File.Copy(sourceFilePath, saveFilePath, true);
                }
                using (var archive = ZipFile.Open(saveFilePath, ZipArchiveMode.Update))
                {
                    var entry = archive.GetEntry("world.xml");
                    if (entry != null)
                    {
                        entry.Delete();
                    }
                    var newEntry = archive.CreateEntry("world.xml");
                    using (var stream = newEntry.Open())
                    {
                        worldXml.Save(stream);
                    }
                }
                sourceFilePath = saveFilePath;
                MessageBox.Show("Save file updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}");
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Stationeers Save Files (*.save)|*.save",
                Title = "Save Stationeers World",
                InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "My Games", "Stationeers", "saves")
            })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    saveFilePath = saveFileDialog.FileName;
                    saveToolStripMenuItem_Click(sender, e);
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
            outsideWorld = new Structure("Loose Items");
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

        void PopulateUI()
        {
            if (structures == null || structures.Count == 0)
            {
                MessageBox.Show("No structures found in the world.");
                return;
            }

            treeView.BeginUpdate();
            treeView.Nodes.Clear();
            foreach (var structure in structures)
            {
                AddStructureToTreeView(structure);
            }
            if (outsideWorld != null)
            {
                AddStructureToTreeView(outsideWorld);
            }
            treeView.EndUpdate();
        }

        void AddStructureToTreeView(Structure structure)
        {
            var node = treeView.Nodes.Add(structure.ToString());
            node.Tag = structure;
            foreach (var thing in structure.ThingsInside)
            {
                var n = node.Nodes.Add(thing.ToString());
                n.Tag = thing;
            }
            foreach (var atmos in structure.AtmospheresInside)
            {
                var n = node.Nodes.Add(atmos.ToString());
                n.Tag = atmos;
            }
            foreach (var room in structure.Rooms)
            {
                var roomNode = node.Nodes.Add(room.ToString());
                roomNode.Tag = room;
                foreach (var grid in room.Grids)
                {
                    var n = roomNode.Nodes.Add(grid.ToString());
                    n.Tag = grid;
                }
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null && treeView.SelectedNode.Tag is Structure structure)
            {
                using (var renameDialog = new Views.RenameDialog())
                {
                    renameDialog.tbStructureName.Text = structure.Name;
                    if (renameDialog.ShowDialog() == DialogResult.OK)
                    {
                        structure.Name = renameDialog.tbStructureName.Text;
                        treeView.SelectedNode.Text = structure.ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a structure to rename.");
            }
        }

        private void offsetSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (MoveDialog moveDialog = new MoveDialog())
            {
                if (moveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (treeView.SelectedNode != null && treeView.SelectedNode.Tag is Structure structure)
                    {
                        var offset = moveDialog.GetOffset();
                        structure.Translate(offset.X, offset.Y, offset.Z);
                        UpdateNodeAndChildrenFromTag(treeView.SelectedNode);
                        treeView.SelectedNode.Text = structure.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Please select a structure to offset.");
                    }
                }
            }
        }

        void UpdateNodeAndChildrenFromTag(TreeNode node)
        {
            // The following casts are probably not necessary,
            // but since it was autocompleted, keeping it anyway.
            // Might try just Node.Text = node.Tag.ToString() later.
            if (node.Tag is Structure structure)
            {
                node.Text = structure.ToString();
                foreach (TreeNode childNode in node.Nodes)
                {
                    if (childNode.Tag is XThing thing)
                    {
                        childNode.Text = thing.ToString();
                    }
                    else if (childNode.Tag is Atmosphere atmosphere)
                    {
                        childNode.Text = atmosphere.ToString();
                    }
                    else if (childNode.Tag is Room room)
                    {
                        childNode.Text = room.ToString();
                        foreach (var grid in room.Grids)
                        {
                            var gridNode = childNode.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Tag == grid);
                            if (gridNode != null)
                            {
                                gridNode.Text = grid.ToString();
                            }
                        }
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
