using System;
using System.ComponentModel.DataAnnotations;

namespace RadioTaxisAPI.Models
{
    public class MovieEarningsModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { set; get; }
        public string Title { get; set; }
        public uint BoxOffice { get; set; }
        public uint Budget { get; set; }
        public uint Earnings { get; set; }
    }
}
