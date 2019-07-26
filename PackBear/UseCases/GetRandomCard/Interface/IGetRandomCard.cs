using PackBear.Card.Interface;
using PackBear.Gateway.Interface;

namespace PackBear.UseCases.GetRandomCard.Interface
{
    public interface IGetRandomCard
    {
        ICard Execute(int seed, int turnNumber, int deckVersion);
    }
}