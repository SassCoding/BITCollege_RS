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
    public partial class MultiDocInterface : Form
    {
        public MultiDocInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// close the mdi
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// open the student form in the frame
        /// </summary>
        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentData student = new StudentData();
            student.MdiParent = this;
            student.Show();

        }

        private void batchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Batch batch = new Batch();
            batch.MdiParent = this;
            batch.Show();
        }

   }
}
