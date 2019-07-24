namespace DealerBear.Card.Interface
{
    public interface ICardOption
    {
        string Title { get; }
        string Description { get; }
        IPlayerStats PlayerStats { get; }
    }
}