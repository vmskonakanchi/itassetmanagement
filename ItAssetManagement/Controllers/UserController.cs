using ItAssetManagement.DataBase;
using ItAssetManagement.Enums;
using ItAssetManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItAssetManagement.Controllers
{
	public class UserController : Controller
	{
		private readonly ApplicationDbContext db;

		public UserController(ApplicationDbContext db)
		{
			this.db = db;
		}


		//GET METHODS 
		public IActionResult Index()
		{
			ViewBag.Nav = "User";
			return View();
		}


		public IActionResult SelectAsset(int selected)
		{
			ViewBag.Nav = "User";
			AssetType type = (AssetType)selected;
			IEnumerable<Asset> asssetsWithSelectedType = db.Assets.Where((a) => a.AssetType == type).ToList();
			return View(asssetsWithSelectedType);
		}


		public IActionResult MyAssets()
		{
			ViewBag.Nav = "User";
			uint? userId = (uint)HttpContext.Session.GetInt32("id")!;

			if (userId == null) return RedirectToAction(nameof(Index));

			User? u = db.Users.Find(userId);

			if (u != null)
			{
				IEnumerable<AssetRequest> requests = db.AssetRequests.Where((r) => r.UserName.Equals(u.Name)).ToList();
				return View(requests);
			}
			else
			{
				return View(new List<AssetRequest>());
			}
		}

		//POST METHODS

		[HttpPost]
		public IActionResult RequestAsset(string name, string type, string? toDate, string? fromDate)
		{
			ViewBag.Nav = "User";
			uint? userId = (uint)HttpContext.Session.GetInt32("id")!;
			if (userId == null) return RedirectToAction(nameof(Index));
			User? u = db.Users.Find(userId);
			Asset? asset = db.Assets.Where((a) => a.Name!.Equals(name)).FirstOrDefault();
			AssetRequest request = new AssetRequest()
			{
				FromDate = fromDate != null ? fromDate : DateTime.Now.ToShortDateString(),
				ToDate = toDate != null ? DateTime.Now.ToShortDateString() : toDate,
				AssetName = asset!.Name,
				UserName = u!.Name,
				Type = type,
				AssetId = asset.Id,
			};
			db.AssetRequests.Add(request);
			db.SaveChanges();
			ViewBag.RedirectSuccess = true;
			return RedirectToAction(nameof(Index));
		}


	}
}
