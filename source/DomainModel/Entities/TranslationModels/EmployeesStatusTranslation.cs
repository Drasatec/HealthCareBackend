using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DomainModel.Entities.SettingsEntities;

namespace DomainModel.Entities.TranslationModels;

public partial class EmployeesStatusTranslation
{
    public int Id { get; set; }

    public string StatusName { get; set; } = string.Empty;

    public short EmployeeStatusId { get; set; }

    public string LangCode { get; set; } = null!;

    [JsonIgnore]
    public virtual EmployeesStatus? EmployeeStatus { get; set; }

    //public virtual Language? LangCodeNavigation { get; set; }
}
