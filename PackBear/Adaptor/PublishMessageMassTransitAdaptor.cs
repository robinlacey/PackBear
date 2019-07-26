using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using PackBear.Adaptor.Interface;

namespace PackBear.Adaptor
{
    public class PublishMessageMassTransitAdaptor : IPublishMessageAdaptor
    {
        private readonly IPublishEndpoint _massTransitEndPoint;

        public PublishMessageMassTransitAdaptor(IPublishEndpoint massTransitEndPoint)
        {
            _massTransitEndPoint = massTransitEndPoint;
        }

        public Task Publish<T>(T message, CancellationToken cancellationToken = default(CancellationToken))
            where T : class
        {
            return _massTransitEndPoint.Publish(message, cancellationToken);
        }
    }
}