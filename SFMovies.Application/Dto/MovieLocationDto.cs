using System.ComponentModel.DataAnnotations;

namespace SFMovies.Application.Dto;

public class MovieLocationDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Range(1900, 2100)]
    public int? ReleaseYear { get; set; }

    [Required]
    [StringLength(300)]
    public string Locations { get; set; } = string.Empty;

    public string? FunFacts { get; set; }
    public string? ProductionCompany { get; set; }
    public string? Distributor { get; set; }
    public string? Director { get; set; }
    public string? Writer { get; set; }
    public string? Actor1 { get; set; }
    public string? Actor2 { get; set; }
    public string? Actor3 { get; set; }

    [Range(-90, 90)]
    public double? Latitude { get; set; }
    [Range(-180, 180)]
    public double? Longitude { get; set; }
}
