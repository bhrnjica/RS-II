using SharedModels;
using System;
using System.Collections.Generic;

namespace XamarinDemo.Models
{
    public interface IAirportRepository
    {
        Airport Get(string id);
        IEnumerable<Airport> GetAll();
    }
}
