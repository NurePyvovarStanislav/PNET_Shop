using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PNET_Shop.Models
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SUPPLIER_ID")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Назва постачальника є обов'язковою")]
        [StringLength(30)]
        [Column("NAME")]
        public string Name { get; set; } = string.Empty;

        [StringLength(20)]
        [Column("PHONE")]
        public string? Phone { get; set; }

        [StringLength(60)]
        [Column("ADDRESS", TypeName = "nvarchar(60)")]
        public string? Address { get; set; }

        public ICollection<Good> Goods { get; set; } = new List<Good>();
    }
}
