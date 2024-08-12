using BuildingBlocks.Messaging.Events;
using FluentValidation;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record BasketCheckoutCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<BasketCheckoutResult>;
public record BasketCheckoutResult(bool IsSuccess);

public class BasketCheckoutCommandValidator : AbstractValidator<BasketCheckoutCommand>
{
    public BasketCheckoutCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto cant be null");
    }
}

public class CheckoutBasketHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint) : ICommandHandler<BasketCheckoutCommand, BasketCheckoutResult>
{
    public async Task<BasketCheckoutResult> Handle(BasketCheckoutCommand command, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        if(basket == null)
        {
            return new BasketCheckoutResult(false);
        }

        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await basketRepository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        return new BasketCheckoutResult(true);
    }
}
