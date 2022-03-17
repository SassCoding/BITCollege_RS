using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BITCollege_RS.Data
{
    public class BITCollege_RSContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BITCollege_RSContext() : base("name=BITCollege_RSContext")
        {
        }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.AcademicProgram> AcademicPrograms { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.GradePointState> GradePointStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.Registration> Registrations { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.AuditCourse> AuditCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.HonoursState> HonoursStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.MasteryCourse> MasteryCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.ProbationState> ProbationStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.RegularState> RegularStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.SuspendedState> SuspendedStates { get; set; }
        
        public System.Data.Entity.DbSet<BITCollege_RS.Models.GradedCourse> GradedCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.StudentCard> StudentCards { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.NextUniqueNumber> NextUniqueNumbers { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.NextAuditCourse> NextAuditCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.NextGradedCourse> NextGradedCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.NextMasteryCourse> NextMasteryCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.NextRegistration> NextRegistrations { get; set; }

        public System.Data.Entity.DbSet<BITCollege_RS.Models.NextStudent> NextStudents { get; set; }
    }
}
