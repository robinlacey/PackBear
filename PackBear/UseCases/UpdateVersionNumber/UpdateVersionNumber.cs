using PackBear.Adaptor.Interface;
using PackBear.Gateway.Interface;
using PackBear.UseCases.UpdateVersionNumber.Interface;

namespace PackBear.UseCases.UpdateVersionNumber
{
    public class UpdateVersionNumber : IUpdateVersionNumber
    {
        private readonly IVersionNumberGateway _versionNumberGateway;
        private readonly IPublishMessageAdaptor _publishMessageAdaptor;

        public UpdateVersionNumber(IVersionNumberGateway versionNumberGateway,
            IPublishMessageAdaptor publishMessageAdaptor)
        {
            _versionNumberGateway = versionNumberGateway;
            _publishMessageAdaptor = publishMessageAdaptor;
        }
        public void Execute(int newVersionNumber)
        {
            throw new System.NotImplementedException();
        }
    }
}