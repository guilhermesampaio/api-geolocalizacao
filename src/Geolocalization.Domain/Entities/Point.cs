using System.Collections.Generic;

namespace Geolocalization.Domain.Entities
{
    public class Point : GeoPosition
    {
        public Point(IEnumerable<double> coordinates)
        {
            Coordinates = coordinates;
            Type = "Point";
        }

        public IEnumerable<double> Coordinates { get; }
    }
}

