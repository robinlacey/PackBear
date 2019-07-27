using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBearTests.Mocks
{
    public class IsValidCardDataStub:IIsValidCardData
    {
        public bool ReturnValue { get; }

        public IsValidCardDataStub(bool returnValue)
        {
            ReturnValue = returnValue;
        }

        public IValidationResult Execute(string card) => new ValidationResultStub(ReturnValue, "");
    }
}