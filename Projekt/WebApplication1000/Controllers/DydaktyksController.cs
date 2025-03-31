using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1000.Context;
using WebApplication1000.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;

/**
 * The DydaktyksController class is a part of the WebApplication1000.Controllers namespace.
 * This class is responsible for handling HTTP requests related to dydaktyks.
 * It includes methods for listing all dydaktyks, deleting a dydaktyk, adding a dydaktyk, assigning a group to a dydaktyk, getting all dydaktyks, and getting all groups.
 * 
 */

namespace WebApplication1000.Controllers
{
    /// <summary>
    /// The DydaktyksController class is responsible for handling HTTP requests related to dydaktyks.
    /// </summary>
    [Route("[controller]")]
    public class DydaktyksController : Controller
    {
        private readonly MyDbContext _context;

        /// <summary>
        /// Initializes a new instance of the DydaktyksController class.
        /// </summary>
        /// <param name="context">
        /// The database context.
        /// </param>
        public DydaktyksController(MyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The List method is responsible for listing all dydaktyks.
        /// </summary>
        /// <returns>
        /// The list view of dydaktyks.
        /// </returns>
        [HttpGet("list")]
        public IActionResult List()
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var dydaktyks = _context.Dydaktyks
                .Include(d => d.OsobaSIndexNavigation)
                .Include(d => d.GrupaIdGrupas)
                .ToList();

            ViewBag.Dydaktyks = dydaktyks;
            ViewBag.Groups = _context.Grupas.ToList();

            return View("List", dydaktyks);
        }

        /// <summary>
        /// The Add method is responsible for adding a new dydaktyk.
        /// </summary>
        /// <param name="name">
        /// The name of the dydaktyk.
        /// </param>
        /// <param name="surname">
        /// The surname of the dydaktyk.
        /// </param>
        /// <param name="magisterDegree">
        /// The magister degree of the dydaktyk.
        /// </param>
        /// <param name="password">
        /// The password of the dydaktyk.
        /// </param>
        /// <returns>
        /// The list view of dydaktyks.
        /// </returns>
        [HttpPost("add")]
        public IActionResult Add(string name, string surname, string magisterDegree, string password)
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var dydaktyk = new Dydaktyk
            {
                OsobaSIndex = _context.Osobas.Max(o => o.SIndex) + 1,
                MagisterDegree = magisterDegree,
                Password = password,
                OsobaSIndexNavigation = new Osoba
                {
                    SIndex = _context.Osobas.Max(o => o.SIndex) + 1,
                    Name = name,
                    Surname = surname
                }
            };
            _context.Dydaktyks.Add(dydaktyk);
            _context.SaveChanges();

            TempData["Notification"] = $"Added group.";

            return RedirectToAction("List");
        }

        /// <summary>
        /// The Delete method is responsible for deleting a dydaktyk.
        /// </summary>
        /// <param name="id">
        /// The id of the dydaktyk to delete.
        /// </param>
        /// <returns>
        /// The list view of dydaktyks.
        /// </returns>
        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var dydaktyk = _context.Dydaktyks
                .Include(d => d.GrupaIdGrupas)
                .FirstOrDefault(d => d.OsobaSIndex == id);

            if (dydaktyk != null)
            {
                // Remove associations with groups
                dydaktyk.GrupaIdGrupas.Clear();
                _context.SaveChanges();

                // Remove the dydaktyk
                _context.Dydaktyks.Remove(dydaktyk);
                _context.SaveChanges();
                TempData["Notification"] = $"Deleted student.";
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// The AssignGroup method is responsible for assigning a group to a dydaktyk.
        /// </summary>
        /// <param name="dydaktykId">
        /// The id of the dydaktyk.
        /// </param>
        /// <param name="groupId">
        /// The id of the group.
        /// </param>
        /// <returns>
        /// The list view of dydaktyks.
        /// </returns>
        [HttpPost("assign-group")]
        public IActionResult AssignGroup(int dydaktykId, int groupId)
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage", "Authorization");
            }

            var dydaktyk = _context.Dydaktyks.Include(d => d.GrupaIdGrupas).FirstOrDefault(d => d.OsobaSIndex == dydaktykId);
            var group = _context.Grupas.Find(groupId);

            if (dydaktyk != null && group != null)
            {
                dydaktyk.GrupaIdGrupas.Add(group);
                _context.SaveChanges();
                TempData["Notification"] = $"Assigned dydaktyk to group {group.IdGrupa}.";
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// The GetDydaktyks method is responsible for getting all dydaktyks.
        /// </summary>
        /// <returns>
        /// A JSON response with all dydaktyks.
        /// </returns>
        [HttpGet("get-dydaktyks")]
        public IActionResult GetDydaktyks()
        {
            var dydaktyks = _context.Dydaktyks.Include(d => d.OsobaSIndexNavigation).ToList();
            return Json(dydaktyks);
        }

        /// <summary>
        /// The GetGroups method is responsible for getting all groups.
        /// </summary>
        /// <returns>
        /// A JSON response with all groups.
        /// </returns>
        [HttpGet("get-groups")]
        public IActionResult GetGroups()
        {
            var groups = _context.Grupas.ToList();
            return Json(groups);
        }
    }
}
