using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities;

public partial class DoctorAttachment
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateTime? DateProduced { get; set; }

    public DateTime? CreateOn { get; set; }

    public int DoctorId { get; set; }

    public virtual Doctor? Doctor { get; set; }
}
