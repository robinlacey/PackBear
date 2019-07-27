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
        private readonly IVersionNumberGateway _versionNumberGateway;

        public AddCard(
            IIsValidCardData validCardData,  
            ICardGateway cardGateway,
            IVersionNumberGateway versionNumberGateway)
        {
            _validCardData = validCardData;
            _cardGateway = cardGateway;
            _versionNumberGateway = versionNumberGateway;
        }

        public void Execute(string json)
        {
            IValidationResult validationResult = _validCardData.Execute(json);
            if (validationResult.Valid)
            {
                validationResult.ValidCardData.VersionAdded = _versionNumberGateway.Get();
                if (_cardGateway.HasCard( validationResult.ValidCardData.CardID))
                {
                    _cardGateway.UpdateCard( validationResult.ValidCardData);
                }
                else
                {
                    _cardGateway.AddCard(validationResult.ValidCardData);
                }
            }
            else
            {
                //TODO throw exception with error message
            }
        }
    }
}