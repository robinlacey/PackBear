using PackBear.Gateway.Interface;

namespace PackBearTests.Mocks
{
    public class PackGatewayDummy:IPackGateway
    {
        public string[] GetCards(int packVersion)
        {
            return new string[0];
        }

        public void SetCards(string[] cards, int packVersion)
        {
         
        }
    }
}