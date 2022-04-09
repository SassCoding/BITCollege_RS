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
    public partial class History : Form
    {
        ///given:  student and registration data will passed throughout 
        ///application. This object will be used to store the current
        ///student and selected registration
        ConstructorData constructorData;

        BITCollege_RSContext db = new BITCollege_RSContext();

        public History()
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
        public History(ConstructorData constructorData)
        {
            InitializeComponent();

            //Populates the current constructorData with data from the past form
            this.constructorData = constructorData;

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
        private void History_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            try
            {
                //Join query used to select data from the Registrations and
                //Courses tables where the StudentId matches the current StudentId
                //in the constructorData.
                var query = (from registration
                             in db.Registrations
                             join course
                             in db.Courses
                             on registration.CourseId
                             equals course.CourseId
                             where registration.StudentId == constructorData.Student.StudentId
                             select new
                             {
                                 //Grabbing the registration number, date, course, grade, and notes 
                                 registrationNumber = registration.RegistrationNumber,
                                 date = registration.RegistrationDate,
                                 course = course.Title,
                                 grade = registration.Grade,
                                 notes = registration.Notes
                             }).ToList();
                

                registrationBindingSource.DataSource = query;
            }
            catch(Exception exception)
            {
                //Message box
                string caption = "Error";
                string errorMessage = $"Error occurred: {exception.Message}";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(errorMessage, caption, buttons);
            }
        }
    }
}
