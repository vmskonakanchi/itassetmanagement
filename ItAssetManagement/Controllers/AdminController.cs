
using ItAssetManagement.DataBase;
using ItAssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

namespace ItAssetManagement.Controllers
{
	public class AdminController : Controller
	{
		private readonly ApplicationDbContext db;

		public AdminController(ApplicationDbContext db)
		{
			this.db = db;
		}
		//GET METHODS
		public IActionResult Index()
		{
			ViewBag.Nav = "Admin";
			return View(db.Assets.ToList());
		}

		public IActionResult DeleteAsset(int id)
		{
			ViewBag.Nav = "Admin";
			Asset? asset = db.Assets.Find(id);
			if (asset != null) db.Assets.Remove(asset);
			else return BadRequest();
			return RedirectToAction(nameof(Index));
		}


		public IActionResult AssetRequests()
		{
			ViewBag.Nav = "Admin";
			return View(db.AssetRequests.Where((r) => r.Status == "Pending"));
		}

		public async Task<IActionResult> Approve(uint id, bool isApproved)
		{
			try
			{
				AssetRequest? request = db.AssetRequests.Find(id);
				request!.Status = isApproved ? "Approved" : "Rejected";
				db.AssetRequests.Update(request);
				db.SaveChanges();
				User? user = db.Users.Find(HttpContext.Session.GetInt32("id"));
				using (var client = new SmtpClient())
				{
					MailMessage msg = new MailMessage();
					msg.From = new MailAddress("admin@itassets.com");
					msg.To.Add(new MailAddress(user!.Email!));
					msg.Subject = "Regarding It Asset That You Requested";
					msg.Body = $" Dear Sir/Mam , \n the asset you requested is {request!.Status}";
					client.Port = 2525;
					client.Host = "smtp.elasticemail.com";
					client.Credentials = new NetworkCredential("cse.takeoff@gmail.com", "C67C74D3F51601A654935C9BBE0362604B8D");
					client.DeliveryMethod = SmtpDeliveryMethod.Network;
					client.Send(msg);
				}
				return RedirectToAction(nameof(AssetRequests));
			}
			catch (Exception)
			{
				return RedirectToAction(nameof(AssetRequests));
			}
		}

		public IActionResult AddAsset()
		{
			ViewBag.Nav = "Admin";
			return View();
		}

		public IActionResult EditAsset(int id)
		{
			ViewBag.Nav = "Admin";
			return View(db.Assets.Find(id));
		}

		public IActionResult AddUser()
		{
			ViewBag.Nav = "Admin";
			return View();
		}

		public IActionResult DeleteAssset(int id)
		{
			if (id > 0)
			{
				Asset? asset = db.Assets.Find(id);
				if (asset != null)
				{
					db.Assets.Remove(asset);
					db.SaveChangesAsync();
				}
				else return BadRequest();
			}
			else
			{
				return BadRequest();
			}
			return RedirectToAction(nameof(Index));
		}


		// POST METHODS
		[HttpPost]
		public IActionResult AddUser(User user)
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

		[HttpPost]
		public IActionResult AddAsset(Asset asset)
		{
			ViewBag.Nav = "Admin";
			asset.EnteredBy = "Admin";
			asset.SerialNo = GetSerialKeyAlphaNumaric(12);
			db.Assets.Add(asset);
			db.SaveChangesAsync();
			ViewBag.Msg = "Successfully Saved Asset";
			return View();
		}

		[HttpPost]
		public IActionResult EditAsset(Asset asset)
		{
			if (asset != null)
			{
				db.Assets.Update(asset);
				db.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return BadRequest();
			}
		}

		// Get the asset by id - view asset
		public IActionResult ViewAsset(int id)
		{
			if (id > 0)
			{
				return View(db.Assets.Find(id));
			}
			else
			{
				return BadRequest();
			}
		}

		// SERIAL NUMBER CREATOR FOR GENERATING SERIAL NUMBERS FOR ASSETS
		public string GetSerialKeyAlphaNumaric(int keyLength)
		{
			string newSerialNumber = "";
			string SerialNumber = Guid.NewGuid().ToString("N").Substring(0, keyLength).ToUpper();
			for (int iCount = 0; iCount < keyLength; iCount += 4)
			{
				newSerialNumber = newSerialNumber + SerialNumber.Substring(iCount, 4) + "-";
			}
			newSerialNumber = newSerialNumber.Substring(0, newSerialNumber.Length - 1);
			return newSerialNumber;
		}
	}
}
