using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BITCollege_RS.Data;
using BITCollege_RS.Models;
using BITCollegeWindows;

namespace BITCollegeWindows
{
    public partial class Batch : Form
    {
        BITCollege_RSContext db = new BITCollege_RSContext();

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
            //if (txtKey.Text == "")
            //{
            //   MessageBox.Show("A 64-bit Key must be entered", "Error");
            //}

            //New batch process object.
            BatchProcess batchProcess = new BatchProcess();

            //If the select single is checked.
            if (radSelect.Checked)
            {
                //Calls the ProcessTransmission method passing it the combo box value member
                //and the key.
                batchProcess.ProcessTransmission(cboProgram.SelectedValue.ToString(), txtKey.Text.ToString());

                //Captures the return of the write log data method
                string writeLogReturn = batchProcess.WriteLogData();

                //Appends the return of the write log data method to the rich text log.
                rtxtLog.Text += writeLogReturn + "\n\n";
            }
            //Else if the select all is checked
            if (radAll.Checked)
            {
                foreach (AcademicProgram item in cboProgram.Items)
                {
                    //Calls the ProcessTransmission method passing it the combo box value member
                    //and the key.
                    batchProcess.ProcessTransmission(item.ProgramAcronym, txtKey.Text.ToString());
                    
                    //Captures the return of the write log data method
                    string writeLogReturn = batchProcess.WriteLogData();
                    
                    //Appends the return of the write log data method to the rich text log.
                    rtxtLog.Text += writeLogReturn + "\n\n";
                }
            }
          
        }

        /// <summary>
        /// given:  open in top right of frame
        /// further code required:
        /// </summary>
        private void Batch_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            IQueryable<AcademicProgram> academicPrograms = db.AcademicPrograms;

            academicProgramBindingSource.DataSource = academicPrograms.ToList();
        }

        /// <summary>
        /// Handles the checked changed event of the radio buttons.
        /// If the select radio button is checked, the combo box is enabled.
        /// </summary>
        private void radSelect_CheckedChanged(object sender, EventArgs e)
        {
            if(radSelect.Checked == true)
            {
                cboProgram.Enabled = true;
            }
            else
            {
                cboProgram.Enabled = false;
            }
        }
    }
}
