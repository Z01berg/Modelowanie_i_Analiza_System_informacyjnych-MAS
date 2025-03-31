using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1000.Context;
using WebApplication1000.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq;

/**
 * The StudentsController class is a part of the WebApplication1000.Controllers namespace.
 * This class is responsible for handling HTTP requests related to students.
 * It includes methods for listing all students, deleting a student, adding a student, and assigning a group to a student.
 * 
 */

namespace WebApplication1000.Controllers
{
    /// <summary>
    /// The StudentsController class is responsible for handling HTTP requests related to students.
    /// </summary>
    [Route("[controller]")]
    public class StudentsController : Controller
    {
        private readonly MyDbContext _context;

        public StudentsController(MyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The List method is responsible for listing all students.
        /// </summary>
        /// <returns>
        /// The list view of students.
        /// </returns>
        [HttpGet("list")]
        public IActionResult List()
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var students = _context.Students
                .Include(s => s.OsobaSIndexNavigation)
                .Include(s => s.GrupaIdGrupas)
                .ToList();

            var groups = _context.Grupas.ToList();

            ViewBag.Students = students;
            ViewBag.Groups = groups;

            return View("List", students);
        }

        /// <summary>
        /// The Add method is responsible for adding a student.
        /// </summary>
        /// <param name="name">
        /// The student's name.
        /// </param>
        /// <param name="surname">
        /// The student's surname.
        /// </param>
        /// <param name="yearOfStudy">
        /// The student's year of study.
        /// </param>
        /// <param name="password">
        /// The student's password.
        /// </param>
        /// <returns>
        /// The list view of students.
        /// </returns>
        [HttpPost("add")]
        public IActionResult Add(string name, string surname, DateOnly yearOfStudy, string password)
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var student = new Student
            {
                OsobaSIndex = _context.Osobas.Max(o => o.SIndex) + 1,
                YearOfStudy = yearOfStudy,
                Password = password,
                OsobaSIndexNavigation = new Osoba
                {
                    SIndex = _context.Osobas.Max(o => o.SIndex) + 1,
                    Name = name,
                    Surname = surname
                }
            };
            _context.Students.Add(student);
            _context.SaveChanges();
            
            TempData["Notification"] = $"Added student {name} {surname}.";

            return RedirectToAction("List");
        }

        /// <summary>
        /// The Delete method is responsible for deleting a student.
        /// </summary>
        /// <param name="id">
        /// The student id.
        /// </param>
        /// <returns>
        /// The list view of students.
        /// </returns>
        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                TempData["Notification"] = $"Deleted student.";
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// The AssignGroup method is responsible for assigning a student to a group.
        /// </summary>
        /// <param name="studentId">
        /// The student id.
        /// </param>
        /// <param name="groupId">
        /// The group id.
        /// </param>
        /// <returns>
        /// The list view of students.
        /// </returns>
        [HttpPost("assign-group")]
        public IActionResult AssignGroup(int studentId, int groupId)
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var student = _context.Students.Include(s => s.GrupaIdGrupas).FirstOrDefault(s => s.OsobaSIndex == studentId);
            var group = _context.Grupas.Find(groupId);

            if (student != null && group != null)
            {
                student.GrupaIdGrupas.Add(group);
                _context.SaveChanges();
                TempData["Notification"] = $"Assigned student {student.OsobaSIndexNavigation.Name} {student.OsobaSIndexNavigation.Surname} to group {group.IdGrupa}.";
            }

            return RedirectToAction("List");
        }
    }
}
