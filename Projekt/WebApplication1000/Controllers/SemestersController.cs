using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1000.Context;
using WebApplication1000.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;

/**
 * The GroupsController class is a part of the WebApplication1000.Controllers namespace.
 * This class is responsible for handling HTTP requests related to groups.
 * It includes methods for listing all groups, deleting a group, adding a group, assigning a student to a group, getting all groups, and getting all semestrs.
*/

namespace WebApplication1000.Controllers
{
    /// <summary>
    /// The SemestersController class is responsible for handling HTTP requests related to semesters.
    /// </summary>
    [Route("[controller]")]
    public class SemestersController : Controller
    {
        private readonly MyDbContext _context;

        public SemestersController(MyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The List method is responsible for listing all semesters.
        /// </summary>
        /// <returns>
        /// The list view of semesters.
        /// </returns>
        [HttpGet("list")]
        public IActionResult List()
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var semesters = _context.Semestrs
                .Include(s => s.PrzedmiotIdPrzedmiots)
                .ToList();

            ViewBag.Semesters = semesters;
            ViewBag.Przedmiots = _context.Przedmiots.ToList();

            return View("List", semesters);
        }

        /// <summary>
        /// The Add method is responsible for adding a semester.
        /// </summary>
        /// <returns>
        /// The list view of semesters.
        /// </returns>
        [HttpPost("add")]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var semester = new Semestr
            {
                IdSemestr = _context.Semestrs.Max(s => s.IdSemestr) + 1,
            };

            _context.Semestrs.Add(semester);
            _context.SaveChanges();
            TempData["Notification"] = $"Assigned semestr.";

            return RedirectToAction("List");
        }
        
        /// <summary>
        /// The Delete method is responsible for deleting a semester.
        /// </summary>
        /// <param name="id">
        /// The semester id.
        /// </param>
        /// <returns>
        /// The list view of semesters.
        /// </returns>
        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var semester = _context.Semestrs
                .Include(s => s.Grupas)
                .Include(s => s.PrzedmiotIdPrzedmiots)
                .FirstOrDefault(s => s.IdSemestr == id);

            if (semester != null)
            {
                // Remove associations with groups and subjects
                semester.Grupas.Clear();
                semester.PrzedmiotIdPrzedmiots.Clear();
                _context.SaveChanges();

                // Remove the semester
                _context.Semestrs.Remove(semester);
                _context.SaveChanges();
                TempData["Notification"] = $"Deleted semestr.";
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// The AssignSubject method is responsible for assigning a subject to a semester.
        /// </summary>
        /// <param name="semesterId">
        /// The id of the semester.
        /// </param>
        /// <param name="subjectId">
        /// The id of the subject.
        /// </param>
        /// <returns>
        /// The list view of semesters.
        /// </returns>
        [HttpPost("assign-subject")]
        public IActionResult AssignSubject(int semesterId, int subjectId)
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var semester = _context.Semestrs.Include(s => s.PrzedmiotIdPrzedmiots).FirstOrDefault(s => s.IdSemestr == semesterId);
            var subject = _context.Przedmiots.Find(subjectId);

            if (semester != null && subject != null)
            {
                semester.PrzedmiotIdPrzedmiots.Add(subject);
                _context.SaveChanges();
                TempData["Notification"] = $"Assigned subject to.";
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// The GetSemesters method is responsible for getting all semesters.
        /// </summary>
        /// <returns>
        /// The list of semesters.
        /// </returns>
        [HttpGet("get-semesters")]
        public IActionResult GetSemesters()
        {
            var semesters = _context.Semestrs.Include(s => s.PrzedmiotIdPrzedmiots).ToList();
            return Json(semesters);
        }

        /// <summary>
        /// The GetSubjects method is responsible for getting all subjects.
        /// </summary>
        /// <returns>
        /// The list of subjects.
        /// </returns>
        [HttpGet("get-subjects")]
        public IActionResult GetSubjects()
        {
            var subjects = _context.Przedmiots.ToList();
            return Json(subjects);
        }
    }
}
