namespace Mantex.ERP.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

    public partial class MaterialType
    {
        public MaterialType()
        {
            Batches = new HashSet<Batch>();
            Transactions = new HashSet<Transaction>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
