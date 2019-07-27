using PackBear.Card.Interface;
using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBear.UseCases.IsValidCardData
{
    public class ValidationResult:IValidationResult
    {
        public bool Valid { get; set; }
        public ICard ValidCardData { get; set; }
        public string ErrorMessage { get; set; }
      
    }
}