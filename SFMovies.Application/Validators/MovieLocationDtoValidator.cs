using FluentValidation;
using SFMovies.Application.Dto;

namespace SFMovies.Application.Validators;

public class MovieLocationDtoValidator : AbstractValidator<MovieLocationDto>
{
    public MovieLocationDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Locations).NotEmpty().MaximumLength(300);
        RuleFor(x => x.ReleaseYear).InclusiveBetween(1900, 2100).When(x => x.ReleaseYear.HasValue);
        RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).When(x => x.Latitude.HasValue);
        RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).When(x => x.Longitude.HasValue);
    }
}
