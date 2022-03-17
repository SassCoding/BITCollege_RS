/*
 * Name: Ryan Sass
 * Program: Business Information Technology
 * Course: ADEV-3008 Programming 3
 * Created: January 7, 2022
 * Updated: January 14, 2022
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using BITCollege_RS.Data;
using System.Data.SqlClient;

namespace BITCollege_RS.Models
{
    /// <summary>
    /// A model that represents the Suspended State.
    /// </summary>
    public class SuspendedState : GradePointState
    {
        private static SuspendedState suspendedState;

        /// <summary>
        /// Constructs an instance of the SuspendedState class.
        /// </summary>
        private SuspendedState()
        {
            this.LowerLimit = 0.00;
            this.UpperLimit = 1.00;
            this.TuitionRateFactor = 1.1;
        }

        /// <summary>
        /// Returns an instance of a SuspendedState object.
        /// </summary>
        /// <returns>Instance of suspended state.</returns>
        public static SuspendedState GetInstance()
        {
            if(suspendedState == null)
            {
                suspendedState = db.SuspendedStates.SingleOrDefault();
                if(suspendedState == null)
                {
                    suspendedState = new SuspendedState();
                    db.SuspendedStates.Add(suspendedState);
                    db.SaveChanges();
                }
            }
            return suspendedState;
        }

        /// <summary>
        /// Calculates the adjusting tuition rate for a given student based on their GPA.
        /// </summary>
        /// <param name="student">A given student.</param>
        /// <returns>The adjusted tuition rate for a given student.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double suspendedStateTuitionRate = 1.1;

            if (student.GradePointAverage < 0.75
               && student.GradePointAverage > 0.5)
            {
                suspendedStateTuitionRate = 1.12;
            }
            if (student.GradePointAverage < 0.5)
            {
                suspendedStateTuitionRate = 1.15;
            }

            return suspendedStateTuitionRate;
        }

        /// <summary>
        /// Checks to see if a student's state has changed.
        /// </summary>
        public override void StateChangeCheck(Student student)
        {
            if(student.GradePointAverage >= UpperLimit)
            {
                student.GradePointStateId = ProbationState.GetInstance().GradePointStateId;
                db.SaveChanges();
            }
        }
    }

    /// <summary>
    /// A model that represents the Probation State.
    /// </summary>
    public class ProbationState : GradePointState
    {
        private static ProbationState probationState;

        /// <summary>
        /// Constructs an instance of the ProbationState class.
        /// </summary>
        private ProbationState()
        {
            this.LowerLimit = 1.00;
            this.UpperLimit = 2.00;
            this.TuitionRateFactor = 1.075;
        }

        /// <summary>
        /// Returns an instance of a ProbationState object.
        /// </summary>
        /// <returns>Instance of suspended state.</returns>
        public static ProbationState GetInstance()
        {
            if (probationState == null)
            {
                probationState = db.ProbationStates.SingleOrDefault();
                if (probationState == null)
                {
                    probationState = new ProbationState();
                    db.ProbationStates.Add(probationState);
                    db.SaveChanges();
                }
            }
            return probationState;
        }

        /// <summary>
        /// Calculates the adjusted tuition rate for a given student based on their GPA.
        /// </summary>
        /// <param name="student">A given student.</param>
        /// <returns>The adjusted tuition rate for a given student.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            IQueryable<Registration> registrationListing = from results
                                                           in db.Registrations
                                                           where results.Grade != null
                                                           && results.StudentId == student.StudentId
                                                           select results;

            int count = registrationListing.Count();
            double probationTuitionRateFactor = 1.075;

            if(count >= 5)
            {
                probationTuitionRateFactor = 1.035;
            }
            return probationTuitionRateFactor;
        }

        /// <summary>
        /// Checks to see if a student's state has changed.
        /// </summary>
        /// <param name="student">A given student.</param>
        public override void StateChangeCheck(Student student)
        {
            if(student.GradePointAverage >= UpperLimit)
            {
                student.GradePointStateId = RegularState.GetInstance().GradePointStateId;
                db.SaveChanges();
            }
            if(student.GradePointAverage < LowerLimit)
            {
                student.GradePointStateId = SuspendedState.GetInstance().GradePointStateId;
                db.SaveChanges();
            }
        }
    }

    /// <summary>
    /// A model that represents the Regular State.
    /// </summary>
    public class RegularState : GradePointState
    {
        private static RegularState regularState;

        /// <summary>
        /// Constructs an instance of the RegulateState class.
        /// </summary>
        private RegularState()
        {
            this.LowerLimit = 2.00;
            this.UpperLimit = 3.70;
            this.TuitionRateFactor = 1;
        }

        /// <summary>
        /// Returns an instance of a RegularState object.
        /// </summary>
        /// <returns>Instance of RegularState.</returns>
        public static RegularState GetInstance()
        {
            if (regularState == null)
            {
                regularState = db.RegularStates.SingleOrDefault();
                if (regularState == null)
                {
                    regularState = new RegularState();
                    db.RegularStates.Add(regularState);
                    db.SaveChanges();
                }
            }
            return regularState;
        }


        /// <summary>
        /// Calculates the adjusted tuition rate for a given student based on their GPA.
        /// </summary>
        /// <param name="student">A given student.</param>
        /// <returns>The adjusted tuition rate for a given student.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            return TuitionRateFactor;
        }

        /// <summary>
        /// Checks to see if a student's state has changed.
        /// </summary>
        /// <param name="student">A given student.</param>
        public override void StateChangeCheck(Student student)
        {
            if(student.GradePointAverage >= UpperLimit)
            {
                student.GradePointStateId = HonoursState.GetInstance().GradePointStateId;
                db.SaveChanges();
            }
            if(student.GradePointAverage < LowerLimit)
            {
                student.GradePointStateId = ProbationState.GetInstance().GradePointStateId;
                db.SaveChanges();
            }
        }
    }

    /// <summary>
    /// A model that represents the Honours State.
    /// </summary>
    public class HonoursState : GradePointState
    {
        private static HonoursState honoursState;

        /// <summary>
        /// Constructs an instance of the HonoursState class.
        /// </summary>
        private HonoursState()
        {
            this.LowerLimit = 3.70;
            this.UpperLimit = 4.50;
            this.TuitionRateFactor = 0.90;
        }

        /// <summary>
        /// Returns an instance of a HonoursState object.
        /// </summary>
        /// <returns>Instance of HonoursState.</returns>
        public static HonoursState GetInstance()
        {
            if (honoursState == null)
            {
                honoursState = db.HonoursStates.SingleOrDefault();
                if (honoursState == null)
                {
                    honoursState = new HonoursState();
                    db.HonoursStates.Add(honoursState);
                    db.SaveChanges();
                }
            }
            return honoursState;
        }

        /// <summary>
        /// Calculates the adjusted tuition rate for a given student based on their GPA.
        /// </summary>
        /// <param name="student">A given student.</param>
        /// <returns>The adjusted tuition rate for a given student.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            IQueryable<Registration> registrationListing = from results
                                                           in db.Registrations
                                                           where results.Grade != null
                                                           && results.StudentId == student.StudentId
                                                           select results;

            int count = registrationListing.Count();
            double honoursTuitionRateFactor = 0.9;

            if(count >= 5 && student.GradePointAverage > 4.25)
            {
                honoursTuitionRateFactor = 0.83;
            }
            else if (count >= 5)
            {
                honoursTuitionRateFactor = 0.85;
            }
            else if (student.GradePointAverage > 4.25)
            {
                honoursTuitionRateFactor = 0.88;
            }
            return honoursTuitionRateFactor;
        }

        /// <summary>
        /// Checks to see if a student's state has changed.
        /// </summary>
        /// <param name="student">A given student.</param>
        public override void StateChangeCheck(Student student)
        {
            if(student.GradePointAverage < LowerLimit)
            {
                student.GradePointStateId = RegularState.GetInstance().GradePointStateId;
                db.SaveChanges();
            }
        }
    }

    /// <summary>
    /// A class that represents a student's GPA state.
    /// </summary>
    public abstract class GradePointState
    {
        protected static BITCollege_RSContext db = new BITCollege_RSContext();

        /// <summary>
        /// Modifies or returns GPA state ID.
        /// </summary>
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int GradePointStateId { get; set; }

        /// <summary>
        /// Modifies or returns the lowest limit possible for GPA.
        /// </summary>
        [Required]
        [Display(Name = "Lower\nLimit")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double LowerLimit { get; set; }

        /// <summary>
        /// Modifies or returns the highest possible limit for a students GPA.
        /// </summary>
        [Required]
        [Display(Name = "Upper\nLimit")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double UpperLimit { get; set; }

        /// <summary>
        /// Modifies or returns the tuition rate factor that has been applied.
        /// </summary>
        [Required]
        [Display(Name = "Tuition Rate\nFactor")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double TuitionRateFactor { get; set; }

        /// <summary>
        /// Returns a description of the students current GPA state.
        /// </summary>
        [Display(Name = "Grade Point\nState")]
        public string Description 
        { 
            get
            {
                return Utility.Tools.StringEditor(GetType().Name, "State");
            }
        }

        /// <summary>
        /// Returns the rate to which the tuition is adjusted.
        /// </summary>
        /// <param name="student">Requires a student object.</param>
        /// <returns>A double representing the tuition rate adjustment for that given student object.</returns>
        public virtual double TuitionRateAdjustment(Student student)
        {
            return 0;
        }

        /// <summary>
        /// Preforms a check to see if a given student object's state has changed.
        /// </summary>
        /// <param name="student">A given student.</param>
        public virtual void StateChangeCheck(Student student)
        {

        }
        
        /// <summary>
        /// Navigation property from GPAState to student.
        /// </summary>
        public virtual ICollection<Student> Student { get; set; }
    }

    /// <summary>
    /// A class that represents a student card.
    /// </summary>
    public class StudentCard
    {
        /// <summary>
        /// Modifies or returns a student card id.
        /// </summary>
        [Required]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentCardId { get; set; }

        /// <summary>
        /// Modifies or returns a given student's id.
        /// </summary>
        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        /// <summary>
        /// Modifies or returns a students card number.
        /// </summary>
        [Required]
        [Display(Name = "Card\nNumber")]
        public long CardNumber { get; set; }

        /// <summary>
        /// Navigational property
        /// </summary>
        public virtual Student Student { get; set; }
    }

    /// <summary>
    /// A class that represents the Student table of a database.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Modifies or returns a given student's ID.
        /// </summary>
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        
        /// <summary>
        /// Modifies or returns a student's current grade point state.
        /// </summary>
        [Required]
        [ForeignKey("GradePointState")]
        public int GradePointStateId { get; set; }

        /// <summary>
        /// Modifies or returns a student's current academic program.
        /// </summary>
        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }
        
        /// <summary>
        /// Modifies or returns a students student number.
        /// </summary>
        [Display(Name = "Student\nNumber")]
        public long StudentNumber { get; set; }

        /// <summary>
        /// Modifies or returns a students first name.
        /// </summary>
        [Required]
        [StringLength(35, MinimumLength = 1, ErrorMessage = "A student's first name must be between 1-35 characters.")]
        [Display(Name = "First\nName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Modifies or returns a students last name.
        /// </summary>
        [Required]
        [StringLength(35, MinimumLength = 1, ErrorMessage = "A student's last name must be between 1-35 characters.")]
        [Display(Name = "Last\nName")]
        public string LastName { get; set; }

        /// <summary>
        /// Modifies or returns a students address.
        /// </summary>
        [Required]
        [StringLength(35, MinimumLength = 1, ErrorMessage = "Address must be between 1-35 characters.")]
        public string Address { get; set; }

        /// <summary>
        /// Modifies or returns a students City.
        /// </summary>
        [Required]
        [StringLength(35, MinimumLength = 1, ErrorMessage = "City must be between 1-35 characters.")]
        public string City { get; set; }

        /// <summary>
        /// Modifies or returns a students province.
        /// </summary>
        [Required]
        [StringLength(2)]
        [RegularExpression("^(N[BLSTU]|[AMN]B|[BQ]C|ON|PE|SK)$", ErrorMessage = "Invalid Province.")]
        public string Province { get; set; }

        /// <summary>
        /// Modifies or returns a students postal code.
        /// </summary>
        [Required]
        [Display(Name = "Postal\nCode")]
        [RegularExpression("^[ABCEGHJ-NPRSTVXY]{1}[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[ ]?[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[0-9]{1}$",
                           ErrorMessage = "Invalid Postal Code.")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Modifies or returns the date in which the students record was created.
        /// </summary>
        [Required]
        [Display(Name = "Date\nCreated")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Modifies or returns a student's grade point average.
        /// </summary>
        [Display(Name = "Grade Point\nAverage")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Range(0, 4.5)]
        public double? GradePointAverage { get; set; }

        /// <summary>
        /// Modifies or returns a student's outstanding fees.
        /// </summary>
        [Required]
        [Display(Name = "Outstanding\nFees")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double OutstandingFees { get; set; }
        
        /// <summary>
        /// Modifies or returns notes for a given student.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Returns a students full name.
        /// </summary>
        [Display(Name = "Name")]
        public string FullName 
        { 
            get
            {
                return String.Format("{0}, {1}", FirstName, LastName);
            }
        }

        /// <summary>
        /// Returns a students full address.
        /// </summary>
        [Display(Name = "Address")]
        public string FullAddress 
        { 
            get
            {
                return String.Format("{0}, {1}, {2}, {3}", Address, City, Province, PostalCode);
            }
        }

        /// <summary>
        /// Changes the state of a student object.
        /// </summary>
        public void ChangeState()
        {
            BITCollege_RSContext db = new BITCollege_RSContext();

            GradePointState query = (from results
                                     in db.GradePointStates
                                     where results.GradePointStateId == this.GradePointStateId
                                     select results).SingleOrDefault();

            GradePointState retrievedState = query;
            int comparedState = 0;

            do
            {
                retrievedState.StateChangeCheck(this);

                comparedState = retrievedState.GradePointStateId;

                retrievedState = (from results
                                  in db.GradePointStates
                                  where results.GradePointStateId == this.GradePointStateId
                                  select results).SingleOrDefault();
            }
            while (retrievedState.GradePointStateId != comparedState);
            retrievedState.TuitionRateAdjustment(this);

        }

        /// <summary>
        /// Sets the next available student number.
        /// </summary>
        public void SetNextStudentNumber()
        {
                this.StudentNumber = (long)StoredProcedure.NextNumber("NextStudent");
        }

        /// <summary>
        /// Navigational properties.
        /// </summary>
        public virtual GradePointState GradePointState { get; set; }

        public virtual AcademicProgram AcademicProgram { get; set; }

        public virtual ICollection<Registration> Registration { get; set; }

        public virtual ICollection<StudentCard> StudentCard { get; set; }
    }

    /// <summary>
    /// A class that represents the Academic Program table of a database.
    /// </summary>
    public class AcademicProgram
    {
        /// <summary>
        /// Modifies or returns a students academic program ID.
        /// </summary>
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AcademicProgramId { get; set; }

        /// <summary>
        /// Modifies or returns a students current program acronym.
        /// </summary>
        [Required]
        [Display(Name = "Program")]
        public string ProgramAcronym { get; set; }

        /// <summary>
        /// Modifies or returns a students program name.
        /// </summary>
        [Required]
        [Display(Name = "Program\nName")]
        public string Description { get; set; }

        /// <summary>
        /// Navigational properties.
        /// </summary>
        public virtual ICollection<Student> Student { get; set; }
        public virtual ICollection<Course> Course { get; set; }
    }

    /// <summary>
    /// A class that represents the Registration table of a database.
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Modifies or returns a registration ID.
        /// </summary>
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RegistrationId { get; set; }

        /// <summary>
        /// Modifies or returns a student ID.
        /// </summary>
        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        /// <summary>
        /// Modifies or returns a course.
        /// </summary>
        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        /// <summary>
        /// Modifies or returns a registration number.
        /// </summary>
        [Display(Name = "Registration\nNumber")]
        public long RegistrationNumber { get; set; }

        /// <summary>
        /// Modifies or returns a given students registration date. 
        /// </summary>
        [Required]
        [Display(Name = "Registration\nDate")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Modifies or returns a grade.
        /// </summary>
        [DisplayFormat(NullDisplayText = "Ungraded")]
        [Range(0, 1)]
        public double? Grade { get; set; }

        /// <summary>
        /// Modifies or returns notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Sets the next available registration number.
        /// </summary>
        public void SetNextRegistrationNumber()
        {
                this.RegistrationNumber = (long)StoredProcedure.NextNumber("NextRegistration");
        }

        /// <summary>
        /// Navigational properties.
        /// </summary>
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }

    /// <summary>
    /// A class that represents the Course table of a database.
    /// </summary>
    public abstract class Course
    {
        /// <summary>
        /// Modifies or returns a course ID.
        /// </summary>
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CourseId { get; set; }

        /// <summary>
        /// Modifies or returns an academic program ID.
        /// </summary>
        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }

        /// <summary>
        /// Modifies or returns a course number.
        /// </summary>
        [Display(Name = "Course\nNumber")]
        public string CourseNumber { get; set; }

        /// <summary>
        /// Modifies or returns a course title. 
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Modifies or returns a courses total credit hours.
        /// </summary>
        [Required]
        [Display(Name = "Credit\nHours")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double CreditHours { get; set; }

        /// <summary>
        /// Modifies or returns a courses tuition amount.
        /// </summary>
        [Required]
        [Display(Name = "Tuition\nAmount")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double TuitionAmount { get; set; }

        /// <summary>
        /// Modifies or returns the type of course.
        /// </summary>
        [Display(Name = "Course\nType")]
        public string CourseType 
        { 
            get
            {
                return Utility.Tools.StringEditor(GetType().Name, "Course");
            }
        }

        /// <summary>
        /// Modifies or returns notes.
        /// </summary>
        public string Notes { get; set; }

        public abstract void SetNextCourseNumber();

        /// <summary>
        /// Navigational properties.
        /// </summary>
        public virtual AcademicProgram AcademicProgram { get; set; }

        public virtual ICollection<Registration> Registration { get; set; }
    }

    /// <summary>
    /// A model that represents the Graded Course of a database.
    /// </summary>
    public class GradedCourse : Course
    {
        /// <summary>
        /// Modifies or returns an assignment's total weight.
        /// </summary>
        [Required]
        [Display(Name = "Assignment\nWeight")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double AssignmentWeight { get; set; }

        /// <summary>
        /// Modifies or returns an midterm's total weight.
        /// </summary>
        [Required]
        [Display(Name = "Midterm\nWeight")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double MidtermWeight { get; set; }

        /// <summary>
        /// Modifies or returns a final exam's weight.
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        [Display(Name = "Final\nWeight")]
        public double FinalWeight { get; set; }

        /// <summary>
        /// Sets the next available course number.
        /// </summary>
        public override void SetNextCourseNumber()
        {
            this.CourseNumber = $"G-{StoredProcedure.NextNumber("NextGradedCourse")}";
        }
    }

    /// <summary>
    /// A model that represents the Audit Course table of a database.
    /// </summary>
    public class AuditCourse : Course
    {
        public override void SetNextCourseNumber()
        {
            this.CourseNumber = $"A-{StoredProcedure.NextNumber("NextAuditCourse")}";
        }
    }

    /// <summary>
    /// A model that represents the Mastery Course table of a database.
    /// </summary>
    public class MasteryCourse : Course
    {
        /// <summary>
        /// Modifies or returns the maximum number of attempts for a course.
        /// </summary>
        [Required]
        [Display(Name = "Maximum\nAttempts")]
        public int MaximumAttempts { get; set; }

        /// <summary>
        /// Sets the next available Course number.
        /// </summary>
        public override void SetNextCourseNumber()
        {
            this.CourseNumber = $"M-{StoredProcedure.NextNumber("NextMasteryCourse")}";
        }
    }

    /// <summary>
    /// A super class for Next Unique number.
    /// </summary>
    public abstract class NextUniqueNumber
    {
        protected static BITCollege_RSContext db = new BITCollege_RSContext();

        /// <summary>
        /// Modifies or returns the next unique number id.
        /// </summary>
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int NextUniqueNumberId { get; set; }

        /// <summary>
        /// Modifies or returns the next available number.
        /// </summary>
        [Required]
        public long NextAvailableNumber { get; set; }
    }

    /// <summary>
    /// A model that represents the Next Graded Course table of a database.
    /// </summary>
    public class  NextGradedCourse : NextUniqueNumber
    {
        private static NextGradedCourse nextGradedCourse;

        private NextGradedCourse()
        {
            this.NextAvailableNumber = 200000;
        }

        public static NextGradedCourse GetInstance()
        {
            if(nextGradedCourse == null)
            {
                nextGradedCourse = db.NextGradedCourses.SingleOrDefault();
                
                if(nextGradedCourse == null)
                {
                    nextGradedCourse = new NextGradedCourse();
                    db.NextGradedCourses.Add(nextGradedCourse);
                    db.SaveChanges();
                }
            }
            return nextGradedCourse;
        }
    }

    /// <summary>
    /// A model that represents the Next Graded Course table of a database.
    /// </summary>
    public class NextStudent : NextUniqueNumber
    {
        private static NextStudent nextStudent;

        private NextStudent()
        {
            this.NextAvailableNumber = 20000000;
        }

        public static NextStudent GetInstance()
        {

            if(nextStudent == null)
            {
                nextStudent = db.NextStudents.SingleOrDefault();

                if(nextStudent == null)
                {
                    nextStudent = new NextStudent();
                    db.NextStudents.Add(nextStudent);
                    db.SaveChanges();
                }
            }
            return nextStudent;
        }
    }

    public class NextAuditCourse : NextUniqueNumber
    {
        private static NextAuditCourse nextAuditCourse;

        private NextAuditCourse()
        {
            this.NextAvailableNumber = 2000;
        }

        public static NextAuditCourse GetInstance()
        {
            if(nextAuditCourse == null)
            {
                nextAuditCourse = db.NextAuditCourses.SingleOrDefault();

                if(nextAuditCourse == null)
                {
                    nextAuditCourse = new NextAuditCourse();
                    db.NextAuditCourses.Add(nextAuditCourse);
                    db.SaveChanges();
                }
            }
            return nextAuditCourse;
        }
    }

    public class NextRegistration : NextUniqueNumber
    {
        private static NextRegistration nextRegistration;

        private NextRegistration()
        {
            this.NextAvailableNumber = 700;
        }

        public static NextRegistration GetInstance()
        {
            if(nextRegistration == null)
            {
                nextRegistration = db.NextRegistrations.SingleOrDefault();

                if(nextRegistration == null)
                {
                    nextRegistration = new NextRegistration();
                    db.NextRegistrations.Add(nextRegistration);
                    db.SaveChanges();
                }
            }
            return nextRegistration;
        }
    }

    public class NextMasteryCourse : NextUniqueNumber
    {
        private static NextMasteryCourse nextMasteryCourse;

        private NextMasteryCourse()
        {
            this.NextAvailableNumber = 20000;
        }

        public static NextMasteryCourse GetInstance()
        {
            if(nextMasteryCourse == null)
            {
                nextMasteryCourse = db.NextMasteryCourses.SingleOrDefault();

                if(nextMasteryCourse == null)
                {
                    nextMasteryCourse = new NextMasteryCourse();
                    db.NextMasteryCourses.Add(nextMasteryCourse);
                    db.SaveChanges();
                }

            }
            return nextMasteryCourse;
        }
    }

    /// <summary>
    /// A static class that executes stored procedures.
    /// </summary>
    public static class StoredProcedure
    {
        /// <summary>
        /// Executes the stored procedure "next_number" and
        /// returns the outcome.
        /// </summary>
        /// <param name="discriminator"></param>
        /// <returns>Long or null</returns>
        public static long? NextNumber(string discriminator)
        {            
            try
            {
                // Establishes that the database used will be BITCollege_RSContext
                SqlConnection connection = new SqlConnection("Data Source=localhost; " +
                                           "Initial Catalog=BITCollege_RSContext;Integrated Security=True");

                // The return value can be null or a long.
                long? returnValue = 0;

                // Loads the "next_number" procedure
                SqlCommand storedProcedure = new SqlCommand("next_number", connection);

                // Sets the command type to stored procedure.
                storedProcedure.CommandType = CommandType.StoredProcedure;

                // Adds parameters "Discriminator" and New Val to the procedure.
                storedProcedure.Parameters.AddWithValue("@Discriminator", discriminator);
                SqlParameter outputParameter = new SqlParameter("@NewVal", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output 
                };
                
                // Adding the output parameter to the procedure.
                storedProcedure.Parameters.Add(outputParameter);
                
                // Opens a connection
                connection.Open();
                
                // Executes the statement.
                storedProcedure.ExecuteNonQuery();
                
                //Closes the connection
                connection.Close();
                
                //Assign the output variables value to a variable named return value and returning it.
                returnValue = (long?)outputParameter.Value;
                return returnValue;
            }
            catch(Exception)
            {
                //Returns null if the above code fails.
                return null;
            }
        }
    }
}