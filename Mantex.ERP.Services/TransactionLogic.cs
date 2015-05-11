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

		public Entities.Transaction GetTransaction(string id)
		{
			return repository.Transactions
				.Include(t => t.Batches)
				.Where(t => t.Id == id)
				.SingleOrDefault();
		}

		public void StartTransaction(string transactionId, int materialTypeId)
		{
			var activeTransaction = GetActiveTransaction();
			if (activeTransaction != null)
				throw new NotSupportedException(string.Format("Transaktion '{0}' måste stoppas först.", activeTransaction.Id));

			var transaction = getTransaction(transactionId);
			var materialType = getMaterialType(materialTypeId);
			var loadionPosition = repository.LoadingPositions.Single();

			addNewBatch(transaction, materialType, loadionPosition);
			repository.SaveChanges();
		}

		public Mantex.ERP.Entities.Batch StopBatch(int Id)
		{
#warning Not thread safe!
			var batch = getBatch(Id);
			if (batch == null)
				throw new ArgumentException(string.Format("Batch '{0}' finns inte.", Id));

			if (batch.StoppedAt.HasValue)
				throw new NotSupportedException(string.Format("Batch '{0}' har redan stoppats.", Id));

			batch.StoppedAt = DateTime.Now;
			//TODO: batch.StoppedBy = ???;
			repository.SaveChanges();
			return batch;
		}

		public void FinishTransaction(string Id)
		{
			var transaction = getTransaction(Id);
			
			var isFinished = transaction.Batches.Any(b => b.IsTransactionDone);
			if (isFinished)
				throw new NotSupportedException("Transaktionen har redan avslutats.");

			var batch = transaction.Batches.SingleOrDefault(b => !b.StoppedAt.HasValue);
			if (batch == null)
				throw new NotSupportedException("Transaktionen måste startas innan den kan avslutas.");

			batch.StoppedAt = DateTime.Now;
			//TODO: batch.StoppedBy = ???;
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
				ShippingDate = model.ShippingDate,
				ExpectedWeight = model.ExpectedWeight,
				ShippingMethod = model.ShippingMethod,
				Supplier = model.Supplier
			};
			repository.Transactions.Add(transaction);
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

		private Entities.MaterialType getMaterialType(int materialTypeId)
		{
			var materialTypes = this.AvailableMaterialTypes();
			var materialType = materialTypes.FirstOrDefault(mt => mt.Id == materialTypeId);
			if (materialType == null)
				throw new ArgumentException("Materialtypen finns inte.");
			return materialType;
		}

		private Entities.Batch getBatch(int id)
		{
			var transactions = this.GetCurrentTransactions();
			var batches = transactions.SelectMany(t => t.Batches);
			var batch = batches.SingleOrDefault(b => b.Id == id);
			return batch;
		}

		private void addNewBatch(Entities.Transaction transaction, Entities.MaterialType materialType, Entities.LoadingPosition loadingPosition)
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
				//TODO: StartedBy = ???,
				StoppedAt = null,
				TransactionId = transactionId,
				Transaction = transaction
			};
			transaction.Batches.Add(batch);
		}
	}
}
