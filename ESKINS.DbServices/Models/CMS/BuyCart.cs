using System.ComponentModel.DataAnnotations;

namespace ESKINS.DbServices.Models.CMS
{
    public class BuyCart
    {
        [Key]
        public int Id { get; set; }
        public string SessionId { get; set; }
        public int? ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }
    }

}
