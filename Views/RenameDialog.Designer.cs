namespace StationeersStructureMover.Views
{
    partial class RenameDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnOk = new Button();
            btnCancel = new Button();
            tbStructureName = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(244, 70);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 0;
            btnOk.Text = "&Ok";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(325, 70);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbStructureName
            // 
            tbStructureName.Location = new Point(20, 41);
            tbStructureName.Name = "tbStructureName";
            tbStructureName.Size = new Size(380, 23);
            tbStructureName.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 19);
            label1.Name = "label1";
            label1.Size = new Size(145, 15);
            label1.TabIndex = 3;
            label1.Text = "Enter new structure name:";
            // 
            // RenameDialog
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(413, 105);
            ControlBox = false;
            Controls.Add(label1);
            Controls.Add(tbStructureName);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "RenameDialog";
            ShowInTaskbar = false;
            Text = "Rename Structure";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOk;
        private Button btnCancel;
        private Label label1;
        public TextBox tbStructureName;
    }
}