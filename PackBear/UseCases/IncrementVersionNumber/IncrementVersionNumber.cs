using PackBear.Adaptor.Interface;
using PackBear.Gateway.Interface;
using PackBear.Messages.Implemented;
using PackBear.UseCases.IncrementVersionNumber.Interface;

namespace PackBear.UseCases.IncrementVersionNumber
{
    public class IncrementVersionNumber : IIncrementVersionNumber
    {
        private readonly IVersionNumberGateway _versionNumberGateway;
        private readonly IPublishMessageAdaptor _publishMessageAdaptor;

        public IncrementVersionNumber(IVersionNumberGateway versionNumberGateway,
            IPublishMessageAdaptor publishMessageAdaptor)
        {
            _versionNumberGateway = versionNumberGateway;
            _publishMessageAdaptor = publishMessageAdaptor;
        }
        public void Execute()
        {
            _versionNumberGateway.Set(_versionNumberGateway.Get()+1);
            _publishMessageAdaptor.Publish(new RequestPackVersionNumberUpdated {PackNumber = _versionNumberGateway.Get()});
        }
    }
}