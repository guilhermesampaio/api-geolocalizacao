using System;

namespace Geolocalization.Domain.Entities
{
    public class Partner
    {
        public Partner(string tradingName, string ownerName, string document, MultiPolygon coverageArea, Point address)
        {
            Id = new Random().Next(1, int.MaxValue);
            TradingName = tradingName;
            OwnerName = ownerName;
            Document = document;
            CoverageArea = coverageArea;
            Address = address;
        }

        public int Id { get; }
        public string TradingName { get; }
        public string OwnerName { get; }
        public string Document { get; }
        public MultiPolygon CoverageArea { get; }
        public Point Address { get; }
    }
}

