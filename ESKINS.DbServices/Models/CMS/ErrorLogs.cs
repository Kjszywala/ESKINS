using System.ComponentModel.DataAnnotations;

namespace ESKINS.DbServices.Models.CMS
{
    public class ErrorLogs
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
