using PackBear.Player.Interface;

namespace PackBear.Player
{
    public class StartingStat : IStartingStat
    {
        public int Current { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
    }
}