namespace ESKINS.DbServices.Models
{
    public class PhasesModels
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string Phase { get; set; }
        public ICollection<ItemsModels>? Item { get; set; }
        public ICollection<TargetsModels>? Target { get; set; }
    }
}
