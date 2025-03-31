using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1000.Context;
using WebApplication1000.Entities;
using System.Linq;

/**
 * This class is responsible for handling HTTP requests related to events.
 * It includes methods for getting all events.
 *
 * Not finished Calendar View
 */

namespace WebApplication1000.Controllers
{
    /// <summary>
    /// The EventsController class is responsible for handling HTTP requests related to events.
    /// </summary>
    [Route("[controller]")]
    public class EventsController : Controller
    {
        private readonly MyDbContext _context;

        /// <summary>
        /// Initializes a new instance of the EventsController class.
        /// </summary>
        /// <param name="context">
        /// The database context.
        /// </param>
        public EventsController(MyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The GetEvents method is responsible for getting all events.
        /// </summary>
        /// <returns>
        /// The list of events.
        /// </returns>
        [HttpGet("get-events")]
        public IActionResult GetEvents()
        {
            var events = _context.Zajecia
                .Include(z => z.GrupaIdGrupaNavigation)
                .Include(z => z.PrzedmiotIdPrzedmiotNavigation)
                .Select(z => new
                {
                    z.IdZajecia,
                    z.Theme,
                    z.StartZajecia,
                    z.EndZajecia,
                    Group = z.GrupaIdGrupaNavigation.IdGrupa,
                    Subject = z.PrzedmiotIdPrzedmiotNavigation.Name
                })
                .ToList();

            return Json(events);
        }
    }
}
