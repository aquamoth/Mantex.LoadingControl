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
			//if (User.Identity.IsAuthenticated)
			//{
			//	if (transactionLogic.GetActiveTransaction() != null)
			//	{
			//		return RedirectToAction("Index", "Loading");
			//	}
			//}
			return View();
		}

		[ChildActionOnly]
		public ActionResult PreviousTransactions()
		{
			var model = transactionLogic.GetFinishedTransactions(0, 5);
			return PartialView("_Transactions", model);
		}

		[ChildActionOnly]
		public ActionResult CurrentTransactions()
		{
			var model = transactionLogic.GetCurrentTransactions()
				.Where(t => t.Batches.Any())
				.OrderBy(t => t.Batches.Select(b => b.StartedAt).Max())
				.ToArray();
			return PartialView("_Transactions", model);
		}

		[ChildActionOnly]
		public ActionResult FutureTransactions()
		{
			var model = transactionLogic.GetCurrentTransactions()
				.Where(t => !t.Batches.Any())
				.OrderBy(t => t.ShippingDate)
				.ToArray();
			return PartialView("_Transactions", model);
		}

		//public ActionResult About()
		//{
		//	ViewBag.Message = "Your application description page.";

		//	return View();
		//}

		//public ActionResult Contact()
		//{
		//	ViewBag.Message = "Your contact page.";

		//	return View();
		//}
	}
}