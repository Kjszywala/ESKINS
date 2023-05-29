namespace ESKINS.DbServices.Models
{
    public class CartModels
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public int? ItemId { get; set; }
        public ItemsModels? Item { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
