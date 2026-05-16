using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PNET_Shop.Models
{
    public class Good
    {
        [Key]
        public int GoodId { get; set; }

        [Required(ErrorMessage = "Назва товару є обов'язковою")]
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ціна є обов'язковою")]
        [Range(0.01, 1000000, ErrorMessage = "Ціна повинна бути більшою за 0")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Кількість є обов'язковою")]
        [Range(0, 1000000, ErrorMessage = "Кількість не може бути від'ємною")]
        public int Quantity { get; set; }

        [StringLength(20)]
        public string? Producer { get; set; }

        [Column(TypeName = "decimal(4,0)")]
        public decimal DeptId { get; set; }

        public int SupplierId { get; set; }

        [StringLength(50)]
        public string? Description { get; set; }

        public Department? Department { get; set; }

        public Supplier? Supplier { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
