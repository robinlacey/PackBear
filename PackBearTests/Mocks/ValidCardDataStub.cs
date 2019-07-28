using PackBear.Card.Interface;
using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBearTests.Mocks
{
    public class ValidCardDataStub : IValidCardData
    {
        private readonly ICard _cardData;
        public bool ReturnValue { get; }

        public ValidCardDataStub(bool returnValue, ICard cardData)
        {
            _cardData = cardData;
            ReturnValue = returnValue;
        }

        public IValidationResult Execute(string card) => new ValidationResultStub(ReturnValue, "", _cardData);
    }
}