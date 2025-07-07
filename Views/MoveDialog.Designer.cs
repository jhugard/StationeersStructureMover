﻿namespace StationeersStructureMover.Views
{
    partial class MoveDialog
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
            label1 = new Label();
            numXOffset = new NumericUpDown();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            numYOffset = new NumericUpDown();
            numZOffset = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numXOffset).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numYOffset).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numZOffset).BeginInit();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(174, 102);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 0;
            btnOk.Text = "&Ok";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(255, 102);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 19);
            label1.Name = "label1";
            label1.Size = new Size(152, 15);
            label1.TabIndex = 3;
            label1.Text = "Move selected structure by:";
            // 
            // numXOffset
            // 
            numXOffset.Location = new Point(53, 44);
            numXOffset.Name = "numXOffset";
            numXOffset.Size = new Size(89, 23);
            numXOffset.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 46);
            label2.Name = "label2";
            label2.Size = new Size(17, 15);
            label2.TabIndex = 5;
            label2.Text = "X:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 75);
            label3.Name = "label3";
            label3.Size = new Size(17, 15);
            label3.TabIndex = 6;
            label3.Text = "Y:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 104);
            label4.Name = "label4";
            label4.Size = new Size(17, 15);
            label4.TabIndex = 7;
            label4.Text = "Z:";
            // 
            // numYOffset
            // 
            numYOffset.Location = new Point(53, 73);
            numYOffset.Name = "numYOffset";
            numYOffset.Size = new Size(89, 23);
            numYOffset.TabIndex = 8;
            // 
            // numZOffset
            // 
            numZOffset.Location = new Point(53, 102);
            numZOffset.Name = "numZOffset";
            numZOffset.Size = new Size(89, 23);
            numZOffset.TabIndex = 9;
            // 
            // MoveDialog
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = btnCancel;
            ClientSize = new Size(337, 143);
            ControlBox = false;
            Controls.Add(numZOffset);
            Controls.Add(numYOffset);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(numXOffset);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "MoveDialog";
            ShowInTaskbar = false;
            Text = "Move Structure";
            ((System.ComponentModel.ISupportInitialize)numXOffset).EndInit();
            ((System.ComponentModel.ISupportInitialize)numYOffset).EndInit();
            ((System.ComponentModel.ISupportInitialize)numZOffset).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOk;
        private Button btnCancel;
        private Label label1;
        private NumericUpDown numXOffset;
        private Label label2;
        private Label label3;
        private Label label4;
        private NumericUpDown numYOffset;
        private NumericUpDown numZOffset;
    }
}