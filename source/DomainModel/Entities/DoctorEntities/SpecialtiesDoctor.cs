namespace DomainModel.Entities.DoctorEntities;

public partial class SpecialtiesDoctor
{
    public int DoctorId { get; set; }

    public int MedicalSpecialtyId { get; set; }

    public DateTime? CreateOn { get; set; }
}
