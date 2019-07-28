namespace PackBear.Player.Interface
{
    public interface IStartingStat
    {
        int Current { get; set; }
        int Minimum { get; set; }
        int Maximum { get; set; }
    }
}