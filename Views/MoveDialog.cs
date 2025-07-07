using StationeersStructureMover.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StationeersStructureMover.Views
{
    public partial class MoveDialog : Form
    {
        public MoveDialog()
        {
            InitializeComponent();
        }

        internal Point3D GetOffset()
        {
            var x = numXOffset.Value;
            var y = numYOffset.Value;
            var z = numZOffset.Value;
            return new Point3D((double)x, (double)y, (double)z);
        }

    }
}
