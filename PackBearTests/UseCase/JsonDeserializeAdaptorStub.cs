using System;
using PackBear.Adaptor.Interface;
using PackBear.Card.Interface;

namespace PackBearTests.UseCase
{
    public class JsonDeserializeAdaptorStub : IJsonDeserializeAdaptor
    {
        private readonly ICard _returnCard;

        public JsonDeserializeAdaptorStub(ICard returnCard)
        {
            _returnCard = returnCard;
        }

        public ICard DeserializeCard(string data)
        {
            return _returnCard;
        }
    }
}