using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace class_library
{
    public class DailyForecastRepository : IEnumerable, IEnumerator
    {
        #region Properties
        private List<DailyForecast> dailyForecasts;
        private int position = -1;
        #endregion

        #region Constructor
        public DailyForecastRepository() 
        {
            dailyForecasts = new List<DailyForecast>();
        }

        public DailyForecastRepository(DailyForecastRepository repository) 
        {
            dailyForecasts = new List<DailyForecast>();
            foreach (DailyForecast forecast in repository.dailyForecasts) 
            {
                dailyForecasts.Add(new DailyForecast(forecast.Day, forecast.DayWeather));
            }
        }
        #endregion

        #region Methods
        public void Add(DailyForecast dailyForecast) 
        {
            if (dailyForecasts.Count == 0)
            {
                dailyForecasts.Add(dailyForecast);
            }
            else 
            {
                bool exists = new bool();
                for (int i = dailyForecasts.Count - 1; i >= 0; i--) 
                {
                    exists = false;
                    if (dailyForecasts[i].Day.Date == dailyForecast.Day.Date)
                    {
                        dailyForecasts[i] = dailyForecast;
                        exists = true;
                        break;
                    }
                }

                if (exists == false) 
                {
                    dailyForecasts.Add(dailyForecast);
                }
            }

            dailyForecasts = dailyForecasts.Where(p => p.Day != null)
                .OrderBy(p => p.Day)
                .ToList();
        }

        public void Add(List<DailyForecast> forecasts) 
        {
            foreach (DailyForecast forecast in forecasts) 
            {
                bool exists = false;
                for (int i = 0; i < dailyForecasts.Count; i++)
                {
                    if (dailyForecasts[i].Day.Date == forecast.Day.Date)
                    {
                        dailyForecasts[i] = forecast;
                        exists = true;
                    }
                }

                if (exists == false) 
                {
                    dailyForecasts.Add(forecast);
                }
            }

            dailyForecasts = dailyForecasts.Where(p => p.Day != null)
                .OrderBy(p => p.Day)
                .ToList();
        }

        public void Remove(DateTime date) 
        {
            if (dailyForecasts.Count == 0)
            {
                throw new NoSuchDailyWeatherException("Repository is empty!");
            }
            else
            {
                bool exists = new bool();
                for (int i = dailyForecasts.Count - 1; i >= 0; i--) 
                {
                    exists = false;
                    if (dailyForecasts[i].Day.Date == date.Date) 
                    {
                        dailyForecasts.RemoveAt(i);
                        exists = true;
                    }
                }

                if (exists == false) 
                {
                    throw new NoSuchDailyWeatherException($"No daily forecast for {date.ToString("dd.MM.yyyy. HH:mm:ss")}");
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public bool MoveNext()
        {
            position++;
            return (position < dailyForecasts.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        public object Current => dailyForecasts[position];

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (DailyForecast forecast in dailyForecasts) 
            {
                stringBuilder.Append(forecast.GetAsString() + "\n");
            }

            return stringBuilder.ToString();
        }
        #endregion
    }
}
