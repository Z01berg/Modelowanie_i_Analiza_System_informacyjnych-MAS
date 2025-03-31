using Microsoft.AspNetCore.Mvc;
using WebApplication1000.Context;
using WebApplication1000.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq;

/**
 * The Authorization class is a part of the WebApplication1000.Controllers namespace.
 * This class is responsible for handling HTTP requests related to authorization.
 * It includes methods for logging in, logging out, and displaying the login page.
 *
 * @remarks
 * This class includes methods for logging in, logging out, and displaying the login page.
 */

namespace WebApplication1000.Controllers
{
    /// <summary>
    /// The Authorization class is responsible for handling HTTP requests related to authorization.
    /// </summary>
    /// <remarks>
    /// This class includes methods for logging in, logging out, and displaying the login page.
    /// </remarks>
    [Route("[controller]")]
    public class Authorization : Controller
    {
        private readonly MyDbContext _context;

        public Authorization(MyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The GetLoginPage method is responsible for displaying the login page.
        /// </summary>
        /// <returns>The login page view.</returns>
        /// <remarks>
        /// If the user is already logged in, the method redirects to the dashboard.
        /// </remarks>
        [HttpGet("login")]
        public IActionResult GetLoginPage()
        {
            if (HttpContext.Session.GetString("UserIndex") != null)
            {
                return RedirectToAction("Dashboard");
            }

            return View("_LoginPage");
        }
        
        /// <summary>
        /// The Login method is responsible for logging in the user.
        /// </summary>
        /// <param name="username">
        /// The username of the user.
        /// </param>
        /// <param name="password">
        /// The password of the user.
        /// </param>
        /// <returns>
        /// The dashboard view if the login is successful, otherwise the login page view with an error message.
        /// </returns>
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            if (!int.TryParse(username, out int userIndex))
            {
                ViewData["ErrorMessage"] = "Invalid username or password";
                return View("_LoginPage");
            }
            var dyrektor = _context.Dyrektors.FirstOrDefault(d => d.OsobaSIndex == userIndex && d.Password == password);
            if (dyrektor != null)
            {
                HttpContext.Session.SetString("UserIndex", username);
                HttpContext.Session.SetString("UserRole", "Dyrektor");
                return RedirectToAction("DyrektorDashboard");
            }
            
            var dydaktyk = _context.Dydaktyks.FirstOrDefault(d => d.OsobaSIndex == userIndex && d.Password == password);
            if (dydaktyk != null)
            {
                HttpContext.Session.SetString("UserIndex", username);
                HttpContext.Session.SetString("UserRole", "Dydaktyk");
                return RedirectToAction("DydaktykDashboard");
            }
            
            var student = _context.Students.FirstOrDefault(s => s.OsobaSIndex == userIndex && s.Password == password);
            if (student != null)
            {
                HttpContext.Session.SetString("UserIndex", username);
                HttpContext.Session.SetString("UserRole", "Student");
                return RedirectToAction("StudentDashboard");
            }
            
            ViewData["ErrorMessage"] = "Invalid username or password";
            return View("_LoginPage");
        }
        
        /// <summary>
        /// The Dashboard method is responsible for displaying the dashboard.
        /// </summary>
        /// <returns>
        /// The dashboard view.
        /// </returns>
        [HttpGet("dyrektor-dashboard")]
        public IActionResult DyrektorDashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != "Dyrektor")
            {
                return RedirectToAction("GetLoginPage");
            }

            return View("_DyrektorLogin");
        }
        
        /// <summary>
        /// The DydaktykDashboard method is responsible for displaying the dashboard for a dydaktyk.
        /// </summary>
        /// <returns>
        /// The dydaktyk dashboard view.
        /// </returns>
        [HttpGet("dydaktyk-dashboard")]
        public IActionResult DydaktykDashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != "Dydaktyk")
            {
                return RedirectToAction("GetLoginPage");
            }

            return View("_DydaktykLogin");
        }
        
        /// <summary>
        /// The StudentDashboard method is responsible for displaying the dashboard for a student.
        /// </summary>
        /// <returns>
        /// The student dashboard view.
        /// </returns>
        [HttpGet("student-dashboard")]
        public IActionResult StudentDashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != "Student")
            {
                return RedirectToAction("GetLoginPage");
            }

            return View("_StudentLogin");
        }
        
        /// <summary>
        /// The Logout method is responsible for logging out the user. 
        /// </summary>
        /// <returns>
        /// The login page view.
        /// </returns>
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("GetLoginPage");
        }
    }
}
