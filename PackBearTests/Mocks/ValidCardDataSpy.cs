using System.Collections.Generic;
using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBearTests.Mocks
{
    public class ValidCardDataSpy : IValidCardData
    {
        public List<string> CardDataHistory = new List<string>();
        public string LastCardData { get; private set; }

        public IValidationResult Execute(string card)
        {
            LastCardData = card;
            CardDataHistory.Add(card);
            return new ValidationResultStub(false, "", null);
        }
    }
}