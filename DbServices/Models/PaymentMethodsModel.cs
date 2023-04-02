namespace DbServices.Models
{
    public class PaymentMethodsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string? ImageName { get; set; }
        public byte[]? Image { get; set; }
    }
}
