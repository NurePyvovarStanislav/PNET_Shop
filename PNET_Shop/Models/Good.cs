using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PNET_Shop.Models
{
    public class Good
    {
        [Key]
        [Column("GOOD_ID")]
        public int GoodId { get; set; }

        [Required(ErrorMessage = "Назва товару є обов'язковою")]
        [StringLength(20)]
        [Column("NAME")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ціна є обов'язковою")]
        [Range(0.01, 1000000, ErrorMessage = "Ціна повинна бути більшою за 0")]
        [Column("PRICE")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Кількість є обов'язковою")]
        [Range(0, 1000000, ErrorMessage = "Кількість не може бути від'ємною")]
        [Column("QUANTITY")]
        public int Quantity { get; set; }

        [StringLength(20)]
        [Column("PRODUCER")]
        public string? Producer { get; set; }

        [Required(ErrorMessage = "Відділ є обов'язковим")]
        [Column("DEPT_ID", TypeName = "decimal(4,0)")]
        public decimal DeptId { get; set; }

        [Required(ErrorMessage = "Постачальник є обов'язковим")]
        [Column("SUPPLIER_ID")]
        public int SupplierId { get; set; }

        [StringLength(50)]
        [Column("DESCRIPTION", TypeName = "nvarchar(50)")]
        public string? Description { get; set; }

        [ForeignKey(nameof(DeptId))]
        public Department? Department { get; set; }

        [ForeignKey(nameof(SupplierId))]
        public Supplier? Supplier { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
