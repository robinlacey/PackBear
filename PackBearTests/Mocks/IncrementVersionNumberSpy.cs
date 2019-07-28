using PackBear.UseCases.IncrementVersionNumber.Interface;

namespace PackBearTests.Mocks
{
    public class IncrementVersionNumberSpy : IIncrementVersionNumber
    {
        private readonly int _returnValue;

        public IncrementVersionNumberSpy(int returnValue)
        {
            _returnValue = returnValue;
        }

        public bool Called { get; private set; }

        public int Execute()
        {
            Called = true;
            return _returnValue;
        }
    }
}