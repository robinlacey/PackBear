using PackBear.Messages;

namespace PackBear.UseCases.GetCardStructure.Interface
{
    public interface IGetCardStructure
    {
        // TODO
        // Pull from AppSettings
        ICardStructure Execute();
    }
}