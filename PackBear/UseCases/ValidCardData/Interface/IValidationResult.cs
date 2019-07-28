using PackBear.Card.Interface;

namespace PackBear.UseCases.IsValidCardData.Interface
{
    public interface IValidationResult
    {
        bool Valid { get; set; }
        ICard ValidCardData { get; }
        string ErrorMessage { get; set; }
    }
}