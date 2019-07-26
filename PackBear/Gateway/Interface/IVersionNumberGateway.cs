namespace PackBear.Gateway.Interface
{
    public interface IVersionNumberGateway
    {
        int Get();
        void Set(int value);
    }
}