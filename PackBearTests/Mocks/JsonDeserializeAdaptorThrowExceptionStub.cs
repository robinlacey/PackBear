using System;
using PackBear.Adaptor.Interface;
using PackBear.Card.Interface;

namespace PackBearTests.Mocks
{
    public class JsonDeserializeAdaptorThrowExceptionStub : IJsonDeserializeAdaptor
    {
        private readonly Exception _exception;

        public JsonDeserializeAdaptorThrowExceptionStub(Exception exception)
        {
            _exception = exception;
        }

        public ICard DeserializeCard(string data)
        {
            throw _exception;
        }
    }
}