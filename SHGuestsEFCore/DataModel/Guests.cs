using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHGuestsEFCore.DataModel
{
    public partial class Guests
    {
        public Guests()
        {
            Photos = new HashSet<Photos>();
            VisitsNavigation = new HashSet<Visits>();
        }

        [Key]
        [Column("GuestID")]
        public int GuestId { get; set; }
        [Required]
        [Column(TypeName = "nchar(1)")]
        public string Roster { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime BirthDate { get; set; }
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Column("SSN")]
        public long Ssn { get; set; }
        [Required]
        [Column(TypeName = "nchar(1)")]
        public string Gender { get; set; }
        public int Visits { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastVisitDate { get; set; }

        [InverseProperty("Guest")]
        public ICollection<Photos> Photos { get; set; }
        [InverseProperty("Guest")]
        public ICollection<Visits> VisitsNavigation { get; set; }

        public override string ToString ( )
        {
            return $"{string.Concat ( LastName, ",", FirstName )} {Ssn.ToString ( "000-00-0000" )}";
        }
    }
}
