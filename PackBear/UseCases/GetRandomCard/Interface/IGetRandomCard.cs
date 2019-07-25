using DealerBear.Card.Interface;
using PackBear.Gateway.Interface;

namespace PackBear.UseCases.GetRandomCard.Interface
{
    public interface IGetRandomCard
    {
        // TODO
        ICard Execute(int seed, int deckVersion, IPackDataGateway packDataGateway);
    }
}