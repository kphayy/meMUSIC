namespace meMUSIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_Playlist
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string MaPlaylist { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaBaiHat { get; set; }

        public DateTime ThoiGianThem { get; set; }

        public virtual BaiHat BaiHat { get; set; }

        public virtual Playlist Playlist { get; set; }
    }
}
