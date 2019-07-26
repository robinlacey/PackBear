using PackBear.Card.Interface;
using PackBear.Gateway.Interface;
using PackBear.UseCases.GetRandomCard.Interface;

namespace PackBear.UseCases.GetRandomCard
{
    public class GetRandomCard : IGetRandomCard
    {
        private readonly IPackGateway _packGateway;

        public GetRandomCard(IPackGateway packGateway)
        {
            _packGateway = packGateway;
        }
        public ICard Execute(int seed, int turnNumber, int packVersion)
        {
            throw new System.NotImplementedException();
        }
    }
}