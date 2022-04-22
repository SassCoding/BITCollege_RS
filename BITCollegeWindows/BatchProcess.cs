using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using BITCollege_RS.Data;
using BITCollege_RS.Models;
using Utility;

namespace BITCollegeWindows
{
    /// <summary>
    /// A class that process' an XML file for data.
    /// </summary>
    class BatchProcess
    {
        string logFileName, logData;
        BITCollege_RSContext db = new BITCollege_RSContext();


        /// <summary>
        /// A property that represents the name of the file being processed.
        /// </summary>
        public string inputFileName { get; set; }

        /// <summary>
        /// A method that will process all detail errors found within the current
        /// file being processed.
        /// </summary>
        /// <param name="beforeQuery"></param>
        /// <param name="afterQuery"></param>
        /// <param name="message"></param>
        private void ProcessErrors(IEnumerable<XElement> beforeQuery, IEnumerable<XElement> afterQuery, string message)
        {
            IEnumerable<XElement> differenceQuery =
                beforeQuery.Except(afterQuery);

            foreach (XElement failedValidationElement in differenceQuery)
            {
                logData += $"======Error======\n" +
                    $"File: {inputFileName}\n" +
                    $"Program: {failedValidationElement.Element("program")}\n" +
                    $"Student Number: {failedValidationElement.Element("student_no")}\n" +
                    $"Course Number: {failedValidationElement.Element("course_no")}\n" +
                    $"Registration Number: {failedValidationElement.Element("registration_no")}\n" +
                    $"Type: {failedValidationElement.Element("type")}\n" +
                    $"Grade: {failedValidationElement.Element("grade")}\n" +
                    $"Notes: {failedValidationElement.Element("notes")}\n" +
                    $"Nodes: {failedValidationElement.Nodes().Count()}\n" +
                    $"{message}\n" +
                    $"============\n\n";
            }
        }

        /// <summary>
        /// A method used to verify the attributes of the xml file's
        /// root element. If any attributes produce and error,
        /// the file is not processed.
        /// </summary>
        private void ProcessHeader()
        {
            //Defines a XDocument object and loads the .xml file
            XDocument xDocument;
            xDocument = XDocument.Load(inputFileName);

            //Defines a XElement object and populates the object with
            //data from the root element of the .xml file loaded above.
            XElement rootElement = xDocument.Element("student_update");

            //Attribute count check
            if (rootElement.Attributes().Count() != 3)
            {
                throw new Exception($"Incorrect number of root attributes for file {inputFileName}\n");
            }

            //Date check
            if (!DateTime.Parse(rootElement.Attribute("date").Value)
                .Equals(DateTime.Today))
            {
                throw new Exception($"The date attribute for {inputFileName} is incorrect.\n");
            }

            //Program attribute check
            XAttribute programAttribute = rootElement.Attribute("program");
            string programValue = programAttribute.Value;

            //Query to check the program acronym in the file against the acronyms
            //in the database.
            AcademicProgram academicProgram = db.AcademicPrograms
                                                              .Where(x => x.ProgramAcronym == programValue).SingleOrDefault();

            if(academicProgram == null)
            {
                throw new Exception($"The program {programValue} does not exist.");
            }

            //Checksum check
            //Query to get all of the student numbers
            IEnumerable<XElement> studentNumbers = rootElement.Descendants()
                                                                .Where(x => x.Name == ("student_no"));
            
            //Sum up the values of the student numbers to get our own
            //check sum value correctly.
            long checkSum = studentNumbers.Sum(x => Int32.Parse(x.Value));

            //Get the checksum amount of the xml file
            int fileCheckSum = Convert.ToInt32(rootElement.Attribute("checksum").Value);

            if (checkSum != fileCheckSum)
            {
                throw new Exception($"The check sum in file {inputFileName} is not correct.");
            }
        }

