namespace meMUSIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Playlist")]
    public partial class Playlist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Playlist()
        {
            CT_Playlist = new HashSet<CT_Playlist>();
        }

        [Key]
        [StringLength(50)]
        public string MaPlaylist { get; set; }

        [Required]
        [StringLength(100)]
        public string TenPlaylist { get; set; }

        [Required]
        [StringLength(30)]
        public string username { get; set; }

        public DateTime NgayPlaylist { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_Playlist> CT_Playlist { get; set; }

        public virtual User User { get; set; }
    }
}
