using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(string id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool isSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (string id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id));
            var response = result.Adapt<DeleteProductResult>();
            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product"); ;
    }
}
