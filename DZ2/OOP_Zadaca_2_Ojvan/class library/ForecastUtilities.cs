using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace class_library
{
    public static class ForecastUtilities
    {
        public static DailyForecast Parse(string dailyForecast)
        {
            string[] parts = dailyForecast.Split(",");

            DateTime date = DateTime.ParseExact(parts[0], "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentUICulture);
            Weather weather = new Weather(double.Parse(parts[1]), double.Parse(parts[3]), double.Parse(parts[2]));

            return new DailyForecast(date, weather);
        }

        public static Weather FindWeatherWithLargestWindchill(Weather[] weathers)
        {
            Weather LargestWindChillWeather = weathers[0];

            for (int i = 0; i < weathers.Length; i++)
                if (LargestWindChillWeather.CalculateWindChill() < weathers[i].CalculateWindChill())
                    LargestWindChillWeather = weathers[i];

            return LargestWindChillWeather;
        }
    }
}
