using System;
using Mantex.ERP.Entities;

namespace Mantex.ERP.Services
{
	public interface ITransactionLogic
	{
		void AddObservation(int batchId, string text, string username);
		System.Collections.Generic.IEnumerable<MaterialType> AvailableMaterialTypes();
		void Create(Transaction model);
		void FinishTransaction(string Id, string username);
		Transaction GetActiveTransaction();
		Entities.Batch GetBatch(int id);
		System.Collections.Generic.IEnumerable<Transaction> GetCurrentTransactions();
		void StartTransaction(string transactionId, int materialTypeId, string username);
		Batch StopBatch(int Id, string username);
		Transaction GetTransaction(string id);
	}
}
