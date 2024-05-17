using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMatchProfile.Domain.Entities
{
    [Table("LegalContract")]
    public partial class LegalContract
    {
        [Key]
        public int IdProcess { get; set; }

        public int Author { get; set; }

        public int LegalEntity { get; set; }

        public string DescribeLegalEntity { get; set; }

        public DateTime? CreatedProcess { get; set; }

        public DateTime? UpdatedProcess { get; set; }

        [ForeignKey("Author")]
        [InverseProperty("LegalContracts")]
        public virtual Author AuthorNavigation { get; set; }

        [ForeignKey("LegalEntity")]
        [InverseProperty("LegalContracts")]
        public virtual LegalEntity LegalEntityNavigation { get; set; }
    }
}