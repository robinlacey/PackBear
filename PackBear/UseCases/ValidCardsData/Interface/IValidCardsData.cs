using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBear.UseCases.ValidCardsData.Interface
{
    public interface IValidCardsData
    {
        IValidationResult[] Execute(string[] cards);
    }
}