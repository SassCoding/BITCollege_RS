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

namespace BITCollegeWindows
{
    public partial class Grading : Form
    {
        ///given:  student and registration data will passed throughout 
        ///application. This object will be used to store the current
        ///student and selected registration
        ConstructorData constructorData;
        BITCollege_RSContext db = new BITCollege_RSContext();

        public Grading()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  This constructor will be used when called from the
        /// Student form.  This constructor will receive 
        /// specific information about the student and registration
        /// further code required:  
        /// </summary>
        /// <param name="constructorData">constructorData object containing
        /// specific student and registration data.</param>
        public Grading(ConstructorData constructorData)
        {
            InitializeComponent();

            //Populates the current constructorData with data from the past form
            this.constructorData = constructorData;

            //Sets this forms data source to the newly populated object
            studentBindingSource.DataSource = constructorData.Student;
            registrationBindingSource.DataSource = constructorData.Registration;
        }

        /// <summary>
        /// given: this code will navigate back to the Student form with
        /// the specific student and registration data that launched
        /// this form.
        /// </summary>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //return to student with the data selected for this form
            StudentData student = new StudentData(constructorData);
            student.MdiParent = this.MdiParent;
            student.Show();
            this.Close();
        }

        /// <summary>
        /// given:  open in top right of frame
        /// further code required:
        /// </summary>
        private void Grading_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            //Make use of the CourseFormat method to retrieve the mask for the Course Number label.
            lblCourseNumber.Mask = Utility.BusinessRules
                .CourseFormat(constructorData.Registration.Course.CourseType);

            if(constructorData.Registration.Grade == null)
            {
                lnkUpdate.Enabled = true;
                txtGrade.Enabled = true;
            }
            else
            {
                txtGrade.Enabled = false;
                lblExisting.Visible = true;
            }
        }

        /// <summary>
        /// Handles the click event of the link Update Grade
        /// </summary>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string gradeToFormat = txtGrade.Text;

            //Makes use of ClearFormatting to clear the % symbol from the grade
            //so it can be placed in the database. The utility takes in a string.
            string formattedGrade = Utility.Numeric.ClearFormatting(gradeToFormat, "%");
            
            if (!Utility.Numeric.IsNumeric(formattedGrade, System.Globalization.NumberStyles.AllowDecimalPoint))
            {
                string message = "Grade entered not valid integer. Please try again.";
                string caption = "Grade Validation Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK);
            }
            else
            {
                //Parse the formatted grade into an int from a string
                double parsedGrade = double.Parse(formattedGrade) / 100;

                if(parsedGrade > 1 || parsedGrade < 0)
                {
                    string message = "Grade entered needs to be a decimal between 0 and 1. Please try again.";
                    string caption = "Grade Validation Error";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK);
                }
                else
                {
                    //Creates a Web Service object
                    CollegeRegistrationService.CollegeRegistrationClient registrationService =
                        new CollegeRegistrationService.CollegeRegistrationClient();

                    //Updates the grade using the UpdateGrade method of the Web Service
                    registrationService.UpdateGrade
                        (
                        parsedGrade, 
                        constructorData.Registration.RegistrationId, 
                        constructorData.Registration.Notes
                        );

                    txtGrade.Enabled = false;
                    lnkUpdate.Enabled = false;
                    lblExisting.Visible = true;
                }
            }
        }
    }
}
