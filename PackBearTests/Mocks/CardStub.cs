using PackBear.Card.Interface;
using PackBear.Card.Options.Interface;

namespace PackBearTests.Mocks
{
    public class CardStub : ICard
    {
        public CardStub(string cardID, string title, string description, string imageUrl, ICardOption[] cardOptions,
            string cardWeight)
        {
            CardID = cardID;
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            Options = cardOptions;
            CardWeight = cardWeight;
        }

        public string CardID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string ImageUrl { get; }
        public string CardWeight { get; }
        public int VersionAdded { get; set; }
        public int? VersionRemoved { get; set; }
        public ICardOption[] Options { get; }
    }
}