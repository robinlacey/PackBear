using PackBear.Card.Interface;
using PackBear.Gateway.Interface;

namespace PackBear.Gateway
{
    public class InMemoryCardGateway : ICardGateway
    {
        public bool HasCard(string cardID)
        {
            throw new System.NotImplementedException();
        }

        public ICard GetCard(string cardID)
        {
            throw new System.NotImplementedException();
        }

        public void AddCard(ICard card)
        {
            throw new System.NotImplementedException();
        }
        
        public void UpdateCard(ICard newCardData)
        {
            throw new System.NotImplementedException();
        }
    }
}