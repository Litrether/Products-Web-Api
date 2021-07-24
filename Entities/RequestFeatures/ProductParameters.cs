namespace Entities.RequestFeatures
{
    public class ProductParameters : RequestParameters
    {
        public ProductParameters()
        {
            OrderBy = "name";
        }

        public string SearchTerm { get; set; }

        public string Fields { get; set; }

        public string Currency { get; set; }

        public string Categories { get; set; }

        public string Providers { get; set; }

        public double MinCost { get; set; }

        public double MaxCost { get; set; } = double.MaxValue;
    }
}
