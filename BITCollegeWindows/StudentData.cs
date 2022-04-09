using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BITCollege_RS;
using BITCollege_RS.Models;
using BITCollege_RS.Data;

namespace BITCollegeWindows
{
    public partial class StudentData : Form
    {
        BITCollege_RSContext db = new BITCollege_RSContext();

        ///Given: Student and Registration data will be retrieved
        ///in this form and passed throughout application
        ///These variables will be used to store the current
        ///Student and selected Registration
        ConstructorData constructorData = new ConstructorData();
        public StudentData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  This constructor will be used when returning to frmStudent
        /// from another form.  This constructor will pass back
        /// specific information about the student and registration
        /// based on activites taking place in another form
        /// </summary>
        /// <param name="constructorData">constructorData object containing
        /// specific student and registration data.</param>
        public StudentData(ConstructorData constructorData)
        {
            InitializeComponent();

            //further code to be added  

            if (constructorData.Student != null)
            {
                populateConstructorData();
                
                //Populates the current constructorData with data from the past form
                this.constructorData = constructorData;

                //Sets this forms data source to the newly populated object
                studentNumberMaskedTextBox.Text = constructorData.Student.StudentNumber.ToString();
                studentNumberMaskedTextBox_Leave(null, null);
            }
        }

        /// <summary>
        /// given: open grading form passing constructor data
        /// </summary>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            populateConstructorData();

            Grading grading = new Grading(constructorData);
            grading.MdiParent = this.MdiParent;
            grading.Show();
            this.Close();
        }

        /// <summary>
        /// given: open history form passing data
        /// </summary>
        private void lnkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            populateConstructorData();

            History history = new History(constructorData);
            history.MdiParent = this.MdiParent;
            history.Show();
            this.Close();
        }
        /// <summary>
        /// given:  opens form in top right of frame
        /// </summary>
        private void StudentData_Load(object sender, EventArgs e)
        {
            //keeps location of form static when opened and closed
            this.Location = new Point(0, 0);
        }

        /// <summary>
        /// Handles the click event 
        /// </summary>
        private void studentNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {
            if (studentNumberMaskedTextBox.Text != "") 
            {
                //Parses any text entered by the user into an integer data type
                long studentNumber = long.Parse(studentNumberMaskedTextBox.Text);

                //Grabs the entered student based off the student number entered
                Student student = db.Students
                                    .Where(x => x.StudentNumber == studentNumber)
                                    .SingleOrDefault();

                if (student == null)
                {
                    //Message box
                    string message = $"Student {studentNumber} does not exist.";
                    string caption = "Invalid Student Number";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);

                    //Sets the link labels to disabled
                    lnkDetails.Enabled = false;
                    lnkUpdate.Enabled = false;

                    //Sets focus to the student number label
                    this.ActiveControl = studentNumberMaskedTextBox;

                    //Reset BindingSource object
                    studentBindingSource.DataSource = typeof(Student);
                    registrationBindingSource.DataSource = typeof(Registration);
                }
                else
                {
                    //Sets the student data source to the student object created above.
                    studentBindingSource.DataSource = student;

                    IQueryable<Registration> registration = db.Registrations
                                                              .Where
                                                              (x => x.StudentId == student.StudentId);

                    //Sets the registration data source to the registration object created above.
                    registrationBindingSource.DataSource = registration.ToList();

                    /*If the registration object is not null, set the registration number
                    combo box to the constructorsData's registration number.
                    This is done to populate this form with the same data as entered previous.*/
                    if(constructorData.Registration != null)
                    {
                        registrationNumberComboBox.Text = constructorData.
                            Registration.RegistrationNumber.ToString();
                    }

                    if (registration.Any())
                    {
                        LinkButtonVisibility(true);
                    }
                    else
                    {
                        LinkButtonVisibility(false);
                        registrationBindingSource.DataSource = typeof(Registration);
                    }
                }
            }
        }
    

        /// <summary>
        /// A method that enables or disables the link labels
        /// </summary>
        /// <param name="enabled">True if enabled, false if disabled</param>
        public void LinkButtonVisibility(bool enabled)
        {
            if (enabled == true)
            {
                lnkDetails.Enabled = true;
                lnkUpdate.Enabled = true;
            }
            else if (enabled == false)
            {
                lnkDetails.Enabled = false;
                lnkUpdate.Enabled = false;
            }
        }

        /// <summary>
        /// A method that populates the constructorData object with the current
        /// student and registration records
        /// </summary>
        private void populateConstructorData()
        {
            /*Sets the constructor data to the created student object above.
            Acts as C#'s version of Sessions. This student object will be passed around the form
            until a new user is entered or the form is closed.*/
            constructorData.Student = (Student)studentBindingSource.Current;
            constructorData.Registration = (Registration)registrationBindingSource.Current;
        }
    }
}