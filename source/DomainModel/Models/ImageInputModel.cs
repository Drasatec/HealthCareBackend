﻿
namespace DomainModel.Models;

public class ImageInputModel
{
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public Stream Content { get; set; } = null!;
}

