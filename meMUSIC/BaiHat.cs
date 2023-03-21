namespace meMUSIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiHat")]
    public partial class BaiHat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiHat()
        {
            CT_Album = new HashSet<CT_Album>();
            CT_Playlist = new HashSet<CT_Playlist>();
        }

        [Key]
        [StringLength(20)]
        public string MaBaiHat { get; set; }

        [Required]
        [StringLength(100)]
        public string TenBaiHat { get; set; }

        [Required]
        [StringLength(20)]
        public string MaArtist { get; set; }

        public int NamPhatHanh { get; set; }

        [Required]
        [StringLength(20)]
        public string TheLoai1 { get; set; }

        [Required]
        [StringLength(20)]
        public string TheLoai2 { get; set; }

        public string BHKeyword { get; set; }

        public int LuotNghe { get; set; }

        public virtual Artist Artist { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_Album> CT_Album { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_Playlist> CT_Playlist { get; set; }
    }
}
