namespace BITCollegeWindows
{
    partial class Grading
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label studentNumberLabel;
            System.Windows.Forms.Label descriptionLabel;
            System.Windows.Forms.Label courseNumberLabel;
            System.Windows.Forms.Label courseTypeLabel;
            System.Windows.Forms.Label gradeLabel;
            this.gbStudent = new System.Windows.Forms.GroupBox();
            this.descriptionLabel1 = new System.Windows.Forms.Label();
            this.studentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblFullName = new System.Windows.Forms.Label();
            this.mlblStudentNumber = new EWSoftware.MaskedLabelControl.MaskedLabel();
            this.gbGrading = new System.Windows.Forms.GroupBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.registrationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblCourseType = new System.Windows.Forms.Label();
            this.lblCourseNumber = new EWSoftware.MaskedLabelControl.MaskedLabel();
            this.lblExisting = new System.Windows.Forms.Label();
            this.lnkReturn = new System.Windows.Forms.LinkLabel();
            this.lnkUpdate = new System.Windows.Forms.LinkLabel();
            this.txtGrade = new System.Windows.Forms.TextBox();
            studentNumberLabel = new System.Windows.Forms.Label();
            descriptionLabel = new System.Windows.Forms.Label();
            courseNumberLabel = new System.Windows.Forms.Label();
            courseTypeLabel = new System.Windows.Forms.Label();
            gradeLabel = new System.Windows.Forms.Label();
            this.gbStudent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).BeginInit();
            this.gbGrading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // studentNumberLabel
            // 
            studentNumberLabel.AutoSize = true;
            studentNumberLabel.Location = new System.Drawing.Point(42, 25);
            studentNumberLabel.Name = "studentNumberLabel";
            studentNumberLabel.Size = new System.Drawing.Size(87, 13);
            studentNumberLabel.TabIndex = 0;
            studentNumberLabel.Text = "Student Number:";
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new System.Drawing.Point(42, 63);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(63, 13);
            descriptionLabel.TabIndex = 3;
            descriptionLabel.Text = "Description:";
            // 
            // courseNumberLabel
            // 
            courseNumberLabel.AutoSize = true;
            courseNumberLabel.Location = new System.Drawing.Point(48, 35);
            courseNumberLabel.Name = "courseNumberLabel";
            courseNumberLabel.Size = new System.Drawing.Size(83, 13);
            courseNumberLabel.TabIndex = 3;
            courseNumberLabel.Text = "Course Number:";
            // 
            // courseTypeLabel
            // 
            courseTypeLabel.AutoSize = true;
            courseTypeLabel.Location = new System.Drawing.Point(48, 72);
            courseTypeLabel.Name = "courseTypeLabel";
            courseTypeLabel.Size = new System.Drawing.Size(70, 13);
            courseTypeLabel.TabIndex = 5;
            courseTypeLabel.Text = "Course Type:";
            // 
            // gbStudent
            // 
            this.gbStudent.Controls.Add(descriptionLabel);
            this.gbStudent.Controls.Add(this.descriptionLabel1);
            this.gbStudent.Controls.Add(this.lblFullName);
            this.gbStudent.Controls.Add(studentNumberLabel);
            this.gbStudent.Controls.Add(this.mlblStudentNumber);
            this.gbStudent.Location = new System.Drawing.Point(20, 39);
            this.gbStudent.Margin = new System.Windows.Forms.Padding(2);
            this.gbStudent.Name = "gbStudent";
            this.gbStudent.Padding = new System.Windows.Forms.Padding(2);
            this.gbStudent.Size = new System.Drawing.Size(542, 109);
            this.gbStudent.TabIndex = 0;
            this.gbStudent.TabStop = false;
            this.gbStudent.Text = "Student Data";
            // 
            // descriptionLabel1
            // 
            this.descriptionLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "AcademicProgram.Description", true));
            this.descriptionLabel1.Location = new System.Drawing.Point(135, 63);
            this.descriptionLabel1.Name = "descriptionLabel1";
            this.descriptionLabel1.Size = new System.Drawing.Size(217, 23);
            this.descriptionLabel1.TabIndex = 4;
            // 
            // studentBindingSource
            // 
            this.studentBindingSource.DataSource = typeof(BITCollege_RS.Models.Student);
            // 
            // lblFullName
            // 
            this.lblFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFullName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "FullName", true));
            this.lblFullName.Location = new System.Drawing.Point(250, 25);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(226, 23);
            this.lblFullName.TabIndex = 3;
            // 
            // mlblStudentNumber
            // 
            this.mlblStudentNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mlblStudentNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "StudentNumber", true));
            this.mlblStudentNumber.Location = new System.Drawing.Point(135, 25);
            this.mlblStudentNumber.Mask = "0000-0000";
            this.mlblStudentNumber.Name = "mlblStudentNumber";
            this.mlblStudentNumber.Size = new System.Drawing.Size(100, 23);
            this.mlblStudentNumber.TabIndex = 1;
            this.mlblStudentNumber.Text = "    -";
            // 
            // gbGrading
            // 
            this.gbGrading.Controls.Add(gradeLabel);
            this.gbGrading.Controls.Add(this.txtGrade);
            this.gbGrading.Controls.Add(this.lblTitle);
            this.gbGrading.Controls.Add(courseTypeLabel);
            this.gbGrading.Controls.Add(this.lblCourseType);
            this.gbGrading.Controls.Add(courseNumberLabel);
            this.gbGrading.Controls.Add(this.lblCourseNumber);
            this.gbGrading.Controls.Add(this.lblExisting);
            this.gbGrading.Controls.Add(this.lnkReturn);
            this.gbGrading.Controls.Add(this.lnkUpdate);
            this.gbGrading.Location = new System.Drawing.Point(75, 171);
            this.gbGrading.Margin = new System.Windows.Forms.Padding(2);
            this.gbGrading.Name = "gbGrading";
            this.gbGrading.Padding = new System.Windows.Forms.Padding(2);
            this.gbGrading.Size = new System.Drawing.Size(473, 265);
            this.gbGrading.TabIndex = 1;
            this.gbGrading.TabStop = false;
            this.gbGrading.Text = "Grading Information";
            // 
            // lblTitle
            // 
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Course.Title", true));
            this.lblTitle.Location = new System.Drawing.Point(253, 34);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(195, 23);
            this.lblTitle.TabIndex = 10;
            // 
            // registrationBindingSource
            // 
            this.registrationBindingSource.DataSource = typeof(BITCollege_RS.Models.Registration);
            // 
            // lblCourseType
            // 
            this.lblCourseType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCourseType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Course.CourseType", true));
            this.lblCourseType.Location = new System.Drawing.Point(137, 72);
            this.lblCourseType.Name = "lblCourseType";
            this.lblCourseType.Size = new System.Drawing.Size(100, 23);
            this.lblCourseType.TabIndex = 6;
            // 
            // lblCourseNumber
            // 
            this.lblCourseNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCourseNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Course.CourseNumber", true));
            this.lblCourseNumber.Location = new System.Drawing.Point(137, 35);
            this.lblCourseNumber.Name = "lblCourseNumber";
            this.lblCourseNumber.Size = new System.Drawing.Size(100, 23);
            this.lblCourseNumber.TabIndex = 4;
            // 
            // lblExisting
            // 
            this.lblExisting.AutoSize = true;
            this.lblExisting.Location = new System.Drawing.Point(263, 152);
            this.lblExisting.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblExisting.Name = "lblExisting";
            this.lblExisting.Size = new System.Drawing.Size(185, 13);
            this.lblExisting.TabIndex = 2;
            this.lblExisting.Text = "Existing grades cannot be overridden.";
            this.lblExisting.Visible = false;
            // 
            // lnkReturn
            // 
            this.lnkReturn.AutoSize = true;
            this.lnkReturn.Location = new System.Drawing.Point(236, 225);
            this.lnkReturn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkReturn.Name = "lnkReturn";
            this.lnkReturn.Size = new System.Drawing.Size(117, 13);
            this.lnkReturn.TabIndex = 1;
            this.lnkReturn.TabStop = true;
            this.lnkReturn.Text = "Return to Student Data";
            this.lnkReturn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReturn_LinkClicked);
            // 
            // lnkUpdate
            // 
            this.lnkUpdate.AutoSize = true;
            this.lnkUpdate.Enabled = false;
            this.lnkUpdate.Location = new System.Drawing.Point(108, 225);
            this.lnkUpdate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkUpdate.Name = "lnkUpdate";
            this.lnkUpdate.Size = new System.Drawing.Size(74, 13);
            this.lnkUpdate.TabIndex = 0;
            this.lnkUpdate.TabStop = true;
            this.lnkUpdate.Text = "Update Grade";
            this.lnkUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpdate_LinkClicked);
            // 
            // gradeLabel
            // 
            gradeLabel.AutoSize = true;
            gradeLabel.Location = new System.Drawing.Point(48, 152);
            gradeLabel.Name = "gradeLabel";
            gradeLabel.Size = new System.Drawing.Size(39, 13);
            gradeLabel.TabIndex = 10;
            gradeLabel.Text = "Grade:";
            // 
            // txtGrade
            // 
            this.txtGrade.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Grade", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "0.00%"));
            this.txtGrade.Location = new System.Drawing.Point(137, 152);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.Size = new System.Drawing.Size(100, 20);
            this.txtGrade.TabIndex = 11;
            // 
            // Grading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(645, 457);
            this.Controls.Add(this.gbGrading);
            this.Controls.Add(this.gbStudent);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Grading";
            this.Text = "Grading";
            this.Load += new System.EventHandler(this.Grading_Load);
            this.gbStudent.ResumeLayout(false);
            this.gbStudent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).EndInit();
            this.gbGrading.ResumeLayout(false);
            this.gbGrading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbStudent;
        private System.Windows.Forms.GroupBox gbGrading;
        private System.Windows.Forms.Label lblExisting;
        private System.Windows.Forms.LinkLabel lnkReturn;
        private System.Windows.Forms.LinkLabel lnkUpdate;
        private System.Windows.Forms.BindingSource studentBindingSource;
        private System.Windows.Forms.Label lblFullName;
        private EWSoftware.MaskedLabelControl.MaskedLabel mlblStudentNumber;
        private System.Windows.Forms.Label descriptionLabel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.BindingSource registrationBindingSource;
        private System.Windows.Forms.Label lblCourseType;
        private EWSoftware.MaskedLabelControl.MaskedLabel lblCourseNumber;
        private System.Windows.Forms.TextBox txtGrade;
    }
}