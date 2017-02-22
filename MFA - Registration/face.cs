namespace MultiFaceRec
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mfa.faces")]
    public partial class face
    {
        public int id { get; set; }

        public int userid { get; set; }

        [Column("face", TypeName = "blob")]
        [Required]
        public byte[] face1 { get; set; }

        public DateTime date_created { get; set; }

        public virtual user user { get; set; }
    }
}
