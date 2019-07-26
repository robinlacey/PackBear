using PackBear.Gateway.Interface;

namespace PackBear.UseCases.UpdateVersionNumber.Interface
{
    public interface IUpdateVersionNumber
    {
        void Execute(int newVersionNumber, IVersionNumberGateway versionNumberGateway);
    }
}