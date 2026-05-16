using System.ComponentModel.DataAnnotations;

namespace PNET_Shop.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        public int CheckNo { get; set; }

        public int GoodId { get; set; }

        [Required]
        public DateTime DateSale { get; set; }

        [Range(1, 1000000, ErrorMessage = "Кількість продажу повинна бути більшою за 0")]
        public int Quantity { get; set; }

        public Check? Check { get; set; }

        public Good? Good { get; set; }
    }
}
