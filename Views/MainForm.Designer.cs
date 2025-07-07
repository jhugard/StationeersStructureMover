using System;
using System.Windows.Forms;

namespace StationeersStructureMover.Views
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            offsetAllToolStripMenuItem = new ToolStripMenuItem();
            offsetSelectedToolStripMenuItem = new ToolStripMenuItem();
            renameToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            revertToSavedToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            worldDetailsToolStripMenuItem = new ToolStripMenuItem();
            selectedDetailsToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            treeView = new TreeView();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(903, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripMenuItem1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(180, 22);
            openToolStripMenuItem.Text = "&Open...";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(180, 22);
            saveToolStripMenuItem.Text = "&Save...";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(180, 22);
            saveAsToolStripMenuItem.Text = "Save &As...";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(180, 22);
            exitToolStripMenuItem.Text = "E&xit";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { offsetAllToolStripMenuItem, offsetSelectedToolStripMenuItem, renameToolStripMenuItem, toolStripMenuItem2, undoToolStripMenuItem, redoToolStripMenuItem, revertToSavedToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "&Edit";
            // 
            // offsetAllToolStripMenuItem
            // 
            offsetAllToolStripMenuItem.Name = "offsetAllToolStripMenuItem";
            offsetAllToolStripMenuItem.Size = new Size(180, 22);
            offsetAllToolStripMenuItem.Text = "Move &All...";
            // 
            // offsetSelectedToolStripMenuItem
            // 
            offsetSelectedToolStripMenuItem.Name = "offsetSelectedToolStripMenuItem";
            offsetSelectedToolStripMenuItem.Size = new Size(180, 22);
            offsetSelectedToolStripMenuItem.Text = "Movet &Selected...";
            offsetSelectedToolStripMenuItem.Click += offsetSelectedToolStripMenuItem_Click;
            // 
            // renameToolStripMenuItem
            // 
            renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            renameToolStripMenuItem.ShortcutKeys = Keys.F2;
            renameToolStripMenuItem.Size = new Size(180, 22);
            renameToolStripMenuItem.Text = "&Rename...";
            renameToolStripMenuItem.Click += renameToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(177, 6);
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(180, 22);
            undoToolStripMenuItem.Text = "&Undo";
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(180, 22);
            redoToolStripMenuItem.Text = "&Redo";
            // 
            // revertToSavedToolStripMenuItem
            // 
            revertToSavedToolStripMenuItem.Name = "revertToSavedToolStripMenuItem";
            revertToSavedToolStripMenuItem.Size = new Size(180, 22);
            revertToSavedToolStripMenuItem.Text = "Re&vert to Saved";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { worldDetailsToolStripMenuItem, selectedDetailsToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "&View";
            // 
            // worldDetailsToolStripMenuItem
            // 
            worldDetailsToolStripMenuItem.Name = "worldDetailsToolStripMenuItem";
            worldDetailsToolStripMenuItem.Size = new Size(164, 22);
            worldDetailsToolStripMenuItem.Text = "World details...";
            // 
            // selectedDetailsToolStripMenuItem
            // 
            selectedDetailsToolStripMenuItem.Name = "selectedDetailsToolStripMenuItem";
            selectedDetailsToolStripMenuItem.Size = new Size(164, 22);
            selectedDetailsToolStripMenuItem.Text = "Selected details...";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeView);
            splitContainer1.Size = new Size(903, 605);
            splitContainer1.SplitterDistance = 590;
            splitContainer1.TabIndex = 1;
            // 
            // treeView
            // 
            treeView.Dock = DockStyle.Fill;
            treeView.FullRowSelect = true;
            treeView.Location = new Point(0, 0);
            treeView.Name = "treeView";
            treeView.Size = new Size(590, 605);
            treeView.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(903, 629);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Stationeers Structure Mover";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem offsetAllToolStripMenuItem;
        private ToolStripMenuItem offsetSelectedToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem revertToSavedToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem worldDetailsToolStripMenuItem;
        private ToolStripMenuItem selectedDetailsToolStripMenuItem;
        private SplitContainer splitContainer1;
        private TreeView treeView;
        private ToolStripMenuItem renameToolStripMenuItem;
    }
}
