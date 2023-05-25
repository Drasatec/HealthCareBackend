using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models.Hospitals
{
    public class HospitalRequest
    {
        public string? Photo { get; set; }

        public DateTime? CreateOn { get; set; }
        public string? Name { get; set; }
        public short? Number { get; set; } = 10;
        public string? Address { get; set; }
    }
}
