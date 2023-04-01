﻿using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models
{
    public class ErrorLogs
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
