using System.Collections.Generic;
using PackBear.Card.Interface;
using PackBear.Gateway.Interface;

namespace PackBearTests.Mocks
{
    public class CardGatewayFake:ICardGateway
    {
        private readonly ICard _defaultreturn;
        private readonly bool _hasCardReturn;

        public CardGatewayFake(ICard defaultreturn, bool hasCardReturn)
        {
            _defaultreturn = defaultreturn;
            _hasCardReturn = hasCardReturn;
        }

        public string GetCardID { get; private set; }

        public bool HasCardCalled { get; private set; }
        public string HasCardID { get; set; }

        public bool HasCard(string cardID)
        {
            HasCardID = cardID;
            HasCardCalled = true;
            return _hasCardReturn;
        }

        public ICard GetCard(string cardID)
        {
            if (_responses.ContainsKey(cardID))
            {
                return _responses[cardID];
            }
            GetCardID = cardID;
            return _defaultreturn;
        }

        Dictionary<string,ICard> _responses = new Dictionary<string, ICard>();
        public void SetGetCardResponse(string cardID, ICard card)
        {
            _responses.Add(cardID,card);
        }

        public bool AddCardCalled { get; private set; }
        public ICard CardAdded { get; private set; }
        public List<ICard> AddCardHistory = new List<ICard>();

        public void AddCard(ICard card)
        {
            AddCardCalled = true;
            CardAdded = card;
            AddCardHistory.Add(card);
        }

        public bool UpdateCardCalled { get; private set; }
        public ICard UpdateCardCardAdded { get; private set; }
        public string UpdateCardID { get; private set; }
        public List<ICard> UpdateCardsHistory = new List<ICard>();

        public void UpdateCard(ICard newCardData)
        {
            UpdateCardCalled = true;
            UpdateCardCardAdded = newCardData;
            UpdateCardID = newCardData.CardID;
            UpdateCardsHistory.Add(newCardData);
        }
    }
}