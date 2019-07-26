using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBearTests.Mocks
{
    public class IsValidCardDataSpy:IIsValidCardData
    {
        public string CardData { get; private set; }
        public bool Execute(string card)
        {
            CardData = card;
            return false;
        }
    }
}