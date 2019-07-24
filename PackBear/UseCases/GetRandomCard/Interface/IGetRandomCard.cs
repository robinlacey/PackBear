using DealerBear.Card.Interface;

namespace PackBear.UseCases.GetRandomCard.Interface
{
    public interface IGetRandomCard
    {
        // TODO
        ICard Execute(int seed, int deckVersion);
    }
}