namespace PackBear.UseCases.IsValidCardData.Interface
{
    public interface IIsValidCardData
    {
        IValidationResult Execute(string card);
    }
}