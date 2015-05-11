using Mantex.ERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mantex.LoadingControl.Controllers
{
	public class HomeController : Controller
	{
		public ITransactionLogic transactionLogic { get; set; }

		public ActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				if (transactionLogic.GetActiveTransaction() != null)
				{
					return RedirectToAction("Index", "Loading");
				}
			}
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}