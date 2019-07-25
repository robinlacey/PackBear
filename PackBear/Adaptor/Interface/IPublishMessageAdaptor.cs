using System.Threading;
using System.Threading.Tasks;

namespace PackBear.Adaptor.Interface
{
    public interface IPublishMessageAdaptor
    {
        Task Publish<T>(T message, CancellationToken cancellationToken = default (CancellationToken)) where T : class;
    }
}