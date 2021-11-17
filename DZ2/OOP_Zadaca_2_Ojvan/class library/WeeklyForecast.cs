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
            string forecast = $"{Week[0].GetAsString()}\n" +
                              $"{Week[1].GetAsString()}\n" +
                              $"{Week[2].GetAsString()}\n" +
                              $"{Week[3].GetAsString()}\n" +
                              $"{Week[4].GetAsString()}\n" +
                              $"{Week[5].GetAsString()}\n" +
                              $"{Week[6].GetAsString()}";

            return forecast; 
        }
        #endregion
    }
}
