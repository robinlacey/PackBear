using Newtonsoft.Json;
using PackBear.Adaptor.Interface;
using PackBear.Card.Interface;

namespace PackBear.Adaptor
{
    public class JsonConvertDeserializeAdaptor : IJsonDeserializeAdaptor
    {
        public ICard DeserializeCard(string data)
        {
            return JsonConvert.DeserializeObject<ICard>(data);
        }
    }
}