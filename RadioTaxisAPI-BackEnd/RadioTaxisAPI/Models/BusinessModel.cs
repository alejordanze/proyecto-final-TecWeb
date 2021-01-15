using System;
using System.ComponentModel.DataAnnotations;
using RadioTaxisAPI.Data.Entities;
using System.Collections.Generic;

namespace RadioTaxisAPI.Models
{
    public class BusinessModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime FoundationDate { set; get; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LogoUrl { get; set; }

        public IEnumerable<DriverEntity> Drivers { get; set; }
    }
}
