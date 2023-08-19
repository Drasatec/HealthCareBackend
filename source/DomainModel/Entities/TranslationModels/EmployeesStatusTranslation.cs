using DomainModel.Entities.SettingsEntities;
using System.Text.Json.Serialization;

namespace DomainModel.Entities.TranslationModels;

public partial class EmployeesStatusTranslation
{
    public int Id { get; set; }

    public string StatusName { get; set; } = string.Empty;

    public short EmployeeStatusId { get; set; }

    public string LangCode { get; set; } = null!;

    [JsonIgnore]
    public virtual EmployeesStatus? EmployeeStatus { get; set; }
}
