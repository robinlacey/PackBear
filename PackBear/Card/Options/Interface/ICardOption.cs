using PackBear.Player.Interface;

namespace PackBear.Card.Options.Interface
{
    public interface ICardOption
    {
        string Title { get; }
        string Description { get; }
        IPlayerStatsToChange PlayerStatsToChange { get; }
        string[] CardsToAdd { get; }
    }
}