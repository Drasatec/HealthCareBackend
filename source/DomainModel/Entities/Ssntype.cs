
using DomainModel.Entities.TranslationModels;
using System;
using System.Collections.Generic;

namespace DomainModel.Entities;

public partial class Ssntype
{
    public int Id { get; set; }

    public string CodeNumber { get; set; } = string.Empty;

    //public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<SsntypesTranslation> SsntypesTranslations { get; set; } = new List<SsntypesTranslation>();

}
