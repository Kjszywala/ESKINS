using System.ComponentModel.DataAnnotations;

namespace ESKINS.DbServices.Models
{
    public class ItemLogsModels
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public string? ItemLogDescription { get; set; }

        public decimal OldPrice { get; set; }

        public decimal? NewPrice { get; set; }
    }
}
