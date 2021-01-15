using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RadioTaxisAPI.Data.Entities
{
    public class DriverEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string Expedido { get; set; }
        public int Ci { get; set; }
        public string Placa { get; set; }
        public string Categoria { get; set; }
        [ForeignKey("BusinessId")]
        public virtual BusinessEntity Business { get; set; }
    }
}
