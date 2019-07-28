using PackBear.Card.Interface;
using PackBear.Gateway.Interface;

namespace PackBearTests.Mocks
{
    public class CardGatewayDummy : ICardGateway
    {
        public bool HasCard(string cardID)
        {
            return false;
        }

        public ICard GetCard(string cardID) => new CardDummy();

        public void AddCard(ICard card)
        {
        }

        public void UpdateCard(ICard newCardData)
        {
        }
    }
}