namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(Product Product) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductrHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        session.Store(command.Product);
        await session.SaveChangesAsync();
        return new CreateProductResult(Guid.NewGuid());
    }
}
