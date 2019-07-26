using PackBear.UseCases.IncrementVersionNumber.Interface;

namespace PackBearTests.Mocks
{
    public class IncrementVersionNumberSpy:IIncrementVersionNumber
    {
        public bool Called { get; private set; }
        public void Execute() => Called = true;
    }
}