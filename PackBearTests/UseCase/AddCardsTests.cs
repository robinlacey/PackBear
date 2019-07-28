using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PackBear.Card.Interface;
using PackBear.Card.Options.Interface;
using PackBear.Exceptions;
using PackBear.Messages;
using PackBear.UseCases.AddCards;
using PackBear.UseCases.IsValidCardData;
using PackBear.UseCases.IsValidCardData.Interface;
using PackBear.UseCases.ValidCardsData.Interface;
using PackBearTests.Mocks;

namespace PackBearTests.UseCase
{
    public class AddCardsTests
    {
        public class GivenNoInvalidCards
        {
            public class WhenCardsDoNotExistInGateway
            {
                [TestCase(55)]
                [TestCase(1)]
                [TestCase(4)]
                public void ThenAddsEachCardDataToGateway(int cardDataCount)
                {
                    List<string> cardDatas = new List<string>();
                    List<ICard> cardsToReturn = new List<ICard>();
                    List<IValidationResult> results = new List<IValidationResult>();
                    for (int i = 0; i < cardDataCount; i++)
                    {
                        cardDatas.Add(Guid.NewGuid().ToString());
                        cardsToReturn.Add(new CardStub(Guid.NewGuid().ToString(), "Title", "Description", "Image",
                            new ICardOption[0], "Heavy"));
                        results.Add(new ValidationResult {Valid = true, ValidCardData = cardsToReturn[i]});
                    }


                    IValidCardsData stub = new ValidCardsDataStub(results.ToArray());
                    // Card is not in gateway
                    CardGatewaySpy spy = new CardGatewaySpy(new CardDummy(), false);

                    new AddCards(stub, spy, new IncrementVersionNumberDummy(), new PublishEndPointSpy()).Execute(
                        new string[results.Count]);

                    foreach (ICard card in spy.AddCardHistory)
                    {
                        Assert.True(cardsToReturn.Contains(card));
                    }

                    Assert.True(spy.HasCardCalled);
                    Assert.True(spy.AddCardCalled);
                    Assert.False(spy.UpdateCardCalled);
                }
            }

            public class WhenCardsDoExistInGateway
            {
                [TestCase(55)]
                [TestCase(1)]
                [TestCase(4)]
                public void ThenUpdatesEachCardDataToGateway(int cardDataCount)
                {
                    List<string> cardDatas = new List<string>();
                    List<ICard> cardsToReturn = new List<ICard>();
                    List<IValidationResult> results = new List<IValidationResult>();
                    for (int i = 0; i < cardDataCount; i++)
                    {
                        cardDatas.Add(Guid.NewGuid().ToString());
                        cardsToReturn.Add(new CardStub(Guid.NewGuid().ToString(), "Title", "Description", "Image",
                            new ICardOption[0], "Heavy"));
                        results.Add(new ValidationResult {Valid = true, ValidCardData = cardsToReturn[i]});
                    }

                    IValidCardsData stub = new ValidCardsDataStub(results.ToArray());

                    CardGatewaySpy spy = new CardGatewaySpy(new CardDummy(), true);
                    new AddCards(stub, spy, new IncrementVersionNumberDummy(), new PublishEndPointSpy()).Execute(
                        new string[results.Count]);

                    Assert.True(spy.UpdateCardCalled);
                    foreach (ICard card in spy.UpdateCardsHistory)
                    {
                        Assert.True(cardsToReturn.Contains(card));
                    }

                    Assert.True(spy.HasCardCalled);
                    Assert.False(spy.AddCardCalled);
                }
            }


