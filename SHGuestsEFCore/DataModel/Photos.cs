using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHGuestsEFCore.DataModel
{
    public partial class Photos
    {
        [Key]
        [Column("Photo_ID")]
        public int PhotoId { get; set; }
        public byte[] Photo { get; set; }
        [Column("Guest_ID")]
        public int GuestId { get; set; }

        [ForeignKey("GuestId")]
        [InverseProperty("Photos")]
        public Guests Guest { get; set; }
    }
}
