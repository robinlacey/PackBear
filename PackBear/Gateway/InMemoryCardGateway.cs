using PackBear.Card.Interface;
using PackBear.Gateway.Interface;

namespace PackBear.Gateway
{
    public class InMemoryCardGateway : ICardGateway
    {
        public ICard GetCard(string cardID)
        {
            throw new System.NotImplementedException();
        }

        public void AddCard(ICard card)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCard(string cardID, ICard newCardData)
        {
            throw new System.NotImplementedException();
        }
    }
}