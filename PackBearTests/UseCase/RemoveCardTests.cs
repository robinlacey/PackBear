using NUnit.Framework;
using PackBear.Card.Interface;
using PackBear.Card.Options.Interface;
using PackBear.UseCases.RemoveCard;
using PackBearTests.Mocks;

namespace PackBearTests.UseCase
{
    public class RemoveCardTests
    {
        [TestCase("Scout")]
        [TestCase("The")]
        [TestCase("Dog")]
        public void ThenCardIDIsPassedToCardGatewayHasCard(string id)
        {
            CardGatewaySpy spy = new CardGatewaySpy(new CardDummy(), false);
            new RemoveCard(spy, new IncrementVersionNumberDummy()).Execute(id);
            Assert.True(spy.HasCardCalled);
            Assert.True(spy.HasCardID == id);
        }

        public class CardIsInGateway
        {
            [TestCase(1, "Dog")]
            [TestCase(12, "Cat")]
            [TestCase(4, "Cow")]
            public void ThenCardIsFetchedAndUpdated(int version, string cardID)
            {
                ICard existingCard = new CardStub(cardID, "Title", "Description", "Image", new ICardOption[0], "Heavy");
                CardGatewaySpy spy = new CardGatewaySpy(existingCard, true);
                IncrementVersionNumberSpy stub = new IncrementVersionNumberSpy(version);
                new RemoveCard(spy, stub).Execute(cardID);

                Assert.True(spy.GetCardID == cardID);
                Assert.True(spy.UpdateCardCalled);
            }
            [TestCase(1, "Dog")]
            [TestCase(12, "Cat")]
            [TestCase(4, "Cow")]
            public void ThenCardIsUpdatedWithIncrementedVersion(int newVersion, string cardID)
            {
                ICard existingCard = new CardStub(cardID, "Title", "Description", "Image", new ICardOption[0], "Heavy");
                existingCard.VersionAdded = newVersion - 1;
                CardGatewaySpy spy = new CardGatewaySpy(existingCard, true);
                IncrementVersionNumberSpy incrementVersionNumberSpy = new IncrementVersionNumberSpy(newVersion);
                new RemoveCard(spy, incrementVersionNumberSpy).Execute(cardID);

                Assert.True(spy.GetCardID == cardID);
                Assert.True(spy.UpdateCardCalled);
                Assert.True(incrementVersionNumberSpy.Called);
                Assert.True(spy.UpdateCardCardAdded.VersionRemoved == newVersion);
            }
        }

        public class CardIsNotInGateway
        {
            [Test]
            public void ThenUpdateIsNotCalled()
            {
                ICard existingCard = new CardStub("ID", "Title", "Description", "Image", new ICardOption[0], "Heavy");
                CardGatewaySpy spy = new CardGatewaySpy(existingCard, false);
                new RemoveCard(spy, new IncrementVersionNumberDummy()).Execute("ID");
                Assert.False(spy.UpdateCardCalled);
            }
        }
    }
}