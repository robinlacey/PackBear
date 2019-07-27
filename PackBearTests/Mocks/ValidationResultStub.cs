using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBearTests.Mocks
{
    public class ValidationResultStub:IValidationResult
    {
        private readonly bool _returnValue;
        private readonly string _errorMessage;

        public ValidationResultStub(bool returnValue, string errorMessage)
        {
            Valid = returnValue;
            ErrorMessage = errorMessage;
        }
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }
    }
}