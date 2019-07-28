using System;
using System.Linq;
using NUnit.Framework;
using PackBear.Card.Options.Interface;
using PackBear.UseCases.AddCard;
using PackBearTests.Mocks;

namespace PackBearTests.UseCase
{
    public class AddCardTests
    {
        [TestCase("Scout")]
        [TestCase("The")]
        [TestCase("Dog")]
        public void CardDataIsPassedToIsValidCardData(string data)
        {
            ValidCardDataSpy spy = new ValidCardDataSpy();
            new AddCard(spy, new CardGatewayDummy(), new PackGatewayDummy(),  new IncrementVersionNumberDummy()).Execute(data);
            Assert.True(spy.LastCardData == data);
        }

        public class GivenValidCardData
        {
            [TestCase("Scout")]
            [TestCase("I A Dog")]
            public void ThenCardGatewayHasCardIsCalled(string cardID)
            {
                CardStub stub = new CardStub(cardID, " ", " ", " ", new ICardOption[0], " ");
                CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(stub, false);
                new AddCard(new ValidCardDataStub(true, stub), cardGatewaySpy, new PackGatewayDummy(),  new IncrementVersionNumberDummy())
                    .Execute("Valid Json");
                Assert.True(cardGatewaySpy.HasCardCalled);
                Assert.True(cardGatewaySpy.HasCardID == cardID);
            }
            
            public class WhenVersionNumberIsAdded
            {
                [TestCase()]
                public void ThenIncrementVersionNumberIsCalled()
                {
                    IncrementVersionNumberSpy spy = new IncrementVersionNumberSpy(0);
                    CardStub stub = new CardStub("Card", " ", " ", " ", new ICardOption[0], " ");
                    CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(stub, false);
                    new AddCard(new ValidCardDataStub(true, stub), cardGatewaySpy, new PackGatewayDummy(),  spy)
                        .Execute("Valid Json");
                    Assert.True(cardGatewaySpy.HasCardCalled);
                    Assert.True(spy.Called);
                }

                [TestCase(4)]
                [TestCase(112)]
                [TestCase(12414)]
                public void ThenNewVersionNumberIsAddedToAllCardsOnUpdate(int newVersionNumber)
                {
                    IncrementVersionNumberSpy spy = new IncrementVersionNumberSpy(newVersionNumber);
                    CardStub stub = new CardStub("Card", " ", " ", " ", new ICardOption[0], " ");
                    CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(stub, true);
                    new AddCard(new ValidCardDataStub(true, stub), cardGatewaySpy,  new PackGatewayDummy(), spy)
                        .Execute("Valid Json");
                    Assert.True(cardGatewaySpy.UpdateCardCardAdded.VersionAdded == newVersionNumber);
                }

                [TestCase(4)]
                [TestCase(112)]
                [TestCase(12414)]
                public void ThenNewVersionNumberIsAddedToAllCardsOnAdd(int newVersionNumber)
                {
                    IncrementVersionNumberSpy spy = new IncrementVersionNumberSpy(newVersionNumber);
                    CardStub stub = new CardStub("Card", " ", " ", " ", new ICardOption[0], " ");
                    CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(stub, false);
                    new AddCard(new ValidCardDataStub(true, stub), cardGatewaySpy,  new PackGatewayDummy(), spy)
                        .Execute("Valid Json");
                    Assert.True(cardGatewaySpy.CardAdded.VersionAdded == newVersionNumber);
                }
                
                [TestCase("Scout",4,3)]
                [TestCase("The",112,2)]
                [TestCase("Dog",12414,34)]
                public void ThenNewPackIsCreatedWithCardID(string cardID,int newVersionNumber, int cardIsnPack)
                {
                    IncrementVersionNumberSpy spy = new IncrementVersionNumberSpy(newVersionNumber);
                    CardStub stub = new CardStub(cardID, " ", " ", " ", new ICardOption[0], " ");
                    CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(stub, false);
                    PackGatewayFake packGatewayFake = new PackGatewayFake(new string[0]);
                    new AddCard(new ValidCardDataStub(true, stub), cardGatewaySpy,  packGatewayFake, spy)
                        .Execute("Valid Json");
                    Assert.True(packGatewayFake.SetCardsHistory.ContainsKey(newVersionNumber));
                    Assert.True(packGatewayFake.SetCardsHistory[newVersionNumber].Contains(cardID));
                }
            }
            public class WhenCardIDExists
            {
                [TestCase("Scout", 42)]
                [TestCase("I A Dog", 24)]
                public void ThenCardGatewayUpdateIsCalledWithPackVersion(string cardID, int versionNumber)
                {
                    CardStub cardToAdd = new CardStub(cardID, "New Card", "Dog", "Cat", new ICardOption[0], "Cow");
                    CardStub existingCard = new CardStub(cardID, "Old Card", " ", " ", new ICardOption[0], " ");
                    CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(existingCard, true);
                    new AddCard(new ValidCardDataStub(true, cardToAdd), cardGatewaySpy, new PackGatewayDummy(), 
                        new IncrementVersionNumberSpy(versionNumber)).Execute("Valid Json");
                    Assert.True(cardGatewaySpy.UpdateCardCalled);

                    Assert.True(cardGatewaySpy.UpdateCardID == cardID);
                    Assert.True(cardGatewaySpy.UpdateCardCardAdded == cardToAdd);
                    Assert.True(cardGatewaySpy.UpdateCardCardAdded.Title == cardToAdd.Title);
                    Assert.True(cardGatewaySpy.UpdateCardCardAdded.Description == cardToAdd.Description);
                    Assert.True(cardGatewaySpy.UpdateCardCardAdded.CardWeight == cardToAdd.CardWeight);
                    Assert.True(cardGatewaySpy.UpdateCardCardAdded.CardID == cardToAdd.CardID);
                    Assert.True(cardGatewaySpy.UpdateCardCardAdded.ImageURL == cardToAdd.ImageURL);
                    Assert.True(cardGatewaySpy.UpdateCardCardAdded.Options == cardToAdd.Options);
                    Assert.True(cardGatewaySpy.UpdateCardCardAdded.VersionRemoved == null);
                    Assert.True(cardGatewaySpy.UpdateCardCardAdded.VersionAdded == versionNumber);
                }

