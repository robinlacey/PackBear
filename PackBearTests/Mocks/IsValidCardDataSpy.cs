using System.Collections.Generic;
using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBearTests.Mocks
{
    public class IsValidCardDataSpy:IIsValidCardData
    {
        public List<string> CardDataHistory = new List<string>();
        public string LastCardData { get; private set; }
        public bool Execute(string card)
        {
            LastCardData = card;
            CardDataHistory.Add(card);
            return false;
        }
    }
}