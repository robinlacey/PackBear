using PackBear.Gateway.Interface;

namespace PackBear.Gateway
{
    public class InMemoryPackGateway : IPackGateway
    {
        public string[] GetCards(int packVersion)
        {
            throw new System.NotImplementedException();
        }

        public void SetCards(string[] cards, int packVersion)
        {
            throw new System.NotImplementedException();
        }
    }
}