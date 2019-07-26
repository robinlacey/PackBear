using NUnit.Framework;
using PackBear.Card.Interface;
using PackBear.Card.Options.Interface;
using PackBear.UseCases.AddCard;
using PackBear.UseCases.IsValidCardData.Interface;
using PackBear.UseCases.UpdateVersionNumber;
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
            IsValidCardDataSpy spy = new IsValidCardDataSpy();
            new AddCard(spy, new UpdateVersionNumberDummy(),new VersionNumberGatewayDummy(),new CardGatewayDummy(), new JsonDeserializeAdaptorDummy()).Execute(data);
            Assert.True(spy.CardData == data);
        }
        public class GivenValidCardData
        {
     
            [TestCase(1)]
            [TestCase(44)]
            [TestCase(52)]
            
            public void ThenUpdateVersionNumberIsIncrementedByOne(int startingVersionNumber)
            {
                VersionNumberGatewayStub gatewayStub = new VersionNumberGatewayStub(startingVersionNumber);
                UpdateVersionNumberSpy spy = new UpdateVersionNumberSpy();
                new AddCard(new IsValidCardDataStub(true),spy,gatewayStub,new CardGatewayDummy(), new JsonDeserializeAdaptorDummy()).Execute("Valid Json");
                Assert.True(spy.NewVersionNumber == startingVersionNumber+1);
            }
            
            [TestCase("Scout")]
            [TestCase("I A Dog")]
            public void ThenCardGatewayHasCardIsCalled(string cardID)
            {
                CardStub stub = new CardStub(cardID," "," "," ",new ICardOption[0]," " );
                CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(stub, false);
                new AddCard(new IsValidCardDataStub(true),new UpdateVersionNumberDummy(), new VersionNumberGatewayStub(0),cardGatewaySpy, new JsonDeserializeAdaptorStub(stub)).Execute("Valid Json");
                Assert.True(cardGatewaySpy.HasCardCalled); 
                Assert.True(cardGatewaySpy.HasCardID == cardID); 
            }

            public class WhenCardIDExists
            {
                [TestCase("Scout")]
                [TestCase("I A Dog")]
                public void ThenCardGatewayUpdateIsCalled(string cardID)
                {
                    CardStub cardToAdd = new CardStub(cardID,"New Card","Dog","Cat",new ICardOption[0],"Cow" );
                    CardStub existingCard = new CardStub(cardID,"Old Card"," "," ",new ICardOption[0]," " );
                    CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(existingCard, true);
                    new AddCard(new IsValidCardDataStub(true),new UpdateVersionNumberDummy(), new VersionNumberGatewayStub(0),cardGatewaySpy, new JsonDeserializeAdaptorStub(cardToAdd)).Execute("Valid Json");
                    Assert.True(cardGatewaySpy.UpdateCardCalled); 
                    Assert.True(cardGatewaySpy.UpdateCardID == cardID); 
                    Assert.True(cardGatewaySpy.UpdateCardCardAdded == cardToAdd); 
                }
            }

            public class WhenCardIDIsNew
            {
                [TestCase("Scout")]
                [TestCase("I A Dog")]
                public void ThenCardGatewayAdd(string cardID)
                {
                    CardStub cardToAdd = new CardStub(cardID,"New Card","Dog","Cat",new ICardOption[0],"Cow" );
                    CardStub existingCard = new CardStub(cardID +"Now Invalid","Old Card"," "," ",new ICardOption[0]," " );
                    CardGatewaySpy cardGatewaySpy = new CardGatewaySpy(existingCard, false);
                    new AddCard(new IsValidCardDataStub(true),new UpdateVersionNumberDummy(), new VersionNumberGatewayStub(0),cardGatewaySpy, new JsonDeserializeAdaptorStub(cardToAdd)).Execute("Valid Json");
                    Assert.True(cardGatewaySpy.AddCardCalled); 
                    Assert.True(cardGatewaySpy.CardAdded == cardToAdd); 
                }
            }
           

        }

        public class GivenInvalidCardData
        {
            IIsValidCardData _isValidCardDataStub = new IsValidCardDataStub(false);
        }
    }
}