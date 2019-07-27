using System;
using System.Linq;
using PackBear.Adaptor.Interface;
using PackBear.Card.Interface;
using PackBear.Player.Interface;
using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBear.UseCases.IsValidCardData
{
    public class IsValidCardData : IIsValidCardData
    
    {
        private readonly IJsonDeserializeAdaptor _jsonDeserializeAdaptor;
        private readonly IStartingStats _startingStats;

        public IsValidCardData(IJsonDeserializeAdaptor jsonDeserializeAdaptor, IStartingStats startingStats)
        {
            _jsonDeserializeAdaptor = jsonDeserializeAdaptor;
            _startingStats = startingStats;
        }

        public IValidationResult Execute(string card)
        {
            ICard newCard;
            try
            {
                newCard = _jsonDeserializeAdaptor.DeserializeCard(card);
                if (InvalidOptionLength(_startingStats, newCard))
                {
                    return new ValidationResult {Valid = false, ErrorMessage = $"Invalid Option count. The card Option count is {newCard.Options.Length }, it should be {_startingStats.OptionsCount}"};
                }

                if (!HasCorrectStatKeys(_startingStats, newCard)) return new ValidationResult {Valid = false,ErrorMessage = $"Invalid Stat values. The card has the following Stat values {GetCardStatValuesAsString(newCard)}, it should have:  {GetStartingStatValuesAsString(_startingStats)}"};
                if (!HasValidWeightKey(_startingStats, newCard)) return new ValidationResult {Valid = false,ErrorMessage = $"Invalid Weight value. The card can have one of the following weight values {GetStartingWeightValuesAsString(_startingStats)}, it currently has:  {GetCardWeightValueAsString(newCard)}"};
            }
            catch
            {
                return new ValidationResult {Valid = false, ErrorMessage = "Failed to parse json string"};
            }

            return new ValidationResult {Valid = true, ValidCardData = newCard};
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

        string GetStartingStatValuesAsString(IStartingStats startingStats)
        {
            string returnString = "";
            foreach (string statsKey in startingStats.Stats.Keys)
            {
                returnString += statsKey + " ";
            }

            return returnString.Trim();
        }
        
        string GetStartingWeightValuesAsString(IStartingStats startingStats)
        {
            string returnString = "";
            foreach (string statsKey in startingStats.Weights.Keys)
            {
                returnString += statsKey + " ";
            }

            return returnString.Trim();
        }

        string GetCardWeightValueAsString(ICard card) => card.CardWeight;
        
        string GetCardStatValuesAsString(ICard card)
        {
            if (!card.Options.Any())
            {
                return "Failed to get Stat values as no card Options in card";
            }
            string returnString = "";
            foreach (string statsKey in card.Options[0].PlayerStatsToChange.Stats.Keys)
            {
                returnString += statsKey + " ";
            }

            return returnString.Trim();
        }
    }
}