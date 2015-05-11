namespace Mantex.ERP.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

    public partial class Observation
    {
		public Observation()
        {
        }

        public int Id { get; set; }

        [Required]
        public int BatchId { get; set; }

		[Required]
		[StringLength(256)]
		public string Text { get; set; }

		public DateTime ObservedAt { get; set; }

		public DateTime RegisteredAt { get; set; }

		[StringLength(256)]
		public string RegisteredBy { get; set; }

        public virtual Batch Batch { get; set; }
    }
}
