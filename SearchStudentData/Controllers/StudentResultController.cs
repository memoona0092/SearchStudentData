using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SearchStudentData.Models;

namespace SearchStudentData.Controllers
{
    public class StudentResultController : Controller
    {
        // GET: StudentResult
        StudentDbContext db = new StudentDbContext();
        //this function is the home page which enlist all the student's result
        public ActionResult StudentsList()
        {
            return View(db.Students.ToList());
        }

        public ActionResult AddStudentResult()
        {
            return View();
        }
        //this function is use to add new student result inforamtion
        [HttpPost]
        public ActionResult AddStudentResult(Student Ostd)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Students.Add(Ostd);
                    db.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
            return View();
        }
        //this function is used to update student information
        [HttpGet]
        public ActionResult EditStudent(int? id)
        { 
            Student student= db.Students.Where(x => x.StudentID == id).FirstOrDefault();
            return View(student) ;
        }
        [HttpPost]
        public ActionResult EditStudent(Student Objstudent)
        {
            try
            {
                var StudentData = db.Students.Where(x => x.StudentID == Objstudent.StudentID).FirstOrDefault();
                if (StudentData != null)
                {
                    StudentData.StudentID = Objstudent.StudentID;
                    StudentData.StdName=Objstudent.StdName;
                    StudentData.StdFatherName=Objstudent.StdFatherName;
                    StudentData.StdObtainMarks=Objstudent.StdObtainMarks;
                    StudentData.StdTotalMarks=Objstudent.StdTotalMarks;
                    StudentData.StdClass=Objstudent.StdClass;
                    StudentData.StdColg=Objstudent.StdColg;
                    db.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
            return RedirectToAction("StudentsList");
        }

        //Provide Student detail on the basis of Student id
        public ActionResult StudentDetail(int id)
        {
            Student oStd=new Student();
            try
            {
                if(id>0)
                {
                    oStd= db.Students.Where(x => x.StudentID == id).FirstOrDefault();
                    
                }
            }
            catch
            {
                throw;
            }
            return View(oStd);
        }
        //This Function is about search and filter and retun list of student objects to the StudentsList view
        public ActionResult SearchStudent(string SearchValue,string SearchOption,string FiltersValue)
        {
            
            
            List<Student> student=new List<Student>();
            try
            {
                if (SearchValue != "" && SearchOption != "")
                {
                    int searchOption = Int32.Parse(SearchOption);
                    if (searchOption == 1)
                    {
                        student = db.Students.Where(x => x.StdName.Contains(SearchValue)).ToList();
                    }
                    else if (searchOption==2)
                    {
                        student = db.Students.Where(x => x.StdClass.Contains(SearchValue)).ToList();
                    }
                    else if(searchOption==3)
                    {
                        student = db.Students.Where(x => x.StdColg.Contains(SearchValue)).ToList();
                    }
                }
                if(FiltersValue != "")
                {
                    
                  int FiltrVal=Int32.Parse(FiltersValue);
                    if(FiltrVal==1)
                    {
                        int marks=db.Students.Min(x=>x.StdObtainMarks);
                        student = db.Students.Where(x => x.StdObtainMarks == marks).ToList();
                    }
                    if(FiltrVal==2)
                    {
                        int maxMarks=db.Students.Max(x=>x.StdObtainMarks);
                        student=db.Students.Where((x)=>x.StdObtainMarks == maxMarks).ToList();

                    }
                }
            }
            catch
            {
                throw;
            }
            
            return View("StudentsList", student);
        }
    }
}