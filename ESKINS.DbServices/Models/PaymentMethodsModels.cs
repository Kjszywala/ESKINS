namespace ESKINS.DbServices.Models
{
    public class PaymentMethodsModels
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string? ImageName { get; set; }
        public byte[]? Image { get; set; }
        public ICollection<InvoicesModels>? Invoices { get; set; }
    }
}
