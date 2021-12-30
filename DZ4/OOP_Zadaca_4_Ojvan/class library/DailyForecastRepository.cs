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
        private LinkedList<DailyForecast> dailyForecasts;
        private LinkedListNode<DailyForecast> currentNode;
        #endregion

        #region Constructor
        public DailyForecastRepository()
        {
            dailyForecasts = new LinkedList<DailyForecast>();
        }

        public DailyForecastRepository(DailyForecastRepository repository)
        {
            dailyForecasts = new LinkedList<DailyForecast>();
            foreach (DailyForecast forecast in repository.dailyForecasts)
            {
                DailyForecast copyForecast = new DailyForecast(forecast.Day, forecast.DayWeather);
                dailyForecasts.AddLast(copyForecast);
            }
        }
        #endregion

        #region Methods
        public void Add(DailyForecast dailyForecast)
        {
            if (dailyForecasts.Count == 0)
            {
                dailyForecasts.AddFirst(new LinkedListNode<DailyForecast>(dailyForecast));
            }
            else
            {
                if (dailyForecasts.Any(it => it.Day.Date == dailyForecast.Day.Date))
                {
                    DailyForecast existingForecast = dailyForecasts.Single(it => it.Day.Date == dailyForecast.Day.Date);
                    LinkedListNode<DailyForecast> existingNode = dailyForecasts.Find(existingForecast);
                    existingNode.Value = dailyForecast;
                }
                else if (dailyForecasts.Any(it => it.Day.Date > dailyForecast.Day.Date))
                {
                    //prvi manji datum i stavi ispred njega
                    DailyForecast olderForecast = dailyForecasts.First(it => it.Day.Date > dailyForecast.Day.Date);
                    LinkedListNode<DailyForecast> olderNode = dailyForecasts.Find(olderForecast);
                    dailyForecasts.AddBefore(olderNode, dailyForecast);
                }
                else
                {
                    //zadnji od najveceg datuma pa stavi poslje tog
                    DailyForecast newerForecast = dailyForecasts.Last(it => it.Day.Date < dailyForecast.Day.Date);
                    LinkedListNode<DailyForecast> newerNode = dailyForecasts.Find(newerForecast);
                    dailyForecasts.AddAfter(newerNode, dailyForecast);
                }
            }
        }
        public void Add(List<DailyForecast> forecasts)
        {
            foreach (DailyForecast forecast in forecasts)
            {
                if (dailyForecasts.Any(it => it.Day.Date == forecast.Day.Date))
                {
                    DailyForecast existingForecast = dailyForecasts.Single(it => it.Day.Date == forecast.Day.Date);
                    LinkedListNode<DailyForecast> existingNode = dailyForecasts.Find(existingForecast);
                    existingNode.Value = forecast;
                }
                else if (dailyForecasts.Any(it => it.Day.Date > forecast.Day.Date))
                {
                    //prvi manji datum i stavi ispred njega
                    DailyForecast olderForecast = dailyForecasts.First(it => it.Day.Date > forecast.Day.Date);
                    LinkedListNode<DailyForecast> olderNode = dailyForecasts.Find(olderForecast);
                    dailyForecasts.AddBefore(olderNode, forecast);
                }
                else
                {
                    //zadnji od najveceg datuma pa stavi poslje tog
                    DailyForecast newerForecast = dailyForecasts.Last(it => it.Day.Date < forecast.Day.Date);
                    LinkedListNode<DailyForecast> newerNode = dailyForecasts.Find(newerForecast);
                    dailyForecasts.AddAfter(newerNode, forecast);
                }
            }
        }
        public void Remove(DateTime date)
        {
            if (dailyForecasts.Count == 0)
            {
                throw new NoSuchDailyWeatherException("Repository is empty!");
            }
            else
            {
                if (dailyForecasts.Any(it => it.Day.Date == date.Date))
                {
                    DailyForecast forecast = dailyForecasts.Single(it => it.Day.Date == date.Date);
                    LinkedListNode<DailyForecast> toDelete = dailyForecasts.Find(forecast);
                    dailyForecasts.Remove(toDelete);
                }
                else
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
            if (currentNode != null)
            {
                currentNode = currentNode.Next;
            }
            return currentNode != null;
        }

        public void Reset()
        {
            currentNode = dailyForecasts.First;
        }
        public object Current => currentNode;

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
