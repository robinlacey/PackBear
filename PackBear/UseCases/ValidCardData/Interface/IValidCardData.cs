namespace PackBear.UseCases.IsValidCardData.Interface
{
    public interface IValidCardData
    {
        IValidationResult Execute(string card);
    }
}