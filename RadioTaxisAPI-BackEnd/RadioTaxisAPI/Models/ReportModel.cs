using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RadioTaxisAPI.Models
{
    public class ReportModel
    {
        public int BusinessId {get; set;}
        public string Name { get; set; }
        public DateTime ReportDate { set; get; }
        public IEnumerable<DriverReportModel> Drivers { get; set;}
    }
}
