using DealerBear.Card.Interface;

namespace PackBear.Messages
{
    public interface ICardStructure
    {
        int OptionCount { get; }
        IPlayerStats StartingStats { get; }
    }
}