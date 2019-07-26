using PackBear.Adaptor.Interface;
using PackBear.Card.Interface;
using PackBear.Gateway.Interface;
using PackBear.UseCases.AddCard.Interface;
using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBear.UseCases.AddCard
{
    public class AddCard:IAddCard
    {
        private readonly IIsValidCardData _validCardData;
        private readonly ICardGateway _cardGateway;
        private readonly IJsonDeserializeAdaptor _jsonDeserializeAdaptor;
        private readonly IVersionNumberGateway _versionNumberGateway;

        public AddCard(
            IIsValidCardData validCardData,  
            ICardGateway cardGateway,
            IJsonDeserializeAdaptor jsonDeserializeAdaptor,
            IVersionNumberGateway versionNumberGateway)
        {
            _validCardData = validCardData;
            _cardGateway = cardGateway;
            _jsonDeserializeAdaptor = jsonDeserializeAdaptor;
            _versionNumberGateway = versionNumberGateway;
        }

        public void Execute(string json)
        {
            if (_validCardData.Execute(json))
            {
                ICard card = _jsonDeserializeAdaptor.DeserializeCard(json);

                card.VersionAdded = _versionNumberGateway.Get();
                if (_cardGateway.HasCard(card.CardID))
                {
                    _cardGateway.UpdateCard(card);
                }
                else
                {
                    _cardGateway.AddCard(card);
                }
            }
        }
    }
}