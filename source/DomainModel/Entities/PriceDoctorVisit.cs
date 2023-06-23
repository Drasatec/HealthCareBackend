using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities;
public partial class PriceDoctorVisit
{
    public int Id { get; set; }

    public short? Price { get; set; }

    public string? PriceCurrency { get; set; }

    public int? DoctorId { get; set; }

    public int? PriceCategoryId { get; set; }

    public int? TypeVisitId { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual PriceCategory? PriceCategory { get; set; }

    public virtual TypesVisit? TypeVisit { get; set; }
}
