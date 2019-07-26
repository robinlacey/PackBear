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
        public int VersionAdded { get; set; }
        public int? VersionRemoved { get; set; }
        public ICardOption[] Options { get;}
    }
}