using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SearchStudentData.Models
{
    public class Student
    {
        public int StudentID { set;get; }
        [Required]
        [MinLength(3),MaxLength(50)]
        [Display(Name ="Full Name")]
        public string StdName { set; get; }
        [Required]
        [Display (Name="Father Name")]
        public string StdFatherName { set; get; }
        [Required]
        [Display(Name ="Obtained Marks")]
        public int StdObtainMarks { set; get; }
        [Required]
        [Display(Name ="Total Marks")]
        public int StdTotalMarks { set; get; }
        [Display(Name ="Class")]
        public string StdClass { set; get; }
        [Display(Name ="College")]
        [MinLength(10)]
        public string StdColg { set; get; }


    }
    public class StudentDbContext:DbContext
    {
        public DbSet<Student> Students { get; set; }

    }
}