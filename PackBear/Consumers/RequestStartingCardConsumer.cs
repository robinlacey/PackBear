using System.Threading.Tasks;
using MassTransit;
using PackBear.Messages;

namespace PackBear.Consumers
{
    public class RequestStartingCardConsumer:IConsumer<IRequestStartingCard>
    {
        public Task Consume(ConsumeContext<IRequestStartingCard> context)
        {
            throw new System.NotImplementedException();
        }
    }
}