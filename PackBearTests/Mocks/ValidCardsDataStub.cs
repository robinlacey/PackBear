using PackBear.UseCases.IsValidCardData.Interface;
using PackBear.UseCases.ValidCardsData.Interface;

namespace PackBearTests.Mocks
{
    public class ValidCardsDataStub : IValidCardsData
    {
        private readonly IValidationResult[] _results;

        public ValidCardsDataStub(IValidationResult[] results)
        {
            _results = results;
        }

        public IValidationResult[] Execute(string[] cards) => _results;
    }
}