using System;
using System.Collections.Generic;

namespace WebAPI.My_Models2;

public partial class EmployeesStatusTranslation
{
    public int Id { get; set; }

    public string? StatusName { get; set; }

    public short? EmployeeStatusId { get; set; }

    public string? LangCode { get; set; }

    public virtual EmployeesStatus? EmployeeStatus { get; set; }

    public virtual Language? LangCodeNavigation { get; set; }
}
