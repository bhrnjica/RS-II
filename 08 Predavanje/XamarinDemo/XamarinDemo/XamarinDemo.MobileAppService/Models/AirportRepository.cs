using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using SharedModels;
namespace XamarinDemo.Models
{
    public class AirportRepository : IAirportRepository
    {
        private static List<Airport> m_airports = new List<Airport>();

        public AirportRepository()
        {
            m_airports = loadAirports();
        }

        public Airport Get(string id)
        {
            var ap = m_airports.Where(x => x.IATA.ToLower() == id.ToLower() || x.ICAO.ToLower() == id.ToLower()).FirstOrDefault();

            if (ap == null)
                return null;
            else
                return ap;
        }

        public IEnumerable<Airport> GetAll()
        {
            return m_airports;
        }

        public IEnumerable<Airport> Find(string name)
        {
            var ap = m_airports.Where(x => x.Name.Contains(name));
            return ap;
        }
       
        private static List<Airport> loadAirports()
        {
            var lines = System.IO.File.ReadAllLines("data/airports.dat");

            List<Airport> air = new List<Airport>();

            foreach (var l in lines)
            {
                if (string.IsNullOrEmpty(l))
                    continue;
                var cols = l.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (cols.Length < 14)
                    continue;
                var airport = new Airport()
                {
                    AirportID = cols[0],
                    Name = cols[1],
                    City = cols[2],
                    Countrey = cols[3],
                    IATA = cols[4],
                    ICAO = cols[5],
                    Latitude = cols[6],
                    Longitude = cols[7],
                    Altitude = cols[8],
                    TimeZone = cols[9],
                    DST = cols[10],
                    Tzdbtimezone = cols[11],
                    Type = cols[12],
                    Source = cols[13]


                };

                air.Add(airport);
            }

            return air;
        }
    }
}
