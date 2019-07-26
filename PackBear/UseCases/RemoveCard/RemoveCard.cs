using PackBear.Card.Interface;
using PackBear.Gateway.Interface;
using PackBear.UseCases.RemoveCard.Interface;

namespace PackBear.UseCases.RemoveCard
{
    public class RemoveCard:IRemoveCard
    {
        private readonly ICardGateway _cardGateway;
        private readonly IVersionNumberGateway _versionNumberGateway;

        public RemoveCard(ICardGateway cardGateway, IVersionNumberGateway versionNumberGateway)
        {
            _cardGateway = cardGateway;
            _versionNumberGateway = versionNumberGateway;
        }
        public void Execute(string cardID)
        {
            if (!_cardGateway.HasCard(cardID)) { return; }
            ICard thisCard = _cardGateway.GetCard(cardID);
            thisCard.VersionRemoved = _versionNumberGateway.Get();
            _cardGateway.UpdateCard(thisCard);
        }
    }
}