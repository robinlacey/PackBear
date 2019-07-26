using PackBear.UseCases.UpdateVersionNumber.Interface;

namespace PackBearTests.Mocks
{
    public class UpdateVersionNumberSpy:IUpdateVersionNumber
    {
        public int NewVersionNumber { get; private set; }
        public void Execute(int newVersionNumber) => NewVersionNumber = newVersionNumber;
    }
}