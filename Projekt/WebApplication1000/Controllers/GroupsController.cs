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
    /// The GroupsController class is responsible for handling HTTP requests related to groups.
    /// </summary>
    [Route("[controller]")]
    public class GroupsController : Controller
    {
        private readonly MyDbContext _context;

        public GroupsController(MyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The List method is responsible for listing all groups.
        /// </summary>
        /// <returns>
        /// The list view of groups.
        /// </returns>
        [HttpGet("list")]
        public IActionResult List()
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var groups = _context.Grupas
                .Include(g => g.SemestrIdSemestrNavigation)
                .Include(g => g.Zajecia)
                    .ThenInclude(z => z.PrzedmiotIdPrzedmiotNavigation)
                .ToList();

            ViewBag.Groups = groups;
            ViewBag.Semestrs = _context.Semestrs.ToList();

            return View("List", groups);
        }

        /// <summary>
        /// The Delete method is responsible for deleting a group.
        /// </summary>
        /// <param name="id">
        /// The group id.
        /// </param>
        /// <returns>
        /// The list view of groups.
        /// </returns>
        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var group = _context.Grupas
                .Include(g => g.StudentOsobaSIndices)
                .FirstOrDefault(g => g.IdGrupa == id);

            if (group != null)
            {
                _context.Grupas.Remove(group);
                _context.SaveChanges();
                TempData["Notification"] = $"Deleted group.";
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// The Add method is responsible for adding a group.
        /// </summary>
        /// <param name="semestrId">
        /// The id of the semester.
        /// </param>
        /// <returns>
        /// The list view of groups.
        /// </returns>
        [HttpPost("add")]
        public IActionResult Add(int semestrId)
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var group = new Grupa
            {
                IdGrupa = _context.Grupas.Max(g => g.IdGrupa) + 1,
                SemestrIdSemestr = semestrId
            };

            _context.Grupas.Add(group);
            _context.SaveChanges();
            TempData["Notification"] = $"Assigned group  to semestr.";

            return RedirectToAction("List");
        }

        /// <summary>
        /// The AssignStudent method is responsible for assigning a student to a group.
        /// </summary>
        /// <returns>
        /// The list view of groups.
        /// </returns>
        [HttpGet("get-groups")]
        public IActionResult GetGroups()
        {
            var groups = _context.Grupas
                .Include(g => g.SemestrIdSemestrNavigation)
                .Include(g => g.Zajecia)
                    .ThenInclude(z => z.PrzedmiotIdPrzedmiotNavigation)
                .ToList();
            return Json(groups);
        }

        /// <summary>
        /// The GetSemestrs method is responsible for getting all semestrs.
        /// </summary>
        /// <returns>
        /// A JSON response with all semestrs.
        /// </returns>
        [HttpGet("get-semestrs")]
        public IActionResult GetSemestrs()
        {
            var semestrs = _context.Semestrs.ToList();
            return Json(semestrs);
        }
    }
}
