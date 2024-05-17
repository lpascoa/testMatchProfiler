using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMatchProfile.Domain.Entities
{

    [Table("Author")]
    public partial class Author
    {
        [Key]
        [Column("idAuthor")]
        public int IdAuthor { get; set; }

        [Required]
        [StringLength(254)]
        public string Describe { get; set; }

        [InverseProperty("AuthorNavigation")]
        public virtual ICollection<LegalContract> LegalContracts { get; set; } = new List<LegalContract>();
    }
}