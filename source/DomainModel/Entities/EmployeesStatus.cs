using System;
using System.Collections.Generic;
using DomainModel.Entities.TranslationModels;

namespace DomainModel.Entities;

public partial class EmployeesStatus
{
    public short Id { get; set; }

    public DateTime? CreateOn { get; set; }

    public virtual ICollection<EmployeesStatusTranslation> EmployeesStatusTranslations { get; set; } = new List<EmployeesStatusTranslation>();
}
