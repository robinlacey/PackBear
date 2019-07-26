using NUnit.Framework;

namespace PackBearTests.UseCase
{
    public class StartingStatsTests
    {
        public class InvalidData
        {
            public class GivenInvalidCardWeights
            {
                public class WhenValuesAreOutOfBounds
                {
                    [Test]
                    public void ThenThrowInvalidCardWeightException()
                    {
                        Assert.Fail();
                    }
                }
            }
        }
    }
}