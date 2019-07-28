using System;
using System.Collections.Generic;
using System.Linq;
using PackBear.Gateway.Interface;
using PackBear.UseCases.AddCard.Interface;
using PackBear.UseCases.IncrementVersionNumber.Interface;
using PackBear.UseCases.IsValidCardData.Interface;

namespace PackBear.UseCases.AddCard
{
    public class AddCard : IAddCard
    {
        private readonly IValidCardData _validCardData;
        private readonly ICardGateway _cardGateway;
        private readonly IPackGateway _packGateway;
        private readonly IIncrementVersionNumber _incrementVersionNumber;

        public AddCard(
            IValidCardData validCardData,
            ICardGateway cardGateway,
            IPackGateway packGateway,
            IIncrementVersionNumber incrementVersionNumber)
        {
            _validCardData = validCardData;
            _cardGateway = cardGateway;
            _packGateway = packGateway;
            _incrementVersionNumber = incrementVersionNumber;
        }

        public void Execute(string json)
        {
            IValidationResult validationResult = _validCardData.Execute(json);
            if (!validationResult.Valid) { return; }
            
            if (_cardGateway.HasCard(validationResult.ValidCardData.CardID))
            {
                RemoveCardIDFromOldPack(validationResult);
                validationResult.ValidCardData.VersionAdded = _incrementVersionNumber.Execute();
                AddNewPack(validationResult);
                UpdateCardWithNewPackNumber(validationResult);
                   
            }
            else
            {
                validationResult.ValidCardData.VersionAdded = _incrementVersionNumber.Execute();
                AddNewPack(validationResult);
                _cardGateway.AddCard(validationResult.ValidCardData);
            }
        }

        private void UpdateCardWithNewPackNumber(IValidationResult validationResult)
        {
            _cardGateway.UpdateCard(validationResult.ValidCardData);
        }

        private void AddNewPack(IValidationResult validationResult)
        {
            _packGateway.SetCards(new[] {validationResult.ValidCardData.CardID}, validationResult.ValidCardData.VersionAdded);
        }

        private void RemoveCardIDFromOldPack(IValidationResult validationResult)
        {
            int oldPackVersion = _cardGateway.GetCard(validationResult.ValidCardData.CardID).VersionAdded;
            List<string> oldPack = _packGateway.GetCards(oldPackVersion).ToList();
            oldPack.Remove(validationResult.ValidCardData.CardID);
            _packGateway.SetCards(oldPack.ToArray(), oldPackVersion);
        }
    }
}