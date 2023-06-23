using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class SpecialtiesDoctor
{
    public int Id { get; set; }

    public int? DoctorId { get; set; }

    public int? MedicalSpecialtyId { get; set; }

    public DateTime? CreateOn { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual MedicalSpecialty? MedicalSpecialty { get; set; }
}
