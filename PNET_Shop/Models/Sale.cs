using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PNET_Shop.Models
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SALE_ID")]
        public int SaleId { get; set; }

        [Required(ErrorMessage = "Номер чека є обов'язковим")]
        [Column("CHECK_NO")]
        public int CheckNo { get; set; }

        [Required(ErrorMessage = "Товар є обов'язковим")]
        [Column("GOOD_ID")]
        public int GoodId { get; set; }

        [Required(ErrorMessage = "Дата продажу є обов'язковою")]
        [Column("DATE_SALE")]
        public DateTime DateSale { get; set; }

        [Required(ErrorMessage = "Кількість є обов'язковою")]
        [Range(1, 1000000, ErrorMessage = "Кількість продажу повинна бути більшою за 0")]
        [Column("QUANTITY")]
        public int Quantity { get; set; }

        [ForeignKey(nameof(CheckNo))]
        public Check? Check { get; set; }

        [ForeignKey(nameof(GoodId))]
        public Good? Good { get; set; }
    }
}
