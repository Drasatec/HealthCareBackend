using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DomainModel.Entities;

public partial class DoctorAttachment
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? AttachFileName { get; set; }

    public DateTime? DateProduced { get; set; }

    public DateTime? CreateOn { get; set; }

    public int DoctorId { get; set; }

   // [JsonIgnore]
   // public virtual Doctor? Doctor { get; set; }
}
