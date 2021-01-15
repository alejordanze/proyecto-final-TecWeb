using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace RadioTaxisAPI.Data.Entities
{
    public class BusinessEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public DateTime FoundationDate { set; get; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<DriverEntity> Drivers { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LogoUrl { get; set; }
    }
}
