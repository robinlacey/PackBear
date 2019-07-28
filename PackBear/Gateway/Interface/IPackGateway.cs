namespace PackBear.Gateway.Interface
{
    public interface IPackGateway
    {
        string[] GetCards(int packVersion);
        void SetCards(string[] cards, int packVersion);
    }
}