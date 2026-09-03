using CatalogApi.Dtos;
using FluentValidation;

namespace CatalogApi.Validation;

// FluentValidation: the rules a CreateProductRequest must pass before we touch the database.
// The controller runs this and returns 400 with the errors if it fails.
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.CategoryId).GreaterThan(0); // new with the Category relationship (day 4)
    }
}
