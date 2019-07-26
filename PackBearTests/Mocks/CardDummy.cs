using PackBear.Card.Interface;
using PackBear.Card.Options.Interface;

namespace PackBearTests.Mocks
{
    public class CardDummy:ICard
    {
        public string CardID { get; }
        public string Title { get; }
        public string Description { get; }
        public string ImageURL { get; }
        public string CardWeight { get; }
        public ICardOption[] Options { get; set; }
    }
}