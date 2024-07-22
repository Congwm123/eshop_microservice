namespace Catalog.API.Products.GetProducts;

public record UpdateProductCommand(Product Product) : ICommand<UpdateProductResult>;
public record UpdateProductResult (Product Product);

internal class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Product.Id, cancellationToken);
        if(product == default)
        {
            throw new ProductNotFoundException(command.Product.Id);
        }
        product.Name = command.Product.Name;
        product.Description = command.Product.Description;
        product.Category = command.Product.Category;
        product.Price = command.Product.Price;

        session.Update(product);
        await session.SaveChangesAsync();
        return new UpdateProductResult(product);
    }
}
