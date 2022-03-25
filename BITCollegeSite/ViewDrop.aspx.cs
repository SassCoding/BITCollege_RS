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
    public partial class ViewDrop : System.Web.UI.Page
    {
        BITCollege_RSContext db = new BITCollege_RSContext();
        
        /// <summary>
        /// Populates the details view with registration data.
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
        /// Handles the page index changing event.
        /// </summary>
        protected void dtvDropReg_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {
            dtvDropReg.PageIndex = e.NewPageIndex;
            PageLoad();
        }

        /// <summary>
        /// Handles the drop course link click event.Need to fix!!
        /// </summary>
        protected void lnkbtnDropCourse_Click(object sender, EventArgs e)
        {
            CollegeRegistrationService.CollegeRegistrationClient collegeService =
                new CollegeRegistrationService.CollegeRegistrationClient();
            
            try
            {
                bool courseDropped = collegeService.DropCourse(int.Parse(this.dtvDropReg.Rows[0].Cells[1].Text));

                if (courseDropped == true)
                {
                    Response.Redirect("~/StudentRegistrations");
                }
            }
            catch (Exception exception)
            {
                lblError.Text = "Error: " + exception.Message;
            }
        }

        /// <summary>
        /// Handles the click event of the return link.
        /// </summary>
        protected void lnkbtnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StudentRegistrations");
        }

        /// <summary>
        /// Populates the details view with registration data.
        /// </summary>
        private void PageLoad()
        {
            if (!IsCallback)
            {
                if (!Page.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Account/Login");
                }
                else
                {
                    //Making use of session variable to pass the student from StudentRegistration
                    Student selectedStudent = (Student)Session["student"];
                    String selectedCourseNumber = Session["selectedCourseNumber"].ToString();

                    Course query = db.Courses
                                     .Where(x => x.CourseNumber == selectedCourseNumber)
                                     .SingleOrDefault();

                    IQueryable<Registration> regQuery = db.Registrations
                                                          .Where(x => x.StudentId == selectedStudent.StudentId)
                                                          .Where(x => x.CourseId == query.CourseId);
                    dtvDropReg.AllowPaging = true;

                    Session["registration"] = regQuery;
                    dtvDropReg.DataSource = regQuery.ToList();
                    this.DataBind();

                    if (this.dtvDropReg.Rows[4].Cells[1].Text == "&nbsp;")
                    {
                        lnkbtnDropCourse.Visible = true;
                    }
                }
            }            
        }
    }
}