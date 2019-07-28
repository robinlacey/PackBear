using PackBear.Card.Options.Interface;
using PackBear.Player.Interface;

namespace PackBearTests.Mocks
{
    public class CardOptionsStub : ICardOption
    {
        public CardOptionsStub(string title, string description, IPlayerStatsToChange playerStatsToChange,
            string[] cardsToAdd)
        {
            Title = title;
            Description = description;
            PlayerStatsToChange = playerStatsToChange;
            CardsToAdd = cardsToAdd;
        }

        public string Title { get; }
        public string Description { get; }
        public IPlayerStatsToChange PlayerStatsToChange { get; }
        public string[] CardsToAdd { get; }
    }
}