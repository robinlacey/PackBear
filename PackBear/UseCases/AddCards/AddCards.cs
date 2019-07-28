using System.Linq;
using PackBear.Adaptor.Interface;
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
        private readonly IIncrementVersionNumber _incrementVersionNumber;
        private readonly IPublishMessageAdaptor _publishMessageAdaptor;

        public AddCards(IValidCardsData validataCards, ICardGateway cardGateway,
            IIncrementVersionNumber incrementVersionNumber, IPublishMessageAdaptor publishMessageAdaptor)
        {
            _validataCards = validataCards;
            _cardGateway = cardGateway;
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
                _publishMessageAdaptor.Publish(new FailedToAddCards()
                {
                    ErrorMessages = (from validationResult in results
                        where !validationResult.Valid
                        select validationResult.ErrorMessage).ToArray()
                });
            }
            else
            {
                int newVersionNumber = _incrementVersionNumber.Execute();
                foreach (IValidationResult validationResult in results)
                {
                    validationResult.ValidCardData.VersionAdded = newVersionNumber;
                    if (_cardGateway.HasCard(validationResult.ValidCardData.CardID))
                    {
                        _cardGateway.UpdateCard(validationResult.ValidCardData);
                    }
                    else
                    {
                        _cardGateway.AddCard(validationResult.ValidCardData);
                    }
                }
            }
        }
    }
}