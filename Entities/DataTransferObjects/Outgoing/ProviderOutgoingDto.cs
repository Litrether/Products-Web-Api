namespace Entities.DataTransferObjects.Outcoming
{
    public class ProviderOutgoingDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal LocationLong { get; set; }

        public decimal LocationLat { get; set; }
    }
}
