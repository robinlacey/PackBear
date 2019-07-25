using PackBear.Adaptor.Interface;
using PackBear.Gateway.Interface;
using PackBear.UseCases.GetRandomCard.Interface;
using PackBear.UseCases.GetStartingStats.Interface;
using Tests.UseCase.GetStartingCard.Interface;

namespace PackBear.UseCases.GetStartingCard
{
    public class GetStartingCard:IGetStartingCard
    {
        public void Execute(IGetRandomCard getRandomCardUseCase, IGetStartingStats getStartingStatsUseCase,
            IPublishMessageAdaptor publishMessageAdaptor, IPackDataGateway packDataGateway)
        {
            throw new System.NotImplementedException();
        }
    }
}