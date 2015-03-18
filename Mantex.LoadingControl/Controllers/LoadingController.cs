using Mantex.ERP.Logic;
using Mantex.LoadingControl.Helpers;
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
		[HttpGet]
		[RestoreModelStateFromTempData]
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

		[HttpPost]
		[ValidateAntiForgeryToken]
		[SetTempDataModelState]
		public ActionResult NewTransaction(Mantex.ERP.Data.Transaction model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					model.MaterialTypeId = 1;
					var transactionLogic = new TransactionLogic();
					transactionLogic.Create(model);
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
				}
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		[ActionName("Index")]
		[ValidateAntiForgeryToken]
		[SetTempDataModelState]
		public ActionResult StartBatch(LoadingModels.IndexPostModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var transactionLogic = new TransactionLogic();
					transactionLogic.StartTransaction(model.SelectedTransaction, model.SelectedMaterialType);
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
				}
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[SetTempDataModelState]
		public ActionResult StopBatch(int Id)
		{
			try
			{
				var transactionLogic = new TransactionLogic();
				transactionLogic.StopBatch(Id);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[SetTempDataModelState]
		public ActionResult FinishTransaction(string Id)
		{
			try
			{
				var transactionLogic = new TransactionLogic();
				transactionLogic.FinishTransaction(Id);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
			}
			return RedirectToAction("Index");
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