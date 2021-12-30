using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
            return $"{Day.ToString("dd.MM.yyyy. HH:mm:ss")}: {DayWeather.GetAsString()}";
        }
        #endregion
    }
}
