using System.Threading.Tasks;
using MassTransit;
using PackBear.Messages;
using PackBear.Player.Interface;

namespace PackBear.Consumers
{
    public class RequestStartingCardConsumer : IConsumer<IRequestStartingCard>
    {
        private readonly IStartingStats _startingStats;

        public RequestStartingCardConsumer(IStartingStats startingStats)
        {
            _startingStats = startingStats;
        }

        public Task Consume(ConsumeContext<IRequestStartingCard> context)
        {
            throw new System.NotImplementedException();
        }
    }
}