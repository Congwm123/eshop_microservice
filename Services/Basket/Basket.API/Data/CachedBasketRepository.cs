
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
            return JsonConvert.DeserializeObject<ShoppingCart>(cachedBasket)!;
        var basket = await basketRepository.GetBasket(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonConvert.SerializeObject(basket), cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await basketRepository.StoreBasket(basket, cancellationToken);
        await cache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket), cancellationToken);
        return basket;
    }
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasket(userName, cancellationToken);
        await cache.RemoveAsync(userName, cancellationToken);
        return true;
    }
}
