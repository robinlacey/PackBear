using System.Collections.Generic;
using PackBear.UseCases.AddCard.Interface;

namespace PackBearTests.Mocks
{
    public class AddCardSpy : IAddCard
    {
        public List<string> CardDataHistory { get; private set; } = new List<string>();

        public bool ExecuteCalled { get; private set; }
        public string Data { get; private set; }

        public void Execute(string json)
        {
            ExecuteCalled = true;
            Data = json;
            CardDataHistory.Add(json);
        }
    }
}