using Mantex.ERP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantex.ERP.Services
{
    public class TransactionLogic : Mantex.ERP.Services.ITransactionLogic
    {
		private readonly IRepository repository;

		public TransactionLogic(IRepository repository)
		{
			this.repository = repository;
		}

		public IEnumerable<Entities.Transaction> GetCurrentTransactions()
		{
			return repository.Transactions.Where(t => !t.Batches.Any() || t.Batches.All(b => !b.IsTransactionDone));
		}

		public IEnumerable<Entities.MaterialType> AvailableMaterialTypes()
		{
			return repository.MaterialTypes;
		}

		public Entities.Transaction GetActiveTransaction()
		{
			return repository.Batches
				.Where(b => !b.StoppedAt.HasValue)
				.Select(b => b.Transaction)
				.SingleOrDefault();
		}

		public IEnumerable<Entities.Transaction> GetFinishedTransactions(int pageIndex, int pageSize)
		{
			return repository.Batches
				.Where(b => b.IsTransactionDone)
				.OrderByDescending(b => b.StartedAt)
				.Select(b => b.Transaction)
				.Skip(pageIndex * pageSize)
				.Take(pageSize)
				.ToArray();
		}

		public Entities.Transaction GetTransaction(string id)
		{
			return repository.Transactions
				.Include(t => t.Batches.Select(b => b.Observations))
				.Where(t => t.Id == id)
				.SingleOrDefault();
		}

		public void StartTransaction(string transactionId, int materialTypeId, string username)
		{
			var activeTransaction = GetActiveTransaction();
			if (activeTransaction != null)
				throw new NotSupportedException(string.Format("Transaktion '{0}' måste stoppas först.", activeTransaction.Id));

			var transaction = getTransaction(transactionId);
			var materialType = getMaterialType(materialTypeId);
			var loadionPosition = repository.LoadingPositions.Single();

			addNewBatch(transaction, materialType, loadionPosition, username);
			repository.SaveChanges();
		}

		public Mantex.ERP.Entities.Batch StopBatch(int Id, string username)
		{
#warning Not thread safe!
			var batch = GetBatch(Id);
			if (batch == null)
				throw new ArgumentException(string.Format("Batch '{0}' finns inte.", Id));

			if (batch.StoppedAt.HasValue)
				throw new NotSupportedException(string.Format("Batch '{0}' har redan stoppats.", Id));

			batch.StoppedAt = DateTime.Now;
			batch.StoppedBy = username;
			repository.SaveChanges();
			return batch;
		}

		public void FinishTransaction(string Id, string username)
		{
			var transaction = getTransaction(Id);
			
			var isFinished = transaction.Batches.Any(b => b.IsTransactionDone);
			if (isFinished)
				throw new NotSupportedException("Transaktionen har redan avslutats.");

			var batch = transaction.Batches.SingleOrDefault(b => !b.StoppedAt.HasValue);
			if (batch == null)
				throw new NotSupportedException("Transaktionen måste startas innan den kan avslutas.");

			batch.StoppedAt = DateTime.Now;
			batch.StoppedBy = username;
			batch.IsTransactionDone = true;
			repository.SaveChanges();
		}

		public void Create(Entities.Transaction model)
		{
			var transactionId = "MX-" + model.Id;
			if (GetCurrentTransactions().Any(t => t.Id == transactionId))
				throw new ArgumentException("Transaktionsnumret finns redan.");
			var materialType = getMaterialType(model.MaterialTypeId);

			var transaction = new Entities.Transaction
			{
				Id = transactionId,
				Name = model.Name,
				Description = model.Description,
				MaterialTypeId = materialType.Id,
				MaterialType = materialType,
				MaterialTypeComment = model.MaterialTypeComment,
				ShippingDate = model.ShippingDate,
				ExpectedWeight = model.ExpectedWeight,
				ShippingMethod = model.ShippingMethod,
				Supplier = model.Supplier
			};
			repository.Transactions.Add(transaction);
			repository.SaveChanges();
		}

		public Entities.Batch GetBatch(int id)
		{
			return repository.Batches.Where(b => b.Id == id).Single();
		}

		public IEnumerable<Entities.Observation> GetLastObservations(int pageIndex, int pageSize)
		{
			return repository.Observations
				.Include(o => o.Batch.Transaction.Batches)
				.OrderByDescending(o => o.ObservedAt)
				.Skip(pageIndex * pageSize)
				.Take(pageSize)
				.ToArray();
		}

		public void AddObservation(int batchId, string text, string username)
		{
			var batch = GetBatch(batchId);
			var observation = new Entities.Observation
			{
				BatchId = batchId,
				Batch = batch,
				ObservedAt = DateTime.Now,
				RegisteredAt = DateTime.Now,
				RegisteredBy = username,
				Text = text
			};
			batch.Observations.Add(observation);
			repository.SaveChanges();
		}



		private Entities.Transaction getTransaction(string transactionId)
		{
			var transactions = this.GetCurrentTransactions();
			var transaction = transactions.SingleOrDefault(t => t.Id == transactionId);
			if (transaction == null)
				throw new ArgumentException(string.Format("Transaktion '{0}' hittades inte.", transactionId));
			return transaction;
		}

		private Entities.MaterialType getMaterialType(int? materialTypeId)
		{
			if (!materialTypeId.HasValue)
				return null;

			var materialTypes = this.AvailableMaterialTypes();
			var materialType = materialTypes.FirstOrDefault(mt => mt.Id == materialTypeId);
			if (materialType == null)
				throw new ArgumentException("Materialtypen finns inte.");
			return materialType;
		}

		private void addNewBatch(Entities.Transaction transaction, Entities.MaterialType materialType, Entities.LoadingPosition loadingPosition, string username)
		{
			string transactionId = transaction.Id;
			int materialTypeId = materialType.Id;
			var batch = new Entities.Batch
			{
				Id = new Random().Next(1, 100000),
				MaterialTypeId = materialTypeId,
				MaterialType = materialType,
				LoadingPosition = loadingPosition,
				StartedAt = DateTime.Now,
				StartedBy = username,
				StoppedAt = null,
				StoppedBy = null,
				TransactionId = transactionId,
				Transaction = transaction
			};
			transaction.Batches.Add(batch);
		}
	}
}
