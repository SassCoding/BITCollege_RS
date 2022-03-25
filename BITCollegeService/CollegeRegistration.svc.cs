/*
 * Name: Ryan Sass
 * Program: Business Information Technology
 * Course: ADEV-3008 Programming 3
 * Created: March 3, 2022
 * Updated: March 10, 2022
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BITCollege_RS.Data;
using BITCollege_RS.Models;
using Utility;

namespace BITCollegeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CollegeRegistration" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CollegeRegistration.svc or CollegeRegistration.svc.cs at the Solution Explorer and start debugging.
    public class CollegeRegistration : ICollegeRegistration
    {
        private BITCollege_RSContext db = new BITCollege_RSContext();

        /// <summary>
        /// Drops a course from a students registrations.
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns>True if the course drop was successful, else false.</returns>
        bool ICollegeRegistration.DropCourse(int registrationId)
        {
            bool courseDropped = false;

            try
            {
                Registration courseToDrop = db.Registrations.Find(registrationId);

                db.Registrations.Remove(courseToDrop);
                db.SaveChanges();

                courseDropped = true;

                return courseDropped;
            }
            catch(Exception)
            {
                return courseDropped;
            }
        }

        /// <summary>
        /// Registers a student for a given course.
        /// </summary>
        /// <param name="studentId">A student's ID</param>
        /// <param name="courseId">A course's ID</param>
        /// <param name="notes">Notes</param>
        /// <returns>Error codes -100, -200, -300 or 0 if successful.</returns>
        int ICollegeRegistration.RegisterCourse(int studentId, int courseId, string notes)
        {

            try
            {
                Registration incompleteCourseRegistration = db.Registrations
                    .Where(x => x.Grade == null 
                    && x.StudentId == studentId 
                    && x.CourseId == courseId)
                    .SingleOrDefault();

                //If the student has pending registration, return error -100.
                if (incompleteCourseRegistration != null)
                {
                    return -100;
                }

                //Finds the amount of times the student has taken a course.
                int courseAttempts = db.Registrations
                    .Count(x => x.StudentId == studentId
                    && x.CourseId == courseId);

                //Finds the maximum amount of attempts for the Mastery Course.
                int? maxAttempts = db.MasteryCourses
                    .Where(x => x.CourseId == courseId)
                    .Select(select => select.MaximumAttempts).SingleOrDefault();

                //If the student has exceeded maximum number of attempts, return -200.
                if (maxAttempts != null)
                {
                    if (courseAttempts >= maxAttempts && maxAttempts > 0)
                    {
                        return -200;
                    }
                }

                //If all conditions above are satisfied, add the course.
                BITCollege_RS.Models.Registration newRegistration = new BITCollege_RS.Models.Registration();

                newRegistration.StudentId = studentId;
                newRegistration.CourseId = courseId;
                newRegistration.Notes = notes;
                newRegistration.SetNextRegistrationNumber();
                newRegistration.RegistrationDate = DateTime.Now;

                Course registeredCourse = db.Courses
                    .Where(x => x.CourseId == newRegistration.CourseId)
                    .SingleOrDefault();

                Student student = db.Students
                    .Where(x => x.StudentId == studentId)
                    .SingleOrDefault();

                GradePointState state = db.GradePointStates
                    .Where(x => x.GradePointStateId == student.GradePointStateId)
                    .SingleOrDefault();

                student.OutstandingFees += state.TuitionRateAdjustment(student) * registeredCourse.TuitionAmount;

                db.Registrations.Add(newRegistration);
                db.SaveChanges();
                return 0;                                             
                                
            }
            catch(Exception e)
            {
                return -300;
            }
        }

        /// <summary>
        /// Updates the grade of a given student.
        /// </summary>
        /// <param name="grade">Given grade</param>
        /// <param name="registrationId">given registration Id</param>
        /// <param name="notes">Notes</param>
        /// <returns>A students grade point average.</returns>
        double? ICollegeRegistration.UpdateGrade(double grade, int registrationId, string notes)
        {
            Registration registrationRecord = db.Registrations
                .Where(x => x.RegistrationId == registrationId)
                .SingleOrDefault();

            registrationRecord.Grade = grade;
            registrationRecord.Notes = notes;
            
            db.SaveChanges();

            double? gradePointAverage = CalculateGradePointAverage(registrationRecord.Student.StudentId);

            Student student = db.Students
                .Where(x => x.StudentId == registrationRecord.Student.StudentId)
                .SingleOrDefault();
            
            student.GradePointAverage = gradePointAverage;
            db.SaveChanges();

            return gradePointAverage;
        }

        /// <summary>
        /// Calculates a given students grade point average.
        /// </summary>
        /// <param name="studentId">A given student's student ID.</param>
        /// <returns>A calculated grade point average.</returns>
        private double? CalculateGradePointAverage(int studentId)
        {
            double totalGradePointValue = 0;
            double totalCreditHours = 0;
            double? gradePointAverage, grades;
            double gradesValue, gradePointValue, result;

            IQueryable<Registration> registrations = db.Registrations.Where(x => x.Grade != null)
                                                                     .Where(x => x.StudentId == studentId);

            foreach (Registration registration in registrations.ToList())
            {
                double creditHours = registration.Course.CreditHours;
                string typeOfCourse = registration.Course.CourseType;
                CourseType courseType = BusinessRules.CourseTypeLookup(typeOfCourse);

                if (typeOfCourse != "Audit")
                {
                    grades = registration.Grade;

                    gradesValue = (double)grades;
                    gradePointValue = BusinessRules.GradeLookup(gradesValue, courseType);

                    result = gradePointValue * creditHours;
                    totalGradePointValue += result;

                    totalCreditHours += creditHours;
                }
            }

            if (totalCreditHours == 0)
            {
                gradePointAverage = null;
            }
            else
            {
                gradePointAverage = totalGradePointValue / totalCreditHours;
            }

            Student student = db.Students.Find(studentId);

            student.GradePointAverage = gradePointAverage;
            student.ChangeState();
            db.SaveChanges();

            return gradePointAverage;
        }
    }
}
