namespace ESKINS.DbServices.Models
{
    public class InvoicesModels
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int InvoiceNumber { get; set; }
        public int? OrderId { get; set; }
        public OrdersModels? Orders { get; set; }
        public int? PaymentMethodId { get; set; }
        public PaymentMethodsModels? PaymentMethods { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
