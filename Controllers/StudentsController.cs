using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class StudentsController : Controller
    {
        private static List<Student> students = new List<Student> {
            new Student{Id=1,Name="Doluma", Age=18, Group="9/4"},
            new Student{Id=2,Name="Kek", Age=20, Group="9/3"},
            new Student { Id = 3, Name = "Boo", Age = 19, Group = "9/2" },
            new Student { Id = 4, Name = "Moa", Age = 17, Group = "9/1" },
            };

        public IActionResult Index(string sortOrder, string searchString, int pageNumber=1)
        {
            int pageSize = 3;

            ViewData["NameSort"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["AgeSort"] = sortOrder == "Age" ? "Age_desc" : "Age";
            ViewData["GroupSort"] = sortOrder == "Group" ? "Group_desc" : "Group";
            var filteredStudents = students.AsQueryable();
            ViewData["CurrentFilter"] = searchString;

            if (!string.IsNullOrEmpty(searchString))
            {
                filteredStudents = filteredStudents.Where(s =>
                s.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                s.Group.Contains(searchString, StringComparison.OrdinalIgnoreCase)

                );
            }

            switch(sortOrder)
            {
                case "Name":
                    filteredStudents = filteredStudents.OrderBy(s => s.Name);
                    break;

                case "Name_desc":
                    filteredStudents = filteredStudents.OrderByDescending(s => s.Name);
                    break;
                    
                case "Age":
                    filteredStudents = filteredStudents.OrderBy(s => s.Age);
                    break;
                    
                case "Age_desc":
                    filteredStudents = filteredStudents.OrderByDescending(s => s.Age);
                    break;
                    
                case "Group":
                    filteredStudents = filteredStudents.OrderBy(s => s.Group);
                    break;
                    
                case "Group_desc":
                    filteredStudents = filteredStudents.OrderByDescending(s => s.Group);
                    break;
                default:
                    filteredStudents = filteredStudents.OrderBy(s => s.Id);
                    break;

            }
            int count = filteredStudents.Count();
            var items = filteredStudents.Skip((pageNumber - 1) * pageSize).ToList();
            var pagedList = new Models.PagedList<Student>(items, count, pageNumber, pageSize);
            return View(pagedList);
        }

        public IActionResult Details(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = students.Max(s => s.Id) + 1;
                students.Add(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student updatedStudent)
        {
            if (ModelState.IsValid)
            {
                var student = students.FirstOrDefault(s => s.Id == updatedStudent.Id);
                if (student == null) return NotFound();

                student.Name = updatedStudent.Name;
                student.Age = updatedStudent.Age;
                student.Group = updatedStudent.Group;

                return RedirectToAction("Index");
            }
            return View(updatedStudent);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {  
                var student = students.FirstOrDefault(x => x.Id == id);
                if (student != null)
                {
                    students.Remove(student);
                }
                return RedirectToAction("Index");
        }
    }
}
