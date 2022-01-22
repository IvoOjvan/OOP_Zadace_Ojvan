using System;
using System.Collections.Generic;
using System.Text;

namespace ForecastUI
{
    public class City
    {
        public double ID { get; set; }
        public string Name { get; set; }

        public City(double id, string name) 
        {
            ID = id;
            Name = name;
        }
    }
}
