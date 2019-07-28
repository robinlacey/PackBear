using PackBear.UseCases.IncrementVersionNumber.Interface;

namespace PackBearTests.Mocks
{
    public class IncrementVersionNumberDummy : IIncrementVersionNumber
    {
        public int Execute() => 0;
    }
}