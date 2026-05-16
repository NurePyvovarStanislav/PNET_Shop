using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PNET_Shop.Models
{
    public class Check
    {
        [Key]
        public int CheckNo { get; set; }

        [Required]
        public DateTime CheckDate { get; set; }

        [Range(0, 1000000)]
        public double TotalSum { get; set; }

        [StringLength(30)]
        public string? CashierName { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
