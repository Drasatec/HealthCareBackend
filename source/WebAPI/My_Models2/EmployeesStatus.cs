using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class EmployeesStatus
{
    public short Id { get; set; }

    public DateTime? CreateOn { get; set; }

    public virtual ICollection<EmployeesStatusTranslation> EmployeesStatusTranslations { get; set; } = new List<EmployeesStatusTranslation>();
}
