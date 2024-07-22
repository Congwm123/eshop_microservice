namespace Catalog.API.Products.GetProducts;

public record UpdateProductRequest(Product Product);
public record UpdateProductResponse(Product Product);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/product", async (UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductRequest>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
