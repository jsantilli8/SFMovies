namespace SFMovies.Application.DTOs;

public class MovieLocationDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int? ReleaseYear { get; set; }
    public string Locations { get; set; } = string.Empty;
    public string? FunFacts { get; set; }
    public string? ProductionCompany { get; set; }
    public string? Distributor { get; set; }
    public string? Director { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}