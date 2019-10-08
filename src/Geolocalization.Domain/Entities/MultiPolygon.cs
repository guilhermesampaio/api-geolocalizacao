using System.Collections.Generic;

namespace Geolocalization.Domain.Entities
{
    public class MultiPolygon : GeoPosition
    {
        public MultiPolygon(IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>> coordinates)
        {
            Coordinates = coordinates;
            Type = "MultiPolygon";
        }
        public IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>> Coordinates { get; }
    }
}

