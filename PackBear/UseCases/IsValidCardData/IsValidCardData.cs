using System;
using System.Linq;
using PackBear.Adaptor.Interface;
using PackBear.Card.Interface;
using PackBear.Player.Interface;

namespace PackBear.UseCases.IsValidCardData
{
    public class IsValidCardData : Interface.IIsValidCardData
    
    {
        private readonly IJsonDeserializeAdaptor _jsonDeserializeAdaptor;
        private readonly IStartingStats _startingStats;

        public IsValidCardData(IJsonDeserializeAdaptor jsonDeserializeAdaptor, IStartingStats startingStats)
        {
            _jsonDeserializeAdaptor = jsonDeserializeAdaptor;
            _startingStats = startingStats;
        }

        public bool Execute(string card)
        {
            try
            {
                ICard newCard = _jsonDeserializeAdaptor.DeserializeCard(card);
                if (InvalidOptionLength(_startingStats, newCard))
                {
                    return false;
                }

                if (!HasCorrectStatKeys(_startingStats, newCard)) return false;
                if (!HasValidWeightKey(_startingStats, newCard)) return false;
            }
            catch
            {
                Console.WriteLine("Caught");
                return false;
            }

            return true;
        }

        private static bool HasValidWeightKey(IStartingStats startingStats, ICard newCard)
        {
            return startingStats.Weights.ContainsKey(newCard.CardWeight);
        }

        private static bool HasCorrectStatKeys(IStartingStats startingStats, ICard newCard)
        {
            return newCard.Options.SelectMany(t => t.PlayerStatsToChange.Stats.Keys)
                .All(keyInOption => startingStats.Stats.Keys.Contains(keyInOption));
        }

        private static bool InvalidOptionLength(IStartingStats startingStats, ICard newCard)
        {
            return newCard.Options.Length != startingStats.OptionsCount;
        }
    }
}