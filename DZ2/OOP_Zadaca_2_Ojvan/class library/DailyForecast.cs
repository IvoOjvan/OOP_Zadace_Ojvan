using System;
using System.Collections.Generic;
using System.Text;

namespace class_library
{
    public class DailyForecast
    {
        #region Properties
        public DateTime Day { get; private set; }
        public Weather DayWeather { get; private set; }
        #endregion

        #region Constructor
        public DailyForecast(DateTime day, Weather dayWeather)
        {
            this.Day = day;
            this.DayWeather = dayWeather;
        }
        #endregion

        #region Methods
        public string GetAsString()
        {
            //inace ispisuje mjesec prije dana
            //{this.Day.Day}/{this.Day.Month}/{this.Day.Year} {this.Day.TimeOfDay}
            return $"{this.Day.Day}/{this.Day.Month}/{this.Day.Year} {this.Day.TimeOfDay}°C, w={DayWeather.GetWindSpeed()}km/h, h={DayWeather.GetHumidity()}%";
        }

       
        #endregion
    }
}
