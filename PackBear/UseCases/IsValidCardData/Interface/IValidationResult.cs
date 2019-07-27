namespace PackBear.UseCases.IsValidCardData.Interface
{
    public interface IValidationResult
    {
        bool Valid { get; set; }
        string ErrorMessage { get; set; }
    }
}