        /// <summary>
        /// A method used to verify the contents of the detail records in the input file.
        /// </summary>
        private void ProcessDetails()
        {
            XDocument xDocument;
            xDocument = XDocument.Load(inputFileName);

            //Gathering collections needed throughout validations.
            XElement rootElement = xDocument.Descendants("student_update").SingleOrDefault();
            
            IEnumerable<long> tempStudentNumbers = db.Students.Select(x => x.StudentNumber);
            IEnumerable<string> tempCourseNumbers = db.Courses.Select(x => x.CourseNumber);
            IEnumerable<long> tempRegistrationNumbers = db.Registrations.Select(x => x.RegistrationNumber);

            List<long> studentNumbers = tempStudentNumbers.ToList();
            List<string> courseNumbers = tempCourseNumbers.ToList();
            List<long> registrationNumbers = tempRegistrationNumbers.ToList();


            //Returns a collection of elements where the element name is transaction.
            IEnumerable<XElement> transactions = xDocument.Descendants().Where(x => x.Name == ("transaction"));

            //Returns a collection of transaction elements where the transactions contain 7 child elements.
            IEnumerable<XElement> childElements = transactions.Where(x => x.Nodes().Count() == 7);
            ProcessErrors(transactions, childElements, "The number of child elements in the XML file must equal 7.");

            //Returns a collection of transaction elements where the transactions program element
            //matches the root element's program attribute.
            IEnumerable<XElement> programMatch = childElements.Where(x => rootElement.Attribute("program").Value == 
                                                                                    x.Element("program").Value);
            ProcessErrors(childElements, programMatch, "The program element did not match the " +
                "program attribute of the root element.");

            //Returns a collection of transaction elements where the transaction type is numeric.
            IEnumerable<XElement> checkTransactionType = programMatch.Where(x => Utility.Numeric.IsNumeric(x.Element("type").Value, System.Globalization.NumberStyles.Integer));
            ProcessErrors(programMatch, checkTransactionType, "The type element was not numeric.");

            //Returns a collection of transaction elements where the grade element is either
            //numeric or has a value of '*'. 
            IEnumerable<XElement> checkGrade = checkTransactionType.Where(x => Utility.Numeric.IsNumeric(x.Element("grade").Value, System.Globalization.NumberStyles.Float) || x.Element("grade").Value == "*");
            ProcessErrors(checkTransactionType, checkGrade, "The grade element was not numeric or an asterisk '*'.");

            //Returns a collection of transaction elements where the type element is either a 1 or a 2.
            IEnumerable<XElement> checkType = checkGrade.Where(x => int.Parse(x.Element("type").Value) == 1 || int.Parse(x.Element("type").Value) == 2);
            ProcessErrors(checkGrade, checkType, "The transaction type was neither a 1 or 2.");

            //Returns a collection of transaction elements.
            //When the grade type element is 1, the grade must be '*'.
            //When the grade type element is 2, the grade must have a value of 1-100 inclusive.
            IEnumerable<XElement> checkGradeType = checkType.Where(x => int.Parse(x.Element("type").Value) == 1 && x.Element("grade").Value == "*" || int.Parse(x.Element("type").Value) == 2 && double.Parse(x.Element("grade").Value) >= 0 && double.Parse(x.Element("grade").Value) <= 100);
            ProcessErrors(checkType, checkGradeType, "If the grade type is 1, the grade value must be a '*'." +
                "If the grade type is 2, the grade value must be between 1-100.");

            //Returns a collection of transactions where the student_no element value matches
            //a value within the database.
            IEnumerable<XElement> checkStudentNumber = checkGradeType.Where(x => studentNumbers.Contains(long.Parse(x.Element("student_no").Value)));
            ProcessErrors(checkGradeType, checkStudentNumber, "The student number entered could not be found in the database.");

            //Returns a collection of transactions where the course_no element's value is either a 
            //'*' or the number exists in the database.
            IEnumerable<XElement> checkCourseNumber = checkStudentNumber.Where(x => x.Element("course_no").Value == "*" || 
                                                                                    courseNumbers.Contains(x.Element("course_no").Value));
            ProcessErrors(checkStudentNumber, checkCourseNumber, "The course number needs to be a * value or in the database to be valid.");

            //Returns a collection of transactions where the registration_no element's value is either a
            //'*' or exists in the database.
            IEnumerable<XElement> checkRegistrationNumber = checkCourseNumber.Where(x => x.Element("registration_no").Value == "*" ||
                                                                                         registrationNumbers.Contains(long.Parse(x.Element("registration_no").Value)));
            ProcessErrors(checkCourseNumber, checkRegistrationNumber, "The registration number needs to be a * value or exist in the database to be valid.");
            
            //If it gets to this stage without failing, call the process transactions method and pass it
            //the error free result set, which in this case is the last check.
            ProcessTransactions(checkRegistrationNumber);
        }

