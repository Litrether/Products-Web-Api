namespace Entities.DataTransferObjects.Outcoming
{
    public class ProductOutgoingDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Cost { get; set; }

        public string Category { get; set; }

        public string Provider { get; set; }

        public int CategoryId { get; set; }

        public int ProviderId { get; set; }

    }
}
