namespace Mantex.ERP.Data.EF
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
	using Mantex.ERP.Entities;
	using System.Collections.Generic;

	public partial class EFRepository : DbContext, IRepository
	{
		public EFRepository()
			: base("name=EFRepository")
		{
		}

		public virtual DbSet<Batch> Batches { get; set; }
		public virtual DbSet<BeltSection> BeltSections { get; set; }
		public virtual DbSet<Event> Events { get; set; }
		public virtual DbSet<EventType> EventTypes { get; set; }
		public virtual DbSet<LoadingPosition> LoadingPositions { get; set; }
		public virtual DbSet<Machine> Machines { get; set; }
		public virtual DbSet<MaterialType> MaterialTypes { get; set; }
		public virtual DbSet<Measurement> Measurements { get; set; }
		public virtual DbSet<Session> Sessions { get; set; }
		public virtual DbSet<Transaction> Transactions { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Batch>()
				.HasMany(e => e.Sessions)
				.WithRequired(e => e.Batch)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<BeltSection>()
				.HasMany(e => e.Events)
				.WithRequired(e => e.BeltSection)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<BeltSection>()
				.HasMany(e => e.Measurements)
				.WithRequired(e => e.BeltSection)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<EventType>()
				.HasMany(e => e.Events)
				.WithRequired(e => e.EventType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<LoadingPosition>()
				.HasMany(e => e.Batches)
				.WithRequired(e => e.LoadingPosition)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Machine>()
				.HasMany(e => e.BeltSections)
				.WithRequired(e => e.Machine)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<MaterialType>()
				.HasMany(e => e.Batches)
				.WithRequired(e => e.MaterialType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<MaterialType>()
				.HasMany(e => e.Transactions)
				.WithRequired(e => e.MaterialType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Session>()
				.HasMany(e => e.Events)
				.WithRequired(e => e.Session)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Session>()
				.HasMany(e => e.Measurements)
				.WithRequired(e => e.Session)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Transaction>()
				.HasMany(e => e.Batches)
				.WithRequired(e => e.Transaction)
				.WillCascadeOnDelete(false);
		}

		#region IRepository interface

		IDbSet<Batch> IRepository.Batches
		{
			get { return this.Batches; }
		}

		IDbSet<MaterialType> IRepository.MaterialTypes
		{
			get { return this.MaterialTypes; }
		}

		IDbSet<Transaction> IRepository.Transactions
		{
			get { return this.Transactions; }
		}

		void IRepository.SaveChanges()
		{
			this.SaveChanges();
		}

		#endregion IRepository interface
	}
}
