﻿using DomainModel.Helpers;
using DomainModel.Models.Hospitals;

namespace DomainModel.Models.Buildings;

public class AllBuildingDto
{
    public int Total { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public List<BuildingDto>? Buildings { get; set; } = new List<BuildingDto>();
}
