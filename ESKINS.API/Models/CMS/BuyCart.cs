using System.ComponentModel.DataAnnotations;
using ESKINS.API.Models;

namespace ESKINS.API.Models.CMS
{
    public class BuyCart
    {
        [Key]
        public int Id { get; set; }
        public string SessionId { get; set; }
        public int? ItemId { get; set; }
        public Items? Item { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }
    }

}
