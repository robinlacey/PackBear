using System;
using System.Linq;
using PackBear.Adaptor.Interface;
using PackBear.Card.Interface;
using PackBear.Player.Interface;

namespace PackBear.UseCases.IsValidCardData
{
    public class IsValidCardData : Interface.IIsValidCardData
    {
        public bool Execute(string card, IJsonDeserializeAdaptor jsonDeserializeAdaptor, IStartingStats startingStats)
        {
            try
            {
                ICard newCard = jsonDeserializeAdaptor.DeserializeCard(card);
                if (InvalidOptionLength(startingStats, newCard))
                {
                    return false;
                }

                if (!HasCorrectStatKeys(startingStats, newCard)) return false;
                if (!HasValidWeightKey(startingStats, newCard)) return false;
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