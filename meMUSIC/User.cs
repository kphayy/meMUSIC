namespace meMUSIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Playlists = new HashSet<Playlist>();
            UserFrequentArtists = new HashSet<UserFrequentArtist>();
        }

        [Key]
        [StringLength(30)]
        public string username { get; set; }

        [Required]
        [StringLength(30)]
        public string matkhau { get; set; }

        [Required]
        [StringLength(20)]
        public string HoUser { get; set; }

        [Required]
        [StringLength(20)]
        public string TenUser { get; set; }

        public DateTime NgayUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Playlist> Playlists { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserFrequentArtist> UserFrequentArtists { get; set; }
    }
}
