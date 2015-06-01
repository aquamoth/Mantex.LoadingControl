namespace Mantex.ERP.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

    public partial class Measurement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int SessionId { get; set; }

		[StringLength(256)]
		public string TransactionId { get; set; }

        public int BeltSectionId { get; set; }

        public int BeltRevolution { get; set; }

        public int FrameCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public double? MoistureValue { get; set; }

        public double? MoistureConfidence { get; set; }

        public double? WetMassflowValue { get; set; }

        public double? WetMassflowConfidence { get; set; }

        public double? DryMassflowValue { get; set; }

        public double? DryMassflowConfidence { get; set; }

        public double? HeatValue { get; set; }

        public double? HeatConfidence { get; set; }

		[Timestamp]
		public byte[] TS { get; set; }

        public virtual BeltSection BeltSection { get; set; }

        public virtual Session Session { get; set; }

		public virtual Transaction Transaction { get; set; }
    }
}