            public class WhenVersionNumberIsAdded
            {
                [TestCase()]
                public void ThenIncrementVersionNumberIsCalled()
                {
                    List<string> cardDatas = new List<string>();
                    List<ICard> cardsToReturn = new List<ICard>();
                    List<IValidationResult> results = new List<IValidationResult>();
                    for (int i = 0; i < 10; i++)
                    {
                        cardDatas.Add(Guid.NewGuid().ToString());
                        cardsToReturn.Add(new CardStub(Guid.NewGuid().ToString(), "Title", "Description", "Image",
                            new ICardOption[0], "Heavy"));
                        results.Add(new ValidationResult {Valid = true, ValidCardData = cardsToReturn[i]});
                    }

                    IncrementVersionNumberSpy spy = new IncrementVersionNumberSpy(0);
                    new AddCards(new ValidCardsDataStub(results.ToArray()), new CardGatewayDummy(), spy,
                        new PublishEndPointSpy()).Execute(new string[results.Count]);
                    Assert.True(spy.Called);
                }

                [TestCase(4)]
                [TestCase(112)]
                [TestCase(12414)]
                public void ThenNewVersionNumberIsAddedToAllCardsOnUpdate(int newVersionNumber)
                {
                    List<string> cardDatas = new List<string>();
                    List<ICard> cardsToReturn = new List<ICard>();
                    List<IValidationResult> results = new List<IValidationResult>();
                    for (int i = 0; i < 10; i++)
                    {
                        cardDatas.Add(Guid.NewGuid().ToString());
                        cardsToReturn.Add(new CardStub(Guid.NewGuid().ToString(), "Title", "Description", "Image",
                            new ICardOption[0], "Heavy"));
                        results.Add(new ValidationResult {Valid = true, ValidCardData = cardsToReturn[i]});
                    }

                    IncrementVersionNumberSpy spy = new IncrementVersionNumberSpy(newVersionNumber);
                    CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(new CardDummy(), true);
                    new AddCards(new ValidCardsDataStub(results.ToArray()), new CardGatewayDummy(), spy,
                        new PublishEndPointSpy()).Execute(new string[results.Count]);
                    foreach (ICard card in cardGatewaySpy.UpdateCardsHistory)
                    {
                        Assert.True(card.VersionAdded == newVersionNumber);
                    }

                    Assert.True(spy.Called);
                }

                [TestCase(4)]
                [TestCase(112)]
                [TestCase(12414)]
                public void ThenNewVersionNumberIsAddedToAllCardsOnAddd(int newVersionNumber)
                {
                    List<string> cardDatas = new List<string>();
                    List<ICard> cardsToReturn = new List<ICard>();
                    List<IValidationResult> results = new List<IValidationResult>();
                    for (int i = 0; i < 10; i++)
                    {
                        cardDatas.Add(Guid.NewGuid().ToString());
                        cardsToReturn.Add(new CardStub(Guid.NewGuid().ToString(), "Title", "Description", "Image",
                            new ICardOption[0], "Heavy"));
                        results.Add(new ValidationResult {Valid = true, ValidCardData = cardsToReturn[i]});
                    }

                    IncrementVersionNumberSpy spy = new IncrementVersionNumberSpy(newVersionNumber);
                    CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(new CardDummy(), false);
                    new AddCards(new ValidCardsDataStub(results.ToArray()), new CardGatewayDummy(), spy,
                        new PublishEndPointSpy()).Execute(new string[results.Count]);
                    foreach (ICard card in cardGatewaySpy.AddCardHistory)
                    {
                        Assert.True(card.VersionAdded == newVersionNumber);
                    }

                    Assert.True(spy.Called);
                }
            }
        }


        public class GivenInvalidCards
        {
            public class WhenValidationResultCountDoesNotEqualDataStringAccount
            {
                [Test]
                public void ThenThrowInvalidValidationResultCountException()
                {
                    List<string> cardDatas = new List<string>();
                    for (int i = 0; i < 4; i++)
                    {
                        cardDatas.Add(Guid.NewGuid().ToString());
                    }

                    List<IValidationResult> results = new List<IValidationResult>();
                    for (int i = 0; i < 2; i++)
                    {
                        results.Add(new ValidationResult {Valid = true});
                    }

                    IValidCardsData stub = new ValidCardsDataStub(results.ToArray());
                    Assert.Throws<InvalidValidationResultCountException>(() =>
                        new AddCards(stub, new CardGatewayDummy(), new IncrementVersionNumberDummy(),
                            new PublishEndPointSpy()).Execute(cardDatas.ToArray()));
                }
            }

