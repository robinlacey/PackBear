using PackBear.UseCases.AddCard.Interface;
using PackBear.UseCases.AddCards.Interface;

namespace PackBear.UseCases.AddCards
{
    public class AddCards:IAddCards
    {
        private readonly IAddCard _addCard;

     
        public AddCards(
           IAddCard addCard)
        {
            _addCard = addCard;
        }

        
        public void Execute(string[] jsons)
        {
            foreach (string json in jsons)
            {
                _addCard.Execute(json);
            }
        }
    }
}