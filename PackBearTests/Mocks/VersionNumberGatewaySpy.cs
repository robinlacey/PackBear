using PackBear.Gateway.Interface;

namespace PackBearTests.Mocks
{
    public class VersionNumberGatewaySpy : IVersionNumberGateway
    {
        public int Current { get; }

        public VersionNumberGatewaySpy(int current)
        {
            Current = current;
        }

        public int Get() => Current;

        public int SetValue { get; private set; }

        public void Set(int value)
        {
            SetValue = value;
        }
    }
}