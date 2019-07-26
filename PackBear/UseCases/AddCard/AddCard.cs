using PackBear.Adaptor.Interface;
using PackBear.Card.Interface;
using PackBear.Gateway.Interface;
using PackBear.UseCases.AddCard.Interface;
using PackBear.UseCases.IsValidCardData.Interface;
using PackBear.UseCases.UpdateVersionNumber.Interface;

namespace PackBear.UseCases.AddCard
{
    public class AddCard:IAddCard
    {
        private readonly IIsValidCardData _validCardData;
        private readonly IUpdateVersionNumber _updateVersionNumber;
        private readonly IVersionNumberGateway _versionNumberGateway;
        private readonly ICardGateway _cardGateway;
        private readonly IJsonDeserializeAdaptor _jsonDeserializeAdaptor;

        public AddCard(
            IIsValidCardData validCardData, 
            IUpdateVersionNumber updateVersionNumber, 
            IVersionNumberGateway versionNumberGateway, 
            ICardGateway cardGateway,
            IJsonDeserializeAdaptor jsonDeserializeAdaptor)
        {
            _validCardData = validCardData;
            _updateVersionNumber = updateVersionNumber;
            _versionNumberGateway = versionNumberGateway;
            _cardGateway = cardGateway;
            _jsonDeserializeAdaptor = jsonDeserializeAdaptor;
        }

        public void Execute(string json)
        {
            if (_validCardData.Execute(json))
            {
                _updateVersionNumber.Execute(_versionNumberGateway.Get()+1);
                
                ICard card = _jsonDeserializeAdaptor.DeserializeCard(json);

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