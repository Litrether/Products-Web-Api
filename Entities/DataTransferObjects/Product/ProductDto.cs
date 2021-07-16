namespace Entities.DataTransferObjects
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string Category { get; set; }

        public string Provider { get; set; }

        public int CategoryId { get; set; }

        public int ProviderId { get; set; }

    }
}
