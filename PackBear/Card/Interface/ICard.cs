using PackBear.Card.Options.Interface;

namespace PackBear.Card.Interface
{
    public interface ICard
    {
        string CardID { get; }
        string Title { get; }
        string Description { get; }
        string ImageURL { get; }
        string CardWeight { get; }
        ICardOption[] Options { get; set; }
    }
}