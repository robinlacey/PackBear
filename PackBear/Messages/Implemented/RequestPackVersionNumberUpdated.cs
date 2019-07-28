namespace PackBear.Messages.Implemented
{
    public class RequestPackVersionNumberUpdated : IRequestPackVersionNumberUpdated
    {
        public int PackNumber { get; set; }
    }
}