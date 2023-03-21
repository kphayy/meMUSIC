namespace meMUSIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Album")]
    public partial class Album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            CT_Album = new HashSet<CT_Album>();
        }

        [Key]
        [StringLength(20)]
        public string MaAlbum { get; set; }

        [Required]
        [StringLength(100)]
        public string TenAlbum { get; set; }

        [Required]
        [StringLength(20)]
        public string MaArtist { get; set; }

        public int NamAlbum { get; set; }

        public string AlKeyword { get; set; }

        public virtual Artist Artist { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_Album> CT_Album { get; set; }
    }
}
