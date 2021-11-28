using System;
using System.Collections.Generic;
using System.Text;

namespace class_library
{
    public class WeeklyForecast
    {
        #region Properties
        public DailyForecast[] Week { get; private set; } = new DailyForecast[7];
        #endregion

        #region Constructor
        public WeeklyForecast(DailyForecast[] dailyForecasts)
        {
            this.Week = dailyForecasts;
        }
        #endregion

        #region Methods
        public double GetMaxTemperature()
        {
            Weather maxTemperature = Week[0].DayWeather;

            for(int i = 0; i < Week.Length; i++)
            {
                if(Week[i].DayWeather > maxTemperature)
                {
                    maxTemperature = Week[i].DayWeather;
                }
            }

            return maxTemperature.GetTemperature();
        }

        public string GetAsString()
        {
            StringBuilder forecast = new StringBuilder();

            for (int i = 0; i < Week.Length; i++) {
                forecast.Append(Week[i].GetAsString() + "\n");
            }

            return forecast.ToString(); 
        }

        public DailyForecast this[int index] => Week[index];
        #endregion
    }
}
