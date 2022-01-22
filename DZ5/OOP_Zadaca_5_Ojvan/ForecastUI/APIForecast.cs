using System;
using System.Collections.Generic;
using System.Text;

namespace ForecastUI
{
    public class APIForecast
    {
        public List<list> list { get; set; } //forecast list   
    }
    public class weather 
    {
        public string main { get; set; } //weather condition
        public string description { get; set; } //weather description
    }

    public class main 
    {
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }
    public class list 
    {
        public double dt { get; set; } //day in msec
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double speed { get; set; }
        public main main { get; set; }
        public List<weather> weather { get; set; } //weather list
    }
}
