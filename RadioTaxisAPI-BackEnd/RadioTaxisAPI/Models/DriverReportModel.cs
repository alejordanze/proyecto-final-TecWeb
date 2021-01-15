using System;
using System.ComponentModel.DataAnnotations;
using RadioTaxisAPI.Data.Entities;
using System.Collections.Generic;

namespace RadioTaxisAPI.Models
{
    public class DriverReportModel
    {
        public string Category { get; set; }
        public int Quantity { get; set; }
        public List<DriverEntity> Drivers { get; set; }
    }
}