                [TestCase("Scout",10, 234,0)]
                [TestCase("The",23,12,1)]
                [TestCase("Dog",12,31,4)]
                public void ThenPackGatewaySetIsCalledToUpdateBothPacks(string cardID, int numberOfCardsInOldPack, int newPackVersion, int oldPackVersion)
                {
                    string[] cardsInOldPack = new string[numberOfCardsInOldPack];
                    cardsInOldPack[0] = cardID;
                    for (int i = 1; i < numberOfCardsInOldPack; i++)
                    {
                        cardsInOldPack[i] = Guid.NewGuid().ToString();
                    }
                    
                    CardStub cardToAdd = new CardStub(cardID, "New Card", "Dog", "Cat", new ICardOption[0], "Cow");
                    
                    CardStub existingCard = new CardStub(cardID, "Old Card", " ", " ", new ICardOption[0], " ");
                    existingCard.VersionAdded = oldPackVersion;
                    
                    CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(existingCard, true);
                    PackGatewayFake packGatewayFake = new PackGatewayFake(cardsInOldPack);
                    
                    new AddCard(new ValidCardDataStub(true, cardToAdd), cardGatewaySpy,packGatewayFake, 
                        new IncrementVersionNumberSpy(newPackVersion)).Execute("Valid Json");
                
                    Console.WriteLine(packGatewayFake.GetCardsPackVersion + " " +oldPackVersion);
                    Assert.True(packGatewayFake.GetCardsPackVersion == oldPackVersion);
                    Assert.True(packGatewayFake.SetCardsHistory.ContainsKey(oldPackVersion));
                    Assert.False(packGatewayFake.SetCardsHistory[oldPackVersion].Contains(cardID));
                    AssertCardIDIsAddedToNewPack(cardID, newPackVersion, packGatewayFake);
                }

                private static void AssertCardIDIsAddedToNewPack(string cardID, int newPackVersion, PackGatewayFake packGatewayFake)
                {
                    Assert.True(packGatewayFake.SetCardsHistory.ContainsKey(newPackVersion));
                    Assert.True(packGatewayFake.SetCardsHistory[newPackVersion].Contains(cardID));
                }

                private static void AssertCardIDIsRemovedFromOldPack(string cardID, int oldPackVersion, PackGatewayFake packGatewayFake)
                {
                    Assert.True(packGatewayFake.GetCardsPackVersion == oldPackVersion);
                    Assert.True(packGatewayFake.SetCardsHistory.ContainsKey(oldPackVersion));
                    Assert.False(packGatewayFake.SetCardsHistory[oldPackVersion].Contains(cardID));
                }
            }

            public class WhenCardIDIsNew
            {
                [TestCase("Scout", 42)]
                [TestCase("I A Dog", 24)]
                public void ThenCardGatewayAddIsCalledWithPackVersion(string cardID, int versionNumber)
                {
                    CardStub cardToAdd = new CardStub(cardID, "New Card", "Dog", "Cat", new ICardOption[0], "Cow");
                    CardStub existingCard = new CardStub(cardID + "Now Invalid", "Old Card", " ", " ",
                        new ICardOption[0], " ");
                    CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(existingCard, false);
                    new AddCard(new ValidCardDataStub(true, cardToAdd), cardGatewaySpy, new PackGatewayDummy(), 
                        new IncrementVersionNumberSpy(versionNumber)).Execute("Valid Json");
                    Assert.True(cardGatewaySpy.AddCardCalled);

                    Assert.True(cardGatewaySpy.CardAdded.Title == cardToAdd.Title);
                    Assert.True(cardGatewaySpy.CardAdded.Description == cardToAdd.Description);
                    Assert.True(cardGatewaySpy.CardAdded.CardWeight == cardToAdd.CardWeight);
                    Assert.True(cardGatewaySpy.CardAdded.CardID == cardToAdd.CardID);
                    Assert.True(cardGatewaySpy.CardAdded.ImageURL == cardToAdd.ImageURL);
                    Assert.True(cardGatewaySpy.CardAdded.Options == cardToAdd.Options);
                    Assert.True(cardGatewaySpy.CardAdded.VersionAdded == versionNumber);
                    Assert.True(cardGatewaySpy.CardAdded.VersionRemoved == null);
                    Assert.True(cardGatewaySpy.CardAdded == cardToAdd);
                }
            }
        }

        public class GivenInvalidCardData
        {
            [Test]
            public void ThenNothingElseIsCalled()
            {
                CardStub cardToAdd = new CardStub("CardID", "New Card", "Dog", "Cat", new ICardOption[0], "Cow");
                CardStub existingCard = new CardStub("Now Invalid", "Old Card", " ", " ", new ICardOption[0], " ");
                CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(existingCard, false);

                new AddCard(new ValidCardDataStub(false, cardToAdd), cardGatewaySpy,  new PackGatewayDummy(), new IncrementVersionNumberDummy())
                    .Execute("Valid Json");
                Assert.False(cardGatewaySpy.AddCardCalled);
                Assert.False(cardGatewaySpy.HasCardCalled);
                Assert.False(cardGatewaySpy.UpdateCardCalled);
            }
        }
    }
}