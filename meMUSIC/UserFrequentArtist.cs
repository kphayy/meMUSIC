namespace meMUSIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserFrequentArtist")]
    public partial class UserFrequentArtist
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string username { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaArtist { get; set; }

        public int LuotNgheCaNhan { get; set; }

        public DateTime LanNgheCuoi { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual User User { get; set; }
    }
}
