using NUnit.Framework;
using PackBear.UseCases.AddCards;
using PackBearTests.Mocks;

namespace PackBearTests.UseCase
{
    public class AddCardsTests
    {
        [TestCase("Scout","The","Dog")]
        [TestCase("The","Dog","Scout")]
        [TestCase("Dog","Called","Scout")]
        public void EachCardDataIsPassedToAddCard(string data1,string data2,string data3)
        {
          
            AddCardSpy spy = new AddCardSpy();
            new AddCards(spy).Execute(new []{data1,data2,data3});
            Assert.True(spy.CardDataHistory.Contains(data1));
            Assert.True(spy.CardDataHistory.Contains(data2));
            Assert.True(spy.CardDataHistory.Contains(data3));
        }
    }
}