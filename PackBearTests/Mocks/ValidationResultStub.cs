using PackBear.Card.Interface;
using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBearTests.Mocks
{
    public class ValidationResultStub:IValidationResult
    {
        private readonly ICard _validCardData;
        private readonly bool _returnValue;
        private readonly string _errorMessage;

        public ValidationResultStub(bool returnValue, string errorMessage,ICard validCardData)
        {
            _validCardData = validCardData;
            Valid = returnValue;
            ErrorMessage = errorMessage;
            ValidCardData = _validCardData;
        }
        public bool Valid { get; set; }
        public ICard ValidCardData { get; }
        public string ErrorMessage { get; set; }
    }
}