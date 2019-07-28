using System;
using System.Collections.Generic;
using NUnit.Framework;
using PackBear.UseCases.IsValidCardData.Interface;
using PackBear.UseCases.ValidCardsData;
using PackBearTests.Mocks;

namespace PackBearTests.UseCase
{
    public class ValidCardsDataTests
    {
        [TestCase(3)]
        [TestCase(22)]
        [TestCase(15)]
        public void ThenEachStringIsPassedToValidCardData(int dataCount)
        {
            string[] stringArray = new string[dataCount];
            for (int i = 0; i < dataCount; i++)
            {
                stringArray[i] = Guid.NewGuid().ToString();
            }

            ValidCardDataSpy spy = new ValidCardDataSpy();

            new ValidCardsData(spy).Execute(stringArray);
            foreach (string s in stringArray)
            {
                Assert.True(spy.CardDataHistory.Contains(s));
            }
        }

        [TestCase(3)]
        [TestCase(22)]
        [TestCase(15)]
        public void ThenEachValidationResultIsReturnedInAnArray(int dataCount)
        {
            List<string> data = new List<string>();
            List<IValidationResult> results = new List<IValidationResult>();
            for (int i = 0; i < dataCount; i++)
            {
                data.Add(Guid.NewGuid().ToString());
                results.Add(new ValidationResultStub(false, Guid.NewGuid().ToString(), new CardDummy()));
            }

            ValidCardDataFake fake = new ValidCardDataFake(results.ToArray());

            IValidationResult[] returnValuds = new ValidCardsData(fake).Execute(data.ToArray());
            for (int i = 0; i < returnValuds.Length; i++)
            {
                Assert.True(returnValuds[i].ErrorMessage == results[i].ErrorMessage);
            }
        }
    }
}