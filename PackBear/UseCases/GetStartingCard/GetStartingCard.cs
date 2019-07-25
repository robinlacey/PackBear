using PackBear.Adaptor.Interface;
using PackBear.Gateway.Interface;
using PackBear.Player.Interface;
using PackBear.UseCases.GetRandomCard.Interface;
using Tests.UseCase.GetStartingCard.Interface;

namespace PackBear.UseCases.GetStartingCard
{
    public class GetStartingCard:IGetStartingCard
    {
        public void Execute(IGetRandomCard getRandomCardUseCase, IStartingStats startingStats,
            IPublishMessageAdaptor publishMessageAdaptor, IPackDataGateway packDataGateway)
        {
            throw new System.NotImplementedException();
        }
    }
}