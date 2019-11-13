using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SAT.DATA.EF
{
    #region Course
    public class CourseMetadata
    {
        //public int CourseId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "* Must be 50 or less characters")]
        [Display(Name = "Course")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "Course Description")]
        public string CourseDescription { get; set; }

        [Required]
        [Display(Name = "Credit Hours")]
        public byte CreditHours { get; set; }

        [StringLength(250, ErrorMessage = "* Must be 250 or less characters")]
        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "None")]
        public string Curriculum { get; set; }

        [UIHint("MulitlineText")]
        [DisplayFormat(NullDisplayText = "None")]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }

    }
    [MetadataType(typeof(CourseMetadata))]
    public partial class Course { }
    #endregion

    #region Enrollment
    public class EnrollmentMetadata
    {
        //public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int ScheduledClassId { get; set; }

        [Required]
        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        public System.DateTime EnrollmentDate { get; set; }

    }
    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment { }
    #endregion

    #region ScheduledClass
    public class ScheduledClassMetadata
    {
        //public int ScheduledClassId { get; set; }
        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public System.DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Data")]
        [DataType(DataType.Date)]
        public System.DateTime EndDate { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "* Must be 250 or less characters")]
        [Display(Name = "Instructor Name")]
        public string InstructorName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "* Must be 20 or less characters")]
        public string Location { get; set; }

        public int SCSID { get; set; }

    }
    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClass { }

    #endregion

    #region Scheduled Class Status
    public class ScheduledClassStatusMetadata
    {
        //public int SCSID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "* Must be 50 or less characters")]
        [Display(Name = "Schedule Class Status")]
        public string SCSName { get; set; }

    }
    [MetadataType(typeof(ScheduledClassStatusMetadata))]
    public partial class ScheduledClassStatus { }
    #endregion

    #region Student
    public class StudentMetadata
    {
        //public int StudentId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "* Must be 20 or less characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "* Must be 20 or less characters")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }


        [StringLength(20, ErrorMessage = "* Must be 20 or less characters")]
        [DisplayFormat(NullDisplayText = "Undeclared")]
        public string Major { get; set; }

        [StringLength(20, ErrorMessage = "* Must be 20 or less characters")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string Address { get; set; }

        [StringLength(25, ErrorMessage = "* Must be 25 or less characters")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "* Must be 2 or less characters")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string State { get; set; }

        [StringLength(10, ErrorMessage = "* Must be 10 or less characters")]
        [DisplayFormat(NullDisplayText = "N/A")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [StringLength(13, ErrorMessage = "* Must be 13 or less characters")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string Phone { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "* Must be 60 or less characters")]
        public string Email { get; set; }

        [StringLength(60, ErrorMessage = "* Must be 60 or less characters")]
        [Display(Name = "Image")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string PhotoUrl { get; set; }

        public int SSID { get; set; }

    }
    [MetadataType(typeof(StudentMetadata))]
    public partial class Student { }
    #endregion

    #region StudentStatus
    public class StudentStatusMetadata
    {
        //public int SSID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The value must be 30 characters or less")]
        [Display(Name = "Student Status Name")]
        public string SSName { get; set; }

        [StringLength(250, ErrorMessage = "The value must be 250 characters or less")]
        public string SSDescription { get; set; }

    }

    [MetadataType(typeof(StudentStatusMetadata))]
    public partial class StudentStatus { }
    #endregion

}
