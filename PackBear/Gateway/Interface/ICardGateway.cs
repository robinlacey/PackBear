using PackBear.Card.Interface;

namespace PackBear.Gateway.Interface
{
    public interface ICardGateway
    {
        ICard GetCard(string cardID);
        void AddCard(ICard card);
        void UpdateCard(string cardID, ICard newCardData);
    }
}