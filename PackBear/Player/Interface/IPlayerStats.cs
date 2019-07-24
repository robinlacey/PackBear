using System.Collections.Generic;
using DealerBear.Player;

namespace DealerBear.Card.Interface
{
    public interface IPlayerStats
    {
        Dictionary<string,IStat> Stats { get; set; }
    }
}