using System;
using System.Collections.Generic;
using NUnit.Framework;
using PackBear.Adaptor.Interface;
using PackBear.Card.Interface;
using PackBear.Card.Options.Interface;
using PackBear.Player;
using PackBear.Player.Interface;
using PackBear.UseCases.IsValidCardData;
using PackBear.UseCases.IsValidCardData.Interface;
using PackBearTests.Mocks;

namespace PackBearTests.UseCase
{
    public class IsValidCardDataTests
    {
        public class GivenInvalidJsonData
        {
            public class WhenJsonParserThrowsAnException
            {
                [Test]
                public void ThenCatchesJsonSerializationExceptionAndReturnFalse()
                {
                    IJsonDeserializeAdaptor adaptorStub = new JsonDeserializeAdaptorThrowExceptionStub(new Exception());
                    IIsValidCardData isValidCardData = new IsValidCardData(adaptorStub, new StartingStatsDummy());
                    Assert.False(isValidCardData.Execute("Not Json"));
                }
            }
        }

        public class GivenInvalidCardData
        {
            public class WhenStatKeysDoNotMatchStartingStats
            {
                [TestCase("Scout", "The", "Dog")]
                [TestCase("Is", "A", "Good")]
                [TestCase("Dog", "Hello", "Robin")]
                public void ThenReturnsFalse(string statOne, string statTwo, string statThree)
                {
                    Dictionary<string, IStartingStat> stats = new Dictionary<string, IStartingStat>
                    {
                        {statOne, new StartingStatStub(5, 10, 0)},
                        {statTwo, new StartingStatStub(5, 10, 0)},
                        {statThree, new StartingStatStub(5, 10, 0)}
                    };

                    Dictionary<string, float> weights = new Dictionary<string, float>()
                    {
                        {"Common", 0.75f},
                        {"Rare", 0.5f},
                        {"Epic", 0.1f}
                    };

                    ICardOption cardOptionOne = new CardOptionsStub(
                        "Option One",
                        "Option One Description",
                        new PlayerStatsToChangeStub(new Dictionary<string, int>
                        {
                            {"Money", -2},
                            {"Power", 2},
                            {"Health", -2}
                        }),
                        new string[0]);

                    ICardOption cardOptionTwo = new CardOptionsStub(
                        "Option Two",
                        "Option Two Description",
                        new PlayerStatsToChangeStub(new Dictionary<string, int>
                        {
                            {"Money", 2},
                            {"Power", 0 - 2},
                            {"Health", 2}
                        }),
                        new string[0]);

                    IStartingStats startingStats = new StartingStatsStub(stats, weights, 2);
                    ICard card = new CardStub("Card ID", "Card Title", "Card Description",
                        "http://google.com/hello.png", new[] {cardOptionOne, cardOptionTwo}, "Common");

                    IJsonDeserializeAdaptor adaptorStub = new JsonDeserializeAdaptorStub(card);
                    IIsValidCardData isValidCardData = new IsValidCardData(adaptorStub, startingStats);
                    Assert.False(isValidCardData.Execute("This is real json" ));
                }
            }

            public class WhenOptionsCountDoNotMatchStartingStats
            {
                [Test]
                public void ThenReturnsFalse()
                {
                    Dictionary<string, IStartingStat> stats = new Dictionary<string, IStartingStat>
                    {
                        {"Money", new StartingStatStub(5, 10, 0)},
                        {"Power", new StartingStatStub(5, 10, 0)},
                        {"Health", new StartingStatStub(5, 10, 0)}
                    };
                    Dictionary<string, float> weights = new Dictionary<string, float>()
                    {
                        {"Common", 0.75f},
                        {"Rare", 0.5f},
                        {"Epic", 0.1f}
                    };

                    ICardOption cardOptionOne = new CardOptionsStub(
                        "Option One",
                        "Option One Description",
                        new PlayerStatsToChangeStub(new Dictionary<string, int>
                        {
                            {"Money", -2},
                            {"Power", 2},
                            {"Health", -2}
                        }),
                        new string[0]);


                    IStartingStats startingStats = new StartingStatsStub(stats, weights, 2);
                    ICard card = new CardStub("Card ID", "Card Title", "Card Description",
                        "http://google.com/hello.png", new[] {cardOptionOne}, "Common");
                    IJsonDeserializeAdaptor adaptorStub = new JsonDeserializeAdaptorStub(card);
                    IIsValidCardData isValidCardData = new IsValidCardData(adaptorStub, startingStats);
                    Assert.False(isValidCardData.Execute("This is real json" ));
                }
            }

