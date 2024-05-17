using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMatchProfile.Domain.Entities
{
    [Table("LegalEntity")]
    public partial class LegalEntity
    {
        [Key]
        [Column("idLegalEntity")]
        public int IdLegalEntity { get; set; }

        [StringLength(254)]
        public string Describe { get; set; }

        [InverseProperty("LegalEntityNavigation")]
        public virtual ICollection<LegalContract> LegalContracts { get; set; } = new List<LegalContract>();
    }
}