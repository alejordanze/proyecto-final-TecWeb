using System;
using System.ComponentModel.DataAnnotations;

namespace RadioTaxisAPI.Models
{
    public class DriverModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public int BusinessId { get; set; }
        public string Expedido { get; set; }
        public int Ci { get; set; }
        public string Placa { get; set; }
        public string Categoria { get; set; }
    }
}
