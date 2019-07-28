using PackBear.UseCases.IsValidCardData.Interface;
using PackBear.UseCases.ValidCardsData.Interface;

namespace PackBear.UseCases.ValidCardsData
{
    public class ValidCardsData : IValidCardsData
    {
        private readonly IValidCardData _validCardDataUseCase;

        public ValidCardsData(IValidCardData validCardDataUseCase)
        {
            _validCardDataUseCase = validCardDataUseCase;
        }

        public IValidationResult[] Execute(string[] cards)
        {
            IValidationResult[] validationResults = new IValidationResult[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                validationResults[i] = _validCardDataUseCase.Execute(cards[i]);
            }

            return validationResults;
        }
    }
}