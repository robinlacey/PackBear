using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBearTests.Mocks
{
    public class ValidCardDataFake : IValidCardData
    {
        private readonly IValidationResult[] _results;

        public ValidCardDataFake(IValidationResult[] results)
        {
            _results = results;
        }

        private int arrayPos = -1;

        public IValidationResult Execute(string card)
        {
            arrayPos++;
            return _results[arrayPos];
        }
    }
}