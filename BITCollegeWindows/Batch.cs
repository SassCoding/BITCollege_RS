using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BITCollegeWindows
{
    public partial class Batch : Form
    {
        public Batch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  ensures key is entered
        /// further code to be added
        /// </summary>
        private void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //NOTE:  This may be commented out until needed
            if (txtKey.Text == "")
            {
                MessageBox.Show("A 64-bit Key must be entered", "Error");
            }
        }

        /// <summary>
        /// given:  open in top right of frame
        /// further code required:
        /// </summary>
        private void Batch_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
        }
    }
}
