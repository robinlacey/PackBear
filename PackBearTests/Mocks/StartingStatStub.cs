using PackBear.Player.Interface;

namespace PackBearTests.Mocks
{
    public class StartingStatStub : IStartingStat
    {
        public int Current { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public StartingStatStub(int current, int maximum, int minimum)
        {
            Current = current;
            Maximum = maximum;
            Minimum = minimum;
        }
    }
}