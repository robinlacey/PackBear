using PackBear.Gateway.Interface;

namespace PackBearTests.Mocks
{
    public class VersionNumberGatewayDummy:IVersionNumberGateway
    {
        public int Get() => 0;
        public void Set(int value) {}
    }
}