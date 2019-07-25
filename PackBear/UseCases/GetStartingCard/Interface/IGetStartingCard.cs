using PackBear.Adaptor.Interface;
using PackBear.Gateway.Interface;
using PackBear.UseCases.GetRandomCard.Interface;
using PackBear.UseCases.GetStartingStats.Interface;

namespace Tests.UseCase.GetStartingCard.Interface
{
    public interface IGetStartingCard
    {
        void Execute(
            IGetRandomCard getRandomCardUseCase, 
            IGetStartingStats getStartingStatsUseCase,
            IPublishMessageAdaptor publishMessageAdaptor,
            IPackDataGateway packDataGateway);
    }
}