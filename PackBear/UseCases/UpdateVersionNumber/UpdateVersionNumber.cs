using PackBear.Gateway.Interface;
using PackBear.UseCases.UpdateVersionNumber.Interface;

namespace PackBear.UseCases.UpdateVersionNumber
{
    public class UpdateVersionNumber : IUpdateVersionNumber
    {
        public void Execute(int newVersionNumber, IVersionNumberGateway versionNumberGateway)
        {
            throw new System.NotImplementedException();
        }
    }
}