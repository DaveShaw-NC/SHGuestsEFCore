using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHGuestsEFCore.DataModel
{
    public partial class Visits
    {
        [Key]
        [Column("VisitID")]
        public int VisitId { get; set; }
        public int VisitNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AdmitDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Discharged { get; set; }
        public int VisitDays { get; set; }
        [Required]
        [Column(TypeName = "nchar(1)")]
        public string Roster { get; set; }
        [Required]
        public string AdmitReason { get; set; }
        [Required]
        public string DischargeReason { get; set; }
        [Required]
        public string Agency { get; set; }
        [Required]
        public string Worker { get; set; }
        public int? Room { get; set; }
        public int? Bed { get; set; }
        public bool CanReturn { get; set; }
        public bool Deceased { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EditDate { get; set; }
        [Column("GuestID")]
        public int GuestId { get; set; }

        [ForeignKey("GuestId")]
        [InverseProperty("VisitsNavigation")]
        public Guests Guest { get; set; }
    }
}
