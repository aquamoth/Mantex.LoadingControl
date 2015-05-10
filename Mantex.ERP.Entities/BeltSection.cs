namespace Mantex.ERP.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

    public partial class BeltSection
    {
        public BeltSection()
        {
            Events = new HashSet<Event>();
            Measurements = new HashSet<Measurement>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int MachineId { get; set; }

        [Required]
        [StringLength(256)]
        public string Rfid { get; set; }

        public virtual Machine Machine { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}
