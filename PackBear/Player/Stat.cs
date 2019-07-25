using DealerBear.Player;

namespace PackBear.Player
{
    public class Stat:IStat
    {
        public int Current { get; set; }
        public int Minimum { get; set;}
        public int Maximum { get; set;}
    }
}