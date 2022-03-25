using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BITCollege_RS.Data;
using BITCollege_RS.Models;

namespace BITCollegeSite
{
    public partial class CourseRegistration : System.Web.UI.Page
    {
        BITCollege_RSContext db = new BITCollege_RSContext();

        /// <summary>
        /// Handles the load event of the page.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.User.Identity.IsAuthenticated)
            {
                try
                {
                    PageLoad();
                }
                catch (Exception exception)
                {
                    lblError.Text = exception.Message;
                }
            }
        }

        /// <summary>
        /// Handles the click of the register link button event.
        /// </summary>
        protected void lnkbtnRegister_Click(object sender, EventArgs e)
        {
            txtboxNotes.Enabled = true;
            Page.Validate();

            if (Page.IsValid)
            {
                //Create a student and a course object to register
                Student studentToRegister = (Student)Session["Student"];
                int courseSelected = int.Parse(ddlCourses.Text);
                Course courseToRegister = db.Courses
                                            .Where(x => x.CourseId == courseSelected)
                                            .SingleOrDefault();

                //Grab the necessary data to pass to RegisterCourse
                int studentID = studentToRegister.StudentId;
                int courseID = courseToRegister.CourseId;
                string notes = txtboxNotes.Text;

                //Create a Web Service Object
                CollegeRegistrationService.CollegeRegistrationClient collegeService =
                new CollegeRegistrationService.CollegeRegistrationClient();

                //Runs the register Course method from the service
                //Stores the return code into a variable
                int returnCode = collegeService.RegisterCourse(studentID, courseID, notes);

                //Based off return code, return a related message.
                switch (returnCode)
                {
                    case -100:
                        lblError.Visible = true;
                        lblError.Text = "Error: Student has a pending registration.";
                        break;
                    case -200:
                        lblError.Visible = true;
                        lblError.Text = "Error: The students has surpassed the maximum attempts for this course.";
                        break;
                    case -300:
                        lblError.Visible = true;
                        lblError.Text = "Error: Unknown error.";
                        break;
                    case 0:
                        Response.Redirect("~/StudentRegistrations");
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the clicking of the return link button event.
        /// </summary>
        protected void lnkbtnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StudentRegistrations");
        }

        /// <summary>
        /// A method that populates the name drop down list
        /// with courses that are available to the student.
        /// </summary>
        private void PageLoad()
        {
            if (!IsPostBack)
            {
                try
                {
                    //Creating a student object using data from a session
                    //and assigning the name to the label.
                    Student selectedStudent = (Student)Session["student"];
                    string courseNumber = (string)Session["courseNumber"];
                    lblStudentName.Text = selectedStudent.FullName;

                    //Creating a course object according to the course number stored in session.
                    Course course = db.Courses
                                      .Where(x => x.CourseNumber == courseNumber)
                                      .SingleOrDefault();

                    //Creating a list of courses
                    IQueryable<Course> listOfCourses = db.Courses
                                                         .Where(x => x.AcademicProgramId 
                                                                       == selectedStudent.AcademicProgramId);                             

                    //Binding the data to the list
                    ddlCourses.DataSource = listOfCourses.ToList();
                    ddlCourses.DataTextField = "Title";
                    ddlCourses.DataValueField = "CourseId";
                    this.DataBind();
                }
                catch(Exception exception)
                {
                    lblError.Text = exception.Message;
                    lblError.Visible = true;
                }
            }
        }
    }
}