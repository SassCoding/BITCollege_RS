using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BITCollege_RS.Data;
using BITCollege_RS.Models;

namespace BITCollegeWindows
{
    /// <summary>
    /// given:TO BE MODIFIED
    /// this class is used to capture data to be passed
    /// among the windows forms
    /// </summary>
    public class ConstructorData
    {
        public Student Student { get; set; }
        public Registration Registration { get; set; }
    }
}
