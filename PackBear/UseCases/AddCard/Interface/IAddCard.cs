using PackBear.Gateway.Interface;
using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBear.UseCases.AddCard.Interface
{
    public interface IAddCard
    {
        bool AddCard(IIsValidCardData isValidCardData, ICardGateway cardGateway);
    }
}