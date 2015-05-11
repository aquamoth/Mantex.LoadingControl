using System;
namespace Mantex.ERP.Services
{
	public interface ITransactionLogic
	{
		System.Collections.Generic.IEnumerable<Mantex.ERP.Entities.MaterialType> AvailableMaterialTypes();
		void Create(Mantex.ERP.Entities.Transaction model);
		void FinishTransaction(string Id);
		Mantex.ERP.Entities.Transaction GetActiveTransaction();
		System.Collections.Generic.IEnumerable<Mantex.ERP.Entities.Transaction> GetCurrentTransactions();
		void StartTransaction(string transactionId, int materialTypeId);
		Mantex.ERP.Entities.Batch StopBatch(int Id);
		Mantex.ERP.Entities.Transaction GetTransaction(string id);
	}
}
