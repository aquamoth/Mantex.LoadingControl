namespace Mantex.ERP.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

    public partial class Machine
    {
        public Machine()
        {
            Batches = new HashSet<Batch>();
            BeltSections = new HashSet<BeltSection>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }

        public virtual ICollection<BeltSection> BeltSections { get; set; }
    }
}
