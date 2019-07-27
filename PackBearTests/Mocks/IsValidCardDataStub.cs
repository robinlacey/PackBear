using PackBear.Card.Interface;
using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBearTests.Mocks
{
    public class IsValidCardDataStub:IIsValidCardData
    {
        private readonly ICard _cardData;
        public bool ReturnValue { get; }

        public IsValidCardDataStub(bool returnValue, ICard cardData)
        {
            _cardData = cardData;
            ReturnValue = returnValue;
        }

        public IValidationResult Execute(string card) => new ValidationResultStub(ReturnValue, "",_cardData);
    }
}