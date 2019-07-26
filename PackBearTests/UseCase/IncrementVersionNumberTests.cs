using NUnit.Framework;
using PackBear.Messages;
using PackBear.UseCases.IncrementVersionNumber;
using PackBearTests.Mocks;

namespace PackBearTests.UseCase
{
    public class IncrementVersionNumberTests
    {
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(5)]
        public void ThenVersionNumberGatewayIsPassedIncrementalValue(int currentVersionNumber)
        {
            VersionNumberGatewaySpy spy = new VersionNumberGatewaySpy(currentVersionNumber);
            new IncrementVersionNumber(spy, new PublishEndPointSpy() ).Execute();
            Assert.True(spy.SetValue == currentVersionNumber+1);
        }
        
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(5)]
        public void ThenVersionNumberIsPublishedAsIRequestPackVersionNumberUpdated(int currentVersionNumber)
        {
            VersionNumberGatewaySpy spy = new VersionNumberGatewaySpy(currentVersionNumber);
            PublishEndPointSpy publishSpy = new PublishEndPointSpy();
            new IncrementVersionNumber(spy, publishSpy ).Execute();
            Assert.True(publishSpy.MessageObject is IRequestPackVersionNumberUpdated);
            IRequestPackVersionNumberUpdated requestPackVersionNumberUpdated =
                (IRequestPackVersionNumberUpdated) publishSpy.MessageObject;
            Assert.True(requestPackVersionNumberUpdated.PackNumber == currentVersionNumber);
        }
    }
}