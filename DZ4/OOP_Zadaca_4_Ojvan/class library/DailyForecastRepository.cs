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
            for (int i = dailyForecasts.Count - 1; i >= 0; i--) 
            {
                if (dailyForecasts[i].Day.Day.Equals(dailyForecast.Day.Day)) //ako je isti dan obriši jer ce kasnije ionako dodati
                {
                    dailyForecasts.RemoveAt(i);
                }
            }
            
            dailyForecasts.Add(dailyForecast); 
            
            //mehanizam za sortiranje liste
            dailyForecasts = dailyForecasts.Where(p => p.Day != null)
                .OrderBy(p => p.Day)
                .ToList();
        }

        public void Add(List<DailyForecast> dailyForecasts) 
        {
            //this.dailyForecasts.AddRange(dailyForecasts);
            foreach (DailyForecast forecast in dailyForecasts) 
            {
                for (int i = dailyForecasts.Count - 1; i >= 0; i--)
                {
                    if (this.dailyForecasts[i].Day.Day.Equals(forecast.Day.Day)) //ako je isti dan obriši jer ce kasnije ionako dodati
                    {
                        this.dailyForecasts.RemoveAt(i);
                    }
                }

                this.dailyForecasts.Add(forecast);
            }
            
            //mehanizam za sortiranje liste
            this.dailyForecasts = this.dailyForecasts.Where(p => p.Day != null)
                .OrderBy(p => p.Day)
                .ToList();
        }

        public void Remove(DateTime date) 
        {
            bool exists = false;
            for (int i = dailyForecasts.Count - 1; i >= 0; i--) 
            {
                if (dailyForecasts[i].Day.Day.Equals(date.Day)) 
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
