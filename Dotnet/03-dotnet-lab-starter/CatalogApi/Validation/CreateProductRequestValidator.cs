using CatalogApi.Dtos;
using FluentValidation;

namespace CatalogApi.Validation;

// FluentValidation: the rules a CreateProductRequest must pass before we touch any data.
// The controller runs this and returns 400 with the errors if it fails.
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        // TODO (you) — Day 3: the rules. One RuleFor per property:
        //   Name  → NotEmpty, MaximumLength(120)
        //   Price → GreaterThan(0)   (a product can't cost zero or less)
    }
}
