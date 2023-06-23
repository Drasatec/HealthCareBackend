using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DomainModel.Entities.TranslationModels;

public partial class SsntypesTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? SsntypeId { get; set; }

    public string LangCode { get; set; } = null!;


    //public virtual Language? LangCodeNavigation { get; set; }

    [JsonIgnore]
    public virtual Ssntype? Ssntype { get; set; }
}
