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
		public ActionResult Index(string id = null)
		{
			var selectedTransaction = transactionLogic.GetActiveTransaction();
			var activeBatch = selectedTransaction != null
				? selectedTransaction.Batches.SingleOrDefault(b => !b.StoppedAt.HasValue)
				: null;
			var selectedTransactionId = selectedTransaction != null 
				? selectedTransaction.Id 
				: id;

			var model = new LoadingModels.IndexModel
			{
				Transactions = transactionLogic.GetCurrentTransactions(),
				MaterialTypes = transactionLogic.AvailableMaterialTypes(),
				SelectedTransaction = selectedTransactionId,
				ActiveBatch = activeBatch
			};
			return View(model);
		}

		//[HttpGet]
		public ActionResult BatchStatus(string id)
		{
			var model = id == null 
				? null 
				: transactionLogic.GetTransaction(id);

			return PartialView(model);
		}

		[HttpGet]
		public ActionResult TransactionInfo()
		{
			return PartialView();
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
		public ActionResult StopBatch(int batchId)
		{
#warning Stopping a batch must be transactional
			string transactionId = null;
			try
			{
				var batch = transactionLogic.StopBatch(batchId);
				transactionId = batch.TransactionId;
				new FlowScannerLogic().StopMeasure();
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
			}
			return RedirectToAction("Index", new { id = transactionId });
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

		//[ChildActionOnly] //Needs access from ajax too
		public ActionResult Progress(string id)
		{
			var transaction = transactionLogic.GetTransaction(id);

			var secondsOfProduction = transaction.Batches.Select(b =>
				{
					var seconds = (b.StoppedAt ?? DateTime.Now).Subtract(b.StartedAt).TotalSeconds;
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