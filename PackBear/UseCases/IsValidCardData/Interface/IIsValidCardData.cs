using PackBear.Adaptor.Interface;
using PackBear.Player.Interface;

namespace PackBear.UseCases.IsValidCardData.Interface
{
    public interface IIsValidCardData
    {
        bool Execute(string card, IJsonDeserializeAdaptor jsonDeserializeAdaptor, IStartingStats startingStats);
    }
}