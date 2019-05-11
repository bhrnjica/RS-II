using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels
{
    public class Airport
    {
        public string AirportID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Countrey { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Altitude { get; set; }
        public string TimeZone { get; set; }
        public string DST { get; set; }
        public string Tzdbtimezone { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
    }
}
