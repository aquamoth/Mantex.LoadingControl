using Mantex.ERP.Entities;
using System;
using System.Collections.Generic;

namespace Mantex.ERP.Services
{
	public interface ITransactionLogic
	{
		void AddObservation(int batchId, string text, string username);
		IEnumerable<MaterialType> AvailableMaterialTypes();
		void Create(Transaction model);
		void FinishTransaction(string Id, string username);
		Transaction GetActiveTransaction();
		Entities.Batch GetBatch(int id);
		IEnumerable<Transaction> GetCurrentTransactions();
		IEnumerable<Transaction> GetFinishedTransactions(int pageIndex, int pageSize);
		IEnumerable<Observation> GetLastObservations(int pageIndex, int pageSize);
		Transaction GetTransaction(string id);
		void StartTransaction(string transactionId, int materialTypeId, string username);
		Batch StopBatch(int Id, string username);
	}
}
