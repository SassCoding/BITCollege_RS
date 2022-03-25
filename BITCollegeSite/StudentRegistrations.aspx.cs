using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BITCollege_RS.Models;
using BITCollege_RS.Data;

namespace BITCollegeSite
{
    public partial class StudentRegistrations : System.Web.UI.Page
    {
        BITCollege_RSContext db = new BITCollege_RSContext();

        /// <summary>
        /// Populates the data grid view control with data as long as 
        /// the user has successfully logged in.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!this.Page.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    try
                    {
                        //To grab the selected student from the db via student number.
                        string userLogin = Page.User.Identity.Name;
                        string studentID = userLogin.Substring(0, userLogin.IndexOf('@'));
                        long parsedID = long.Parse(studentID);

                        Student studentQuery = db.Students
                                                .Where(x => x.StudentNumber == parsedID)
                                                .SingleOrDefault();

                        Student selectedStudent = studentQuery;
                        Session["student"] = selectedStudent;

                        lblStudentName.Text = selectedStudent.FullName;

                        //To grab a collection of registration records associated
                        //with the student logged into the site and bind to the grid view.

                        IQueryable<Registration> registrationQuery = db.Registrations
                                                                       .Where(x => x.StudentId == selectedStudent.StudentId)
                                                                       .OrderBy(x => x.CourseId);                                                                       

                        Session["registrations"] = registrationQuery;
                        
                        dgvCourses.DataSource = registrationQuery.ToList();
                        this.DataBind();
                        dgvCourses.Visible = true;
                    }
                    catch (Exception exception)
                    {
                        //Catches exceptions and outputs them to them
                        //to the error message label
                        lblError.Text = "Error: " + exception.Message;
                        lblError.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the selected index changed event of the courses data grid.
        /// </summary>
        protected void dgvCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["selectedCourseNumber"] = this.dgvCourses.Rows[this.dgvCourses.SelectedIndex].Cells[1].Text;
            Response.Redirect("~/ViewDrop");
        }

        /// <summary>
        /// Handles the click event of the register link button.
        /// </summary>
        protected void lnkbtnRegister_Click(object sender, EventArgs e)
        {
            Session["courseNumber"] = this.dgvCourses.Rows[1].Cells[1].Text;
            Response.Redirect("~/CourseRegistration");
        }
    }
}