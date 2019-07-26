using PackBear.Adaptor.Interface;
using PackBear.Card.Interface;

namespace PackBearTests.Mocks
{
    public class JsonDeserializeAdaptorDummy:IJsonDeserializeAdaptor
    {
        public ICard DeserializeCard(string data) => new CardDummy();
    }
}