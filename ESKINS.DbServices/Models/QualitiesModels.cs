namespace ESKINS.DbServices.Models
{
    public class QualitiesModels
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string Quality { get; set; }
        public ICollection<ItemsModels>? Items { get; set; }
    }
}
