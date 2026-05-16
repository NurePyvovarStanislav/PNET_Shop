using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PNET_Shop.Models
{
    public class Check
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CHECK_NO")]
        public int CheckNo { get; set; }

        [Required(ErrorMessage = "Дата чека є обов'язковою")]
        [Column("CHECK_DATE")]
        public DateTime CheckDate { get; set; }

        [Column("TOTAL_SUM")]
        public double TotalSum { get; set; }

        [StringLength(30)]
        [Column("CASHIER_NAME")]
        public string? CashierName { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
