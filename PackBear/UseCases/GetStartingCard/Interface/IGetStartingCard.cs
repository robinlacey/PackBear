using PackBear.Adaptor.Interface;
using PackBear.Gateway.Interface;
using PackBear.Player.Interface;
using PackBear.UseCases.GetRandomCard.Interface;

namespace PackBear.UseCases.GetStartingCard.Interface
{
    public interface IGetStartingCard
    {
        void Execute(
            IGetRandomCard getRandomCardUseCase,
            IStartingStats startingStats,
            IPublishMessageAdaptor publishMessageAdaptor,
            IPackGateway packGateway);
    }
}