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
			var model = new LoadingModels.IndexModel
			{
				Transactions = transactionLogic.GetCurrentTransactions(),
				MaterialTypes = transactionLogic.AvailableMaterialTypes(),
				ActiveTransactionId = transactionLogic.GetActiveTransaction().Id
			};
			return View(model);
		}
	
		[ChildActionOnly]
		public ActionResult ProductionStatus()
		{
			var transactionLogic = new TransactionLogic();
			var model = new LoadingModels.ProductionStatusModel
			{
				Transaction = transactionLogic.GetActiveTransaction(),
				BatchIndex = 3,
				StartTime = new DateTime(2015, 3, 10, 12, 55, 2),
				ProductionTime_Batch = 45 * 60,
				ProductionTime_Transaction = 2 * 60 * 60 + 37 * 60
			};
			return PartialView(model);
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