            public class WhenWeightKeysDoNotMatchStartingStats
            {
                [TestCase("Scout", "The", "Dog")]
                [TestCase("Hello", "Cat", "Cow")]
                [TestCase("Moo", "Cow", "Pig")]
                public void ThenReturnsFalse(string weight1, string weight2, string weight3)
                {
                    Dictionary<string, IStartingStat> stats = new Dictionary<string, IStartingStat>
                    {
                        {"Money", new StartingStatStub(5, 10, 0)},
                        {"Health", new StartingStatStub(5, 10, 0)},
                        {"Power", new StartingStatStub(5, 10, 0)}
                    };

                    Dictionary<string, float> weights = new Dictionary<string, float>()
                    {
                        {weight1, 0.75f},
                        {weight2, 0.5f},
                        {weight3, 0.1f}
                    };

                    ICardOption cardOptionOne = new CardOptionsStub(
                        "Option One",
                        "Option One Description",
                        new PlayerStatsToChangeStub(new Dictionary<string, int>
                        {
                            {"Money", -2},
                            {"Power", 2},
                            {"Health", -2}
                        }),
                        new string[0]);

                    ICardOption cardOptionTwo = new CardOptionsStub(
                        "Option Two",
                        "Option Two Description",
                        new PlayerStatsToChangeStub(new Dictionary<string, int>
                        {
                            {"Money", 2},
                            {"Power", 0 - 2},
                            {"Health", 2}
                        }),
                        new string[0]);

                    IStartingStats startingStats = new StartingStatsStub(stats, weights, 2);
                    ICard card = new CardStub("Card ID", "Card Title", "Card Description",
                        "http://google.com/hello.png", new[] {cardOptionOne, cardOptionTwo}, "Common");

                    IJsonDeserializeAdaptor adaptorStub = new JsonDeserializeAdaptorStub(card);
                    IIsValidCardData isValidCardData = new IsValidCardData(adaptorStub, startingStats);
                    Assert.False(isValidCardData.Execute("This is real json"));
                }
            }
        }

        public class GivenValidJsonData
        {
            public class WhenMatchesStartingStats
            {
                [TestCase(4, 3)]
                [TestCase(2, 5)]
                [TestCase(1, 4)]
                public void ThenReturnsTrue(int optionsCount, int valueCount)
                {
                    string[] values = GetRanomValues(valueCount);
                    ICardOption[] cardOptions = CreateCardOptions(optionsCount, values);
                    Dictionary<string, IStartingStat> stats = GetStatsWithValues(values);
                    Dictionary<string, float> weights = new Dictionary<string, float>()
                    {
                        {"Common", 0.75f},
                        {"Rare", 0.5f},
                        {"Epic", 0.1f}
                    };

                    IStartingStats startingStats = new StartingStatsStub(stats, weights, optionsCount);
                    ICard card = new CardStub("Card ID", "Card Title", "Card Description",
                        "http://google.com/hello.png", cardOptions, "Common");
                    IJsonDeserializeAdaptor adaptorStub = new JsonDeserializeAdaptorStub(card);
                    IIsValidCardData isValidCardData = new IsValidCardData(adaptorStub, startingStats);
                    Assert.True(isValidCardData.Execute("This is real json"));
                }

                Dictionary<string, IStartingStat> GetStatsWithValues(string[] values)
                {
                    Dictionary<string, IStartingStat> returnDictionary = new Dictionary<string, IStartingStat>();
                    for (int i = 0; i < values.Length; i++)
                    {
                        returnDictionary.Add(values[i], new StartingStat() {Current = 1, Maximum = 10, Minimum = -10});
                    }

                    return returnDictionary;
                }

                string[] GetRanomValues(int count)
                {
                    string[] returnValues = new string[count];
                    for (int i = 0; i < count; i++)
                    {
                        returnValues[i] = Guid.NewGuid().ToString();
                        Console.WriteLine(returnValues[i]);
                    }

                    return returnValues;
                }

                ICardOption[] CreateCardOptions(int count, string[] valueNames)
                {
                    CardOptionsStub[] options = new CardOptionsStub[count];
                    for (int i = 0; i < count; i++)
                    {
                        Dictionary<string, int> stats = new Dictionary<string, int>();
                        for (int j = 0; j < valueNames.Length; j++)
                        {
                            stats.Add(valueNames[j], new Random().Next(-10, 10));
                        }

                        options[i] = new CardOptionsStub(
                            "Option Two",
                            "Option Two Description",
                            new PlayerStatsToChangeStub(stats),
                            new string[0]);
                    }

                    return options;
                }
            }
        }
    }
}