using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities;

public class HospitalMedicalSpecialties
{
    public int HospitalId { get; set; }
    public virtual Hospital? Hospital { get; set; }

    public int SpecialtyId { get; set; }
    public virtual MedicalSpecialty? Specialty { get; set; }
}
