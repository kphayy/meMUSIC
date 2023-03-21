namespace meMUSIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_Album
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaAlbum { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaBaiHat { get; set; }

        [Required]
        [StringLength(30)]
        public string TrangThaiPhatHanh { get; set; }

        public virtual Album Album { get; set; }

        public virtual BaiHat BaiHat { get; set; }
    }
}
