using PackBear.Card.Interface;

namespace PackBear.Adaptor.Interface
{
    public interface IJsonDeserializeAdaptor
    {
        ICard DeserializeCard(string data);
    }
}