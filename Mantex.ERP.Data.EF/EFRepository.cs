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

		public virtual IDbSet<Batch> Batches { get; set; }
		public virtual IDbSet<BeltSection> BeltSections { get; set; }
		public virtual IDbSet<Event> Events { get; set; }
		public virtual IDbSet<EventType> EventTypes { get; set; }
		public virtual IDbSet<LoadingPosition> LoadingPositions { get; set; }
		public virtual IDbSet<Machine> Machines { get; set; }
		public virtual IDbSet<MaterialType> MaterialTypes { get; set; }
		public virtual IDbSet<Measurement> Measurements { get; set; }
		public virtual IDbSet<Session> Sessions { get; set; }
		public virtual IDbSet<Transaction> Transactions { get; set; }
		public virtual IDbSet<Observation> Observations { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Batch>()
				.HasMany(e => e.Observations)
				.WithRequired(e => e.Batch)
				.WillCascadeOnDelete(false);

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

	}
}
