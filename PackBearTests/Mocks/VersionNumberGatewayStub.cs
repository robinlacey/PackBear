using PackBear.Gateway.Interface;

namespace PackBearTests.Mocks
{
    public class VersionNumberGatewayStub : IVersionNumberGateway
    {
        public int Value { get; }

        public VersionNumberGatewayStub(int value)
        {
            Value = value;
        }

        public int Get() => Value;

        public void Set(int value)
        {
        }
    }
}