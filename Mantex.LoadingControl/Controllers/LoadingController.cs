using Mantex.ERP.Services;
using Mantex.LoadingControl.Helpers;
using Mantex.LoadingControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mantex.LoadingControl.Controllers
{
	[Authorize(Roles="Users")]
	public class LoadingController : Controller
	{
		private readonly ITransactionLogic transactionLogic;

		public LoadingController(ITransactionLogic transactionLogic)
		{
			this.transactionLogic = transactionLogic;
		}

		[HttpGet]
		[RestoreModelStateFromTempData]
		public ActionResult Index()
		{
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
		public ActionResult NewTransaction(Mantex.ERP.Entities.Transaction model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					model.MaterialTypeId = 1;
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
#warning Starting a batch must be transactional

					transactionLogic.StartTransaction(model.SelectedTransaction, model.SelectedMaterialType);

					var flowScannerLogic = new FlowScannerLogic();
					var machineStatus = flowScannerLogic.GetStatus();
					if (machineStatus == Mantex.ERP.Entities.MachineStatusEnum.Working)
					{
						flowScannerLogic.StartMeasure(model.SelectedTransaction, model.SelectedMaterialType);
					}
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
#warning Stopping a batch must be transactional
			try
			{
				transactionLogic.StopBatch(Id);
				new FlowScannerLogic().StopMeasure();
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
#warning Finishing a batch must be transactional
			try
			{
				transactionLogic.FinishTransaction(Id);
				new FlowScannerLogic().StopMeasure();
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
			var transaction = transactionLogic.GetActiveTransaction();

			var secondsOfProduction = transaction.Batches.Select(b =>
				{
					var seconds = (b.EndTime ?? DateTime.Now).Subtract(b.StartTime).TotalSeconds;
					return seconds;
				}).Sum();

			var secondsToHalfDone = 60;
			var percentage = (int)(100 * secondsOfProduction / (secondsOfProduction + secondsToHalfDone));

			return PartialView(percentage);
		}

		[ChildActionOnly]
		public ActionResult MachineStatus()
		{
			var flowScannerLogic = new FlowScannerLogic();
			var model = flowScannerLogic.GetStatus();
			return PartialView("MachineStatus", model);
		}
	}
}