using ItAssetManagement.DataBase;
using ItAssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ItAssetManagement.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext db;
		public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
		{
			_logger = logger;
			this.db = db;
		}

		// GET METHODS FOR INDEX , LOGIN , REGISTER PAGES

		public IActionResult Index()
		{
			ViewBag.Nav = "Main";
			if (HttpContext.Session.GetInt32("id") == -1)
			{
				return RedirectToAction("Index", "Admin");
			}

			if (HttpContext.Session.GetInt32("id") > 0)
			{
				return RedirectToAction("Index", "User");
			}
			return View();
		}

		public IActionResult Login()
		{
			ViewBag.Nav = "Main";
			return View();
		}

		public IActionResult Register()
		{
			ViewBag.Nav = "Main";
			return View();
		}


		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction(nameof(Index));
		}

		// POST METHODS FOR LOGIN AND REGISTER
		[HttpPost]
		public IActionResult Login(string email, string password)
		{

			ViewBag.Nav = "Main";
			if (email == "" || password == "")
			{
				ViewBag.Fields = 1;
				return View();
			}
			if (email.Equals("admin@gmail.com") && password.Equals("admin"))
			{
				HttpContext.Session.SetInt32("id", -1);
				return RedirectToAction("Index", "Admin");
			}

			User? u = db.Users.Where(dbUser => dbUser.Email == email).FirstOrDefault();
			if (u != null)
			{
				if (u.Password.Equals(password))
				{
					HttpContext.Session.SetInt32("id", (int)u.Id);
					return RedirectToAction("Index", "User");
				}
			}
			ViewBag.Fields = 0;
			ViewBag.Error = 1;
			return View();
		}


		[HttpPost]
		public IActionResult Register(User user)
		{
			ViewBag.Nav = "Main";
			User? curentUser = db.Users.Where((u) => u.Email == user.Email).FirstOrDefault();
			if (curentUser != null)
			{
				ViewBag.Error = 2;
				return View();
			}
			db.Users.AddAsync(user);
			db.SaveChanges();
			ViewBag.Success = 1;
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}