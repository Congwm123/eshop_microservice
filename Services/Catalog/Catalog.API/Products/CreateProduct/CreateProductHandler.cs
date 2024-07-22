
namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(Product Product) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is not empty");
    }
}
internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        session.Store(command.Product);
        await session.SaveChangesAsync();
        return new CreateProductResult(command.Product.Id);
    }
}
