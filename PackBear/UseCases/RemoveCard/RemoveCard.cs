using PackBear.Card.Interface;
using PackBear.Gateway.Interface;
using PackBear.UseCases.IncrementVersionNumber.Interface;
using PackBear.UseCases.RemoveCard.Interface;

namespace PackBear.UseCases.RemoveCard
{
    public class RemoveCard : IRemoveCard
    {
        private readonly ICardGateway _cardGateway;
        private readonly IIncrementVersionNumber _incrementVersionNumber;

        public RemoveCard(ICardGateway cardGateway,  IIncrementVersionNumber incrementVersionNumber)
        {
            _cardGateway = cardGateway;
            _incrementVersionNumber = incrementVersionNumber;
        }

        public void Execute(string cardID)
        {
            if (!_cardGateway.HasCard(cardID))
            {
                return;
            }

            ICard thisCard = _cardGateway.GetCard(cardID);
            thisCard.VersionRemoved = _incrementVersionNumber.Execute();
            _cardGateway.UpdateCard(thisCard);
        }
    }
}