using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PNET_Shop.Models
{
    public class Department
    {
        [Key]
        [Column(TypeName = "decimal(4,0)")]
        public decimal DeptId { get; set; }

        [Required(ErrorMessage = "Назва відділу є обов'язковою")]
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;

        [StringLength(150)]
        public string? Info { get; set; }

        public ICollection<Good> Goods { get; set; } = new List<Good>();
    }
}
