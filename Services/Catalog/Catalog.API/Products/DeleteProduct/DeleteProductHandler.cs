namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductRequest(Guid Id) : ICommand<DeleteProductResponse>;
public record DeleteProductResponse(bool isSuccess);

public class DeleteProductHandler(IDocumentSession session) : ICommandHandler<DeleteProductRequest, DeleteProductResponse>
{
    public async Task<DeleteProductResponse> Handle(DeleteProductRequest command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == default)
        {
            throw new ProductNotFoundException(command.Id);
        }
        session.Delete(product);
        await session.SaveChangesAsync();
        return new DeleteProductResponse(true);
    }
}
