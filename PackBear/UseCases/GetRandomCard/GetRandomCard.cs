using PackBear.Card.Interface;
using PackBear.Gateway.Interface;
using PackBear.UseCases.GetRandomCard.Interface;

namespace PackBear.UseCases.GetRandomCard
{
    public class GetRandomCard : IGetRandomCard
    {
        public ICard Execute(int seed, int turnNumber, int packVersion, IPackGateway packGateway)
        {
            throw new System.NotImplementedException();
        }
    }
}