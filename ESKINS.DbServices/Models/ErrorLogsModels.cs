﻿namespace ESKINS.DbServices.Models
{
    public class ErrorLogsModels
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}