            [TestCase(4, 1)]
            [TestCase(13, 12)]
            [TestCase(0, 1)]
            public void ThenPublishesMessageWithFailDetails(int validCards, int invalidCards)
            {
                List<IValidationResult> results = new List<IValidationResult>();
                for (int i = 0; i < validCards; i++)
                {
                    results.Add(new ValidationResult() {ErrorMessage = Guid.NewGuid().ToString(), Valid = true});
                }

                for (int i = 0; i < invalidCards; i++)
                {
                    results.Add(new ValidationResult() {ErrorMessage = Guid.NewGuid().ToString(), Valid = false});
                }

                IValidCardsData stub = new ValidCardsDataStub(results.OrderBy(a => Guid.NewGuid()).ToArray());
                PublishEndPointSpy spy = new PublishEndPointSpy();
                new AddCards(stub, new CardGatewayDummy(), new IncrementVersionNumberDummy(), spy).Execute(
                    new string[results.Count]);
                Assert.True(spy.MessageObject is IFailedToAddCards);
                IFailedToAddCards failedToAddCardsMessage = (IFailedToAddCards) spy.MessageObject;

                foreach (IValidationResult validationResult in results.Where(_ => !_.Valid))
                {
                    Assert.True(failedToAddCardsMessage.ErrorMessages.Any(_ =>
                        _.ToLower().Trim().Contains(validationResult.ErrorMessage.ToLower().Trim())));
                }
            }

            [TestCase(4, 1)]
            [TestCase(13, 12)]
            [TestCase(0, 1)]
            public void ThenGatewayIsNotCalled(int validCards, int invalidCards)
            {
                List<IValidationResult> results = new List<IValidationResult>();
                for (int i = 0; i < validCards; i++)
                {
                    results.Add(new ValidationResult() {ErrorMessage = Guid.NewGuid().ToString(), Valid = true});
                }

                for (int i = 0; i < invalidCards; i++)
                {
                    results.Add(new ValidationResult() {ErrorMessage = Guid.NewGuid().ToString(), Valid = false});
                }

                // Shuffle list using linq
                IValidCardsData stub = new ValidCardsDataStub(results.OrderBy(a => Guid.NewGuid()).ToArray());
                CardGatewaySpy spy = new CardGatewaySpy(new CardDummy(), true);
                new AddCards(stub, spy, new IncrementVersionNumberDummy(), new PublishEndPointSpy()).Execute(
                    new string[results.Count]);
                Assert.False(spy.AddCardCalled);
                Assert.False(spy.HasCardCalled);
                Assert.False(spy.UpdateCardCalled);
            }

            [TestCase(4, 1)]
            [TestCase(13, 12)]
            [TestCase(0, 1)]
            public void ThenVersionNumberIncrementIsNotCalled(int validCards, int invalidCards)
            {
                List<IValidationResult> results = new List<IValidationResult>();
                for (int i = 0; i < validCards; i++)
                {
                    results.Add(new ValidationResult() {ErrorMessage = Guid.NewGuid().ToString(), Valid = true});
                }

                for (int i = 0; i < invalidCards; i++)
                {
                    results.Add(new ValidationResult() {ErrorMessage = Guid.NewGuid().ToString(), Valid = false});
                }

                // Shuffle list using linq
                IValidCardsData stub = new ValidCardsDataStub(results.OrderBy(a => Guid.NewGuid()).ToArray());
                CardGatewaySpy spy = new CardGatewaySpy(new CardDummy(), true);
                IncrementVersionNumberSpy versionNumberSpy = new IncrementVersionNumberSpy(22);
                new AddCards(stub, spy, versionNumberSpy, new PublishEndPointSpy()).Execute(new string[results.Count]);
                Assert.False(versionNumberSpy.Called);
            }
        }
    }
}