using System;
using Mantex.ERP.Entities;

namespace Mantex.ERP.Services
{
	public interface ITransactionLogic
	{
		void AddObservation(int batchId, string text);
		System.Collections.Generic.IEnumerable<MaterialType> AvailableMaterialTypes();
		void Create(Transaction model);
		void FinishTransaction(string Id);
		Transaction GetActiveTransaction();
		Entities.Batch GetBatch(int id);
		System.Collections.Generic.IEnumerable<Transaction> GetCurrentTransactions();
		void StartTransaction(string transactionId, int materialTypeId);
		Batch StopBatch(int Id);
		Transaction GetTransaction(string id);
	}
}
