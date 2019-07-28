using PackBear.Card.Interface;

namespace PackBear.Gateway.Interface
{
    public interface ICardGateway
    {
        bool HasCard(string cardID);
        ICard GetCard(string cardID);
        void AddCard(ICard card);
        void UpdateCard(ICard card);
    }
}