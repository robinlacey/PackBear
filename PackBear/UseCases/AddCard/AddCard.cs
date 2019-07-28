using PackBear.Gateway.Interface;
using PackBear.UseCases.AddCard.Interface;
using PackBear.UseCases.IncrementVersionNumber.Interface;
using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBear.UseCases.AddCard
{
    public class AddCard : IAddCard
    {
        private readonly IValidCardData _validCardData;
        private readonly ICardGateway _cardGateway;
        private readonly IIncrementVersionNumber _incrementVersionNumber;

        public AddCard(
            IValidCardData validCardData,
            ICardGateway cardGateway,
            IIncrementVersionNumber incrementVersionNumber)
        {
            _validCardData = validCardData;
            _cardGateway = cardGateway;
            _incrementVersionNumber = incrementVersionNumber;
        }

        public void Execute(string json)
        {
            IValidationResult validationResult = _validCardData.Execute(json);
            if (validationResult.Valid)
            {
                validationResult.ValidCardData.VersionAdded = _incrementVersionNumber.Execute();
                
                if (_cardGateway.HasCard(validationResult.ValidCardData.CardID))
                {
                    _cardGateway.UpdateCard(validationResult.ValidCardData);
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