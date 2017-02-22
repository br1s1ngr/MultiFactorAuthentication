namespace MultiFaceRec
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mfa.otp")]
    public partial class otp
    {
        public int id { get; set; }

        public int userId { get; set; }

        [Column("otp")]
        public int otp1 { get; set; }

        public bool used { get; set; }

        public DateTime timesent { get; set; }

        public DateTime timeused { get; set; }

        [StringLength(20)]
        public string status { get; set; }

        public virtual user user { get; set; }
    }
}
