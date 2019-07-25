using DealerBear.Card.Interface;
using PackBear.Gateway.Interface;
using PackBear.UseCases.GetRandomCard.Interface;

namespace PackBear.UseCases.GetRandomCard
{
    public class GetRandomCard:IGetRandomCard
    {
        public ICard Execute(int seed, int packVersion, IPackDataGateway packDataGateway)
        {
            throw new System.NotImplementedException();
        }
    }
}