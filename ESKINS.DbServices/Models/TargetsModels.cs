namespace ESKINS.DbServices.Models
{
    public class TargetsModels
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int? UserId { get; set; }
        public UsersModels? User { get; set; }
        public decimal PriceUnder { get; set; }
        public int? ItemId { get; set; }
        public ItemsModels? Item { get; set; }
        public int? PhaseId { get; set; }
        public PhasesModels? Phase { get; set; }
        public string UnderFloat { get; set; }
    }
}
