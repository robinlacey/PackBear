using System.Collections.Generic;
using System.Linq;
using PackBear.Adaptor.Interface;
using PackBear.Card.Interface;
using PackBear.Exceptions;
using PackBear.Gateway.Interface;
using PackBear.Messages.Implemented;
using PackBear.UseCases.AddCards.Interface;
using PackBear.UseCases.IncrementVersionNumber.Interface;
using PackBear.UseCases.IsValidCardData.Interface;
using PackBear.UseCases.ValidCardsData.Interface;

namespace PackBear.UseCases.AddCards
{
    public class AddCards : IAddCards
    {
        private readonly IValidCardsData _validataCards;
        private readonly ICardGateway _cardGateway;
        private readonly IPackGateway _packGateway;
        private readonly IIncrementVersionNumber _incrementVersionNumber;
        private readonly IPublishMessageAdaptor _publishMessageAdaptor;

        public AddCards(IValidCardsData validataCards, ICardGateway cardGateway, IPackGateway packGateway,
            IIncrementVersionNumber incrementVersionNumber, IPublishMessageAdaptor publishMessageAdaptor)
        {
            _validataCards = validataCards;
            _cardGateway = cardGateway;
            _packGateway = packGateway;
            _incrementVersionNumber = incrementVersionNumber;
            _publishMessageAdaptor = publishMessageAdaptor;
        }


        public void Execute(string[] jsons)
        {
            IValidationResult[] results = _validataCards.Execute(jsons);
            if (jsons.Length != results.Length)
            {
                throw new InvalidValidationResultCountException();
            }

            if (results.Any(_ => !_.Valid))
            {
                PublishErrorMessage(results);
            }
            else
            {
                int newVersionNumber = _incrementVersionNumber.Execute();
                List<string> cardIDs = new List<string>();
                foreach (IValidationResult validationResult in results)
                {
                    cardIDs.Add(validationResult.ValidCardData.CardID);
                    validationResult.ValidCardData.VersionAdded = newVersionNumber;
                    if (_cardGateway.HasCard(validationResult.ValidCardData.CardID))
                    {
                        _cardGateway.UpdateCard(validationResult.ValidCardData);
                        RemoveFromOldPack(validationResult);
                    }
                    else
                    {
                        _cardGateway.AddCard(validationResult.ValidCardData);
                    }
                }
                _packGateway.SetCards( cardIDs.ToArray(),newVersionNumber);
            }
        }

        private void PublishErrorMessage(IValidationResult[] results)
        {
            _publishMessageAdaptor.Publish(new FailedToAddCards()
            {
                ErrorMessages = (from validationResult in results
                    where !validationResult.Valid
                    select validationResult.ErrorMessage).ToArray()
            });
        }

        private void RemoveFromOldPack(IValidationResult validationResult)
        {
            ICard card = _cardGateway.GetCard(validationResult.ValidCardData.CardID);
            int oldPackNumber = card.VersionAdded;
            List<string> pack = _packGateway.GetCards(card.VersionAdded).ToList();
            pack.Remove(validationResult.ValidCardData.CardID);
            _packGateway.SetCards(pack.ToArray(), oldPackNumber);
        }
    }
}