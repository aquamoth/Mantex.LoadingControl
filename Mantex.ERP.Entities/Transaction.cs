namespace Mantex.ERP.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
	//using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        public Transaction()
        {
            Batches = new HashSet<Batch>();
        }

        [StringLength(256)]
        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public int MaterialTypeId { get; set; }

        public int? LoadingPositionId { get; set; }

        public DateTime? ShippingDate { get; set; }

        [StringLength(256)]
        public string Supplier { get; set; }

        public double? ExpectedWeight { get; set; }

        [StringLength(256)]
        public string ShippingMethod { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }

        public virtual LoadingPosition LoadingPosition { get; set; }

        public virtual MaterialType MaterialType { get; set; }


		public Batch LastBatch
		{
			get
			{
				return this.Batches.OrderByDescending(b => b.StartedAt).FirstOrDefault();
			}
		}

		public Batch ActiveBatch
		{
			get
			{
				return this.Batches.SingleOrDefault(b => !b.StoppedAt.HasValue);
			}
		}

		public int BatchNumberOf(Batch batch)
		{
			return this.Batches
				.OrderBy(b => b.StartedAt)
				.ToList()
				.IndexOf(batch) + 1;
		}
	}
}
