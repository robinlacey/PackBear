namespace PackBear.Messages.Implemented
{
    public class FailedToAddCards : IFailedToAddCards
    {
        public string[] ErrorMessages { get; set; }
    }
}