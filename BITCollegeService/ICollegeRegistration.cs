using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BITCollegeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICollegeRegistration" in both code and config file together.
    [ServiceContract]
    public interface ICollegeRegistration
    {
        /// <summary>
        /// Allows for dropping of courses.
        /// </summary>
        /// <param name="registrationId">Registration ID</param>
        /// <returns></returns>
        [OperationContract]
        bool DropCourse(int registrationId);

        /// <summary>
        /// Allows for registering of courses.
        /// </summary>
        /// <param name="studentId">ID of a given student.</param>
        /// <param name="courseId">ID of a given course.</param>
        /// <param name="notes">Notes</param>
        /// <returns></returns>
        [OperationContract]
        int RegisterCourse(int studentId, int courseId, string notes);

        /// <summary>
        /// Allows for updating of a students grade.
        /// </summary>
        /// <param name="grade">A student's grade.</param>
        /// <param name="registrationId">A student's registration Id.</param>
        /// <param name="notes">Notes.</param>
        /// <returns></returns>
        [OperationContract]
        double? UpdateGrade(double grade, int registrationId, string notes);
    }
}
