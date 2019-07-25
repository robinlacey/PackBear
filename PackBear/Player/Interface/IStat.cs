namespace DealerBear.Player
{
    public interface IStat
    {
         int Current { get; set; }
         int Minimum { get; set; }
         int Maximum { get; set; }
    }
}