using PackBear.Player.Interface;

namespace PackBear.Messages
{
    public interface ICardStructure
    {
        int OptionCount { get; }
        IPlayerStatsToChange StartingStatsToChange { get; }
    }
}