using PackBear.Gateway.Interface;
using PackBear.UseCases.UpdateVersionNumber.Interface;

namespace PackBearTests.Mocks
{
    public class UpdateVersionNumberDummy:IUpdateVersionNumber
    {
        public void Execute(int newVersionNumber)
        { 
        }
    }
}