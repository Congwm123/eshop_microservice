namespace Catalog.API.Products.GetProducts;

//public record GetProductByIdRequest();
public record GetProductByCategoryResponse(Product Product);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{category}", async (string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByCategoryQuery(category));
            var response = result.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProductByCategory")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
    }
}
