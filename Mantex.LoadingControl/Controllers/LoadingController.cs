using Mantex.ERP.Logic;
using Mantex.LoadingControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mantex.LoadingControl.Controllers
{
	public class LoadingController : Controller
	{
		public ActionResult Index()
		{
			var transactionLogic = new TransactionLogic();
			var activeTransaction = transactionLogic.GetActiveTransaction();
			var activeBatch = activeTransaction == null 
				? null 
				: activeTransaction.Batches.SingleOrDefault(b => !b.EndTime.HasValue);

			var model = new LoadingModels.IndexModel
			{
				Transactions = transactionLogic.GetCurrentTransactions(),
				MaterialTypes = transactionLogic.AvailableMaterialTypes(),
				ActiveBatch = activeBatch
			};
			return View(model);
		}

		[ChildActionOnly]
		public ActionResult Progress(string Id)
		{
			int percentage = 35;
			return PartialView(percentage);
		}

		[ChildActionOnly]
		public ActionResult MachineStatus()
		{
			return PartialView();
		}
	}
}