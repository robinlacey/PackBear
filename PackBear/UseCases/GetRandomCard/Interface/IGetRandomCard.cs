using PackBear.Card.Interface;
using PackBear.Gateway.Interface;

namespace PackBear.UseCases.GetRandomCard.Interface
{
    public interface IGetRandomCard
    {
        // TODO
        ICard Execute(int seed, int turnNumber, int deckVersion, IPackGateway packGateway);
    }
}