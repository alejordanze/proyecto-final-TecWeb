using System;
using Microsoft.AspNetCore.Http;

namespace RadioTaxisAPI.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