        /// <summary>
        /// A method used to process all valid transaction records.
        /// </summary>
        /// <param name="transactionRecords">An error free result set passed </param>
        private void ProcessTransactions(IEnumerable<XElement> transactionRecords)
        {
            //Service object.
            CollegeRegistrationService.CollegeRegistrationClient registrationService =
                                                                 new CollegeRegistrationService.CollegeRegistrationClient();

            //Loops through each transaction record in the collection passed.
            foreach (XElement transactionRecord in transactionRecords)
            {
                //If the type is 1 which indicates registration, register the student using
                //the WCF service method.
                if(int.Parse(transactionRecord.Element("type").Value) == 1)
                {
                    string courseNumber = transactionRecord.Element("course_no").Value;
                    Course course = db.Courses.Where(x => x.CourseNumber == courseNumber).SingleOrDefault();

                    long studentNumber = long.Parse(transactionRecord.Element("student_no").Value);
                    Student student = db.Students.Where(x => x.StudentNumber == studentNumber).SingleOrDefault();

                    int registered = registrationService.RegisterCourse
                                                            (student.StudentId, 
                                                            course.CourseId, 
                                                            transactionRecord.Element("notes").Value);

                    if (registered == 0)
                    {
                        this.logData += $"Successful Registration student {student.StudentNumber} course {course.CourseNumber}\n\n";
                    }
                    else
                    {
                        this.logData += $"ERROR:{Utility.BusinessRules.RegisterError(registered)}\n\n";
                    }

                }
                //Else if the type is 2 which indicates grading, update the student's grade using
                //the WCF service method.
                else if (int.Parse(transactionRecord.Element("type").Value) == 2)
                {
                    long studentNo = long.Parse(transactionRecord.Element("student_no").Value);
                    Student student = db.Students.Where(x => x.StudentNumber == studentNo).SingleOrDefault();

                    int registrationNumber = int.Parse(transactionRecord.Element("registration_no").Value);
                    Registration registration = db.Registrations.Where(x => x.RegistrationNumber ==
                                                                            registrationNumber)
                                                                            .SingleOrDefault();

                    double gradeAsDecimal = double.Parse(transactionRecord.Element("grade").Value) / 100;

                    double? updatedGradePointAverage = registrationService.UpdateGrade
                                                            (gradeAsDecimal, registration.RegistrationId, 
                                                            transactionRecord.Element("notes").Value);

                    if (updatedGradePointAverage != null)
                    {
                        //Appends a message to logData indicating successful updating of grades.
                        logData += $"Grade {transactionRecord.Element("grade").Value.ToString()} " +
                        $"applied to student {transactionRecord.Element("student_no").Value.ToString()}" +
                        $" for registration {transactionRecord.Element("registration_no").Value.ToString()}\n\n";
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string WriteLogData()
        {
            //Creates a string that mimics the input file name but adds
            //"COMPLETE-" to the beginning
            string completedFileName = $"COMPLETE-{inputFileName}";
            
            if(File.Exists(completedFileName))
            {
                //Deletes the file. This is done so that the File.Move(found below)
                //completes successfully.
                File.Delete(completedFileName);
            }

            if(File.Exists(inputFileName))
            {
                //If the file exists, the move method renames the file to include "COMPLETE-".
                File.Move(inputFileName, completedFileName);
            }

            //Creates a stream writer object streaming to the log file name.
            //Then writes the accumulated log data into the file and closes.
            StreamWriter streamWriterLog = new StreamWriter(logFileName);
            streamWriterLog.Write(logData);
            streamWriterLog.Close();

            //Captures the current log data
            string currentLogData = logData;

            //Clears the log data and log file name variables for future use.
            logData = null;
            logFileName = null;

            return currentLogData;
        }

        /// <summary>
        /// A method that initiates the batch process by determining the
        /// appropriate filename and then proceeding with the header and 
        /// detail processing.
        /// </summary>
        /// <param name="programAcronym">The acronym of a given program</param>
        /// <param name="key">To be used at a later date.</param>
        public void ProcessTransmission(string programAcronym, string key)
        {
            //Set the input file name and the log file name.
            this.inputFileName = $"{DateTime.Today.Year}-{DateTime.Today.DayOfYear}-{programAcronym}.xml";
            this.logFileName = "LOG " + this.inputFileName.Replace("xml", "txt");

            //If the file does not exist, append to logData,
            //else call ProcessHeader and ProcessDetails methods
            //and catch any exceptions by logging to logData.
            if (!File.Exists(inputFileName))
            {
                this.logData += $"{inputFileName}: This file does not exist";
            }
            else
            {
                try
                {
                    ProcessHeader();
                    ProcessDetails();
                }
                catch(Exception error)
                {
                    this.logData += $"An error has occured: {error.Message}\n\n";
                }
            }
        }
    }
}
