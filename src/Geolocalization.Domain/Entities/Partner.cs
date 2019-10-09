namespace Geolocalization.Domain.Entities
{
    public class Partner
    {
        public Partner(string tradingName, string ownerName, string document, MultiPolygon coverageArea, Point address)
        {
            TradingName = tradingName;
            OwnerName = ownerName;
            Document = document;
            CoverageArea = coverageArea;
            Address = address;
        }

        public Partner(string id, string tradingName, string ownerName, string document, MultiPolygon coverageArea, Point address)
            : this(tradingName, ownerName, document, coverageArea, address)
        {
            Id = id;
        }

        public string Id { get; private set; }
        public string TradingName { get; }
        public string OwnerName { get; }
        public string Document { get; }
        public MultiPolygon CoverageArea { get; }
        public Point Address { get; }

        public void SetId(string id)
        {
            Id = string.IsNullOrWhiteSpace(Id) ? id : Id;
        }
    }
}

