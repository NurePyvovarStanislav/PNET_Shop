using System.ComponentModel.DataAnnotations;

namespace PNET_Shop.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Назва постачальника є обов'язковою")]
        [StringLength(30)]
        public string Name { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(60)]
        public string? Address { get; set; }

        public ICollection<Good> Goods { get; set; } = new List<Good>();
    }
}
