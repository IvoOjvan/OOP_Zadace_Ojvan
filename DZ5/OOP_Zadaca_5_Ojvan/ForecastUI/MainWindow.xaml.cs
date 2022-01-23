using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ForecastUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string APP_ID = "64e752fb34d613672640d595ee04e60f";
        public MainWindow()
        {
            InitializeComponent();
            DefaultCity();
            DefaultForecast();
        }

        private void DefaultCity() 
        {
            const int OsijekId = 3193935;

            string query = String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&appid={1}&units=metric", OsijekId, APP_ID);
            JObject response = JObject.Parse(new System.Net.WebClient().DownloadString(query));

            var objects = JsonConvert.DeserializeObject<APIWeatherInfo.root>(response.ToString());
            APIWeatherInfo.root outPut = objects;

            Weather weather = new Weather(outPut.main.temp, outPut.main.humidity / 100, outPut.wind.speed);

            cityLabel.Content = outPut.name + $", {DateTime.Now.ToString("MMMM dd")}";
            tempLabel.Content = Math.Round(outPut.main.temp) + "°";
            windLabel.Content = $"Wind speed {Math.Round(outPut.wind.speed)} km/h";
            humLabel.Content = $"Humidity {Math.Round(outPut.main.humidity)} %";
            feelsLabel.Content = $"Feels like {Math.Round(weather.CalculateFeelsLikeTemperature())} °C";
            sumLabel.Content = FirstToUpper(response.SelectToken("weather[0].description").ToString());

            BitmapImage image = new BitmapImage(new Uri("http://openweathermap.org/img/w/" + response.SelectToken("weather[0].icon") + ".png"));
            weatherIcon.Source = image; 
        }

        private string FirstToUpper(string word) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(char.ToUpper(word[0]));
            for (int i = 1; i < word.Length; i++) 
            {
                sb.Append(word[i]);
            }
            return sb.ToString();
        }

        private void cityTextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cityTextBox.Text = "";
        }

        private void cityTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            cityTextBox.Text = string.Empty;
        }

        private void cityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cityTextBox.Text == "") 
            {
                cityTextBox.Text = "Search";
            }
        }

        private void buttonToC_Click(object sender, RoutedEventArgs e)
        {
            if (buttonToF.Foreground == Brushes.White) 
            {
                string[] tempValue = tempLabel.Content.ToString().Split("°");

                tempLabel.Content = ConvertToC(double.Parse(tempValue[0])) + "°";

                string[] feelsValue = feelsLabel.Content.ToString().Split(" ");
                feelsLabel.Content = "Feels like " + ConvertToC(double.Parse(feelsValue[2]))+ " °C";

                ConvertForecastToC();

                buttonToC.Foreground = Brushes.White;
                buttonToC.FontSize = 35;

                buttonToF.Foreground = Brushes.Wheat;
                buttonToF.FontSize = 25;
            }
        }

        private void buttonToF_Click(object sender, RoutedEventArgs e)
        {
            if (buttonToC.Foreground == Brushes.White)
            {
                string[] tempValue = tempLabel.Content.ToString().Split("°");

                tempLabel.Content = ConvertToF(double.Parse(tempValue[0])) + "°";

                //
                string[] feelsValue = feelsLabel.Content.ToString().Split(" ");
                feelsLabel.Content = "Feels like " + ConvertToF(double.Parse(feelsValue[2])) + " °F";
                //

                ConvertForecastToF();

                buttonToF.Foreground = Brushes.White;
                buttonToF.FontSize = 35;

                buttonToC.Foreground = Brushes.Wheat;
                buttonToC.FontSize = 25;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (cityTextBox.Text != string.Empty) 
            {
                List<City> cityList = new List<City>();
                using (StreamReader r = new StreamReader("JSON/city.list.json"))
                {
                    string json = r.ReadToEnd();
                    cityList = JsonConvert.DeserializeObject<List<City>>(json);
                }
                int cityID = 0;
                if (cityTextBox.Text != "" && cityTextBox.Text != "Search" && cityList.Count(it => it.Name == cityTextBox.Text) != 0)
                {
                    cityID = cityList.FindIndex(it => it.Name.ToLower() == cityTextBox.Text.ToLower());

                    string query = String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&appid={1}&units=metric", cityList[cityID].ID, APP_ID);
                    JObject response = JObject.Parse(new System.Net.WebClient().DownloadString(query));

                    var objects = JsonConvert.DeserializeObject<APIWeatherInfo.root>(response.ToString());
                    APIWeatherInfo.root outPut = objects;

                    Weather weather = new Weather(outPut.main.temp, outPut.main.humidity / 100, outPut.wind.speed);

                    cityLabel.Content = outPut.name + $", {DateTime.Now.ToString("MMMM dd")}";
                    tempLabel.Content = Math.Round(outPut.main.temp) + "°";
                    windLabel.Content = $"Wind speed {Math.Round(outPut.wind.speed)} km/h";
                    humLabel.Content = $"Humidity {Math.Round(outPut.main.humidity)} %";
                    feelsLabel.Content = $"Feels like {Math.Round(weather.CalculateFeelsLikeTemperature())} °C";
                    sumLabel.Content = FirstToUpper(response.SelectToken("weather[0].description").ToString());

                    BitmapImage image = new BitmapImage(new Uri("http://openweathermap.org/img/w/" + response.SelectToken("weather[0].icon") + ".png"));
                    weatherIcon.Source = image;

                    PopulateForecast(cityID);
                }
                else
                {
                    MessageBox.Show("No such city!", "City error");
                }
            }
        }

        private void cityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) 
            {
                btnSearch_Click(sender, e);
            }
        }

        public void DefaultForecast() 
        {

            int cityID = 3193935;

            string query = String.Format("http://api.openweathermap.org/data/2.5/forecast?id={0}&appid={1}&units=metric&cnt=5", cityID, APP_ID);
            JObject response = JObject.Parse(new System.Net.WebClient().DownloadString(query));

            var objects = JsonConvert.DeserializeObject<APIForecast>(response.ToString());
            APIForecast forecast = objects;

            //day 2
            day2Date.Content = DateTime.Now.AddDays(1).ToString("MMMM dd");
            day2MaxTemp.Content = Math.Round(forecast.list[1].main.temp_max) + "°";
            day2MinTemp.Content = Math.Round(forecast.list[1].main.temp_min) + "°";
            day2Summary.Content = forecast.list[1].weather[0].main;

            BitmapImage image = new BitmapImage(new Uri("http://openweathermap.org/img/w/" + response.SelectToken("list[1].weather[0].icon") + ".png"));
            day2Icon.Source = image;

            //day 3
            day3Date.Content = DateTime.Now.AddDays(2).ToString("MMMM dd");
            day3MaxTemp.Content = Math.Round(forecast.list[2].main.temp_max) + "°";
            day3MinTemp.Content = Math.Round(forecast.list[2].main.temp_min) + "°";
            day3Summary.Content = forecast.list[2].weather[0].main;

            image = new BitmapImage(new Uri("http://openweathermap.org/img/w/" + response.SelectToken("list[2].weather[0].icon") + ".png"));
            day3Icon.Source = image;

            //day 4
            day4Date.Content = DateTime.Now.AddDays(3).ToString("MMMM dd");
            day4MaxTemp.Content = Math.Round(forecast.list[3].main.temp_max) + "°";
            day4MinTemp.Content = Math.Round(forecast.list[3].main.temp_min) + "°";
            day4Summary.Content = forecast.list[3].weather[0].main;

            image = new BitmapImage(new Uri("http://openweathermap.org/img/w/" + response.SelectToken("list[3].weather[0].icon") + ".png"));
            day4Icon.Source = image;

            //day5
            day5Date.Content = DateTime.Now.AddDays(4).ToString("MMMM dd");
            day5MaxTemp.Content = Math.Round(forecast.list[4].main.temp_max) + "°";
            day5MinTemp.Content = Math.Round(forecast.list[4].main.temp_min) + "°";
            day5Summary.Content = forecast.list[1].weather[0].main;

            image = new BitmapImage(new Uri("http://openweathermap.org/img/w/" + response.SelectToken("list[4].weather[0].icon") + ".png"));
            day5Icon.Source = image;
        }

        public void PopulateForecast(int cityID) 
        {
            List<City> cityList = new List<City>();
            using (StreamReader r = new StreamReader("JSON/city.list.json"))
            {
                string json = r.ReadToEnd();
                cityList = JsonConvert.DeserializeObject<List<City>>(json);
            }

            string query = String.Format("http://api.openweathermap.org/data/2.5/forecast?id={0}&appid={1}&units=metric&cnt=5", cityList[cityID].ID, APP_ID);
            JObject response = JObject.Parse(new System.Net.WebClient().DownloadString(query));

            var objects = JsonConvert.DeserializeObject<APIForecast>(response.ToString());
            APIForecast forecast = objects;

            //day 2
            day2Date.Content = DateTime.Now.AddDays(1).ToString("MMMM dd");
            day2MaxTemp.Content = Math.Round(forecast.list[1].main.temp_max) + "°";
            day2MinTemp.Content = Math.Round(forecast.list[1].main.temp_min) + "°";
            day2Summary.Content = forecast.list[1].weather[0].main;

            BitmapImage image = new BitmapImage(new Uri("http://openweathermap.org/img/w/" + response.SelectToken("list[1].weather[0].icon") + ".png"));
            day2Icon.Source = image;

            //day 3
            day3Date.Content = DateTime.Now.AddDays(2).ToString("MMMM dd");
            day3MaxTemp.Content = Math.Round(forecast.list[2].main.temp_max) + "°";
            day3MinTemp.Content = Math.Round(forecast.list[2].main.temp_min) + "°";
            day3Summary.Content = forecast.list[2].weather[0].main;

            image = new BitmapImage(new Uri("http://openweathermap.org/img/w/" + response.SelectToken("list[2].weather[0].icon") + ".png"));
            day3Icon.Source = image;

            //day 4
            day4Date.Content = DateTime.Now.AddDays(3).ToString("MMMM dd");
            day4MaxTemp.Content = Math.Round(forecast.list[3].main.temp_max) + "°";
            day4MinTemp.Content = Math.Round(forecast.list[3].main.temp_min) + "°";
            day4Summary.Content = forecast.list[3].weather[0].main;

            image = new BitmapImage(new Uri("http://openweathermap.org/img/w/" + response.SelectToken("list[3].weather[0].icon") + ".png"));
            day4Icon.Source = image;

            //day5
            day5Date.Content = DateTime.Now.AddDays(4).ToString("MMMM dd");
            day5MaxTemp.Content = Math.Round(forecast.list[4].main.temp_max) + "°";
            day5MinTemp.Content = Math.Round(forecast.list[4].main.temp_min) + "°";
            day5Summary.Content = forecast.list[1].weather[0].main;

            image = new BitmapImage(new Uri("http://openweathermap.org/img/w/" + response.SelectToken("list[4].weather[0].icon") + ".png"));
            day5Icon.Source = image;
        }

        public double ConvertToC(double temp) 
        {
            return Math.Round((temp - 32) * 5 / 9);
        }

        public double ConvertToF(double temp) 
        {
            return Math.Round((temp * 1.8) + 32);
        }

        public void ConvertForecastToF() 
        {
            string day2Max = day2MaxTemp.Content.ToString();
            string day2Min = day2MinTemp.Content.ToString();
            string day3Max = day3MaxTemp.Content.ToString();
            string day3Min = day3MinTemp.Content.ToString();
            string day4Max = day4MaxTemp.Content.ToString();
            string day4Min = day4MinTemp.Content.ToString();
            string day5Max = day5MaxTemp.Content.ToString();
            string day5Min = day5MinTemp.Content.ToString();

            day2MinTemp.Content = ConvertToF(double.Parse(day2Min.Remove(day2Min.Length - 1))) + "°";
            day2MaxTemp.Content = ConvertToF(double.Parse(day2Max.Remove(day2Max.Length - 1))) + "°";
            day3MinTemp.Content = ConvertToF(double.Parse(day3Min.Remove(day3Min.Length - 1))) + "°";
            day3MaxTemp.Content = ConvertToF(double.Parse(day3Max.Remove(day3Max.Length - 1))) + "°";
            day4MinTemp.Content = ConvertToF(double.Parse(day4Min.Remove(day4Min.Length - 1))) + "°";
            day4MaxTemp.Content = ConvertToF(double.Parse(day4Max.Remove(day4Max.Length - 1))) + "°";
            day5MinTemp.Content = ConvertToF(double.Parse(day5Min.Remove(day5Min.Length - 1))) + "°";
            day5MaxTemp.Content = ConvertToF(double.Parse(day5Max.Remove(day5Max.Length - 1))) + "°";
        }

        public void ConvertForecastToC()
        {
            string day2Max = day2MaxTemp.Content.ToString();
            string day2Min = day2MinTemp.Content.ToString();
            string day3Max = day3MaxTemp.Content.ToString();
            string day3Min = day3MinTemp.Content.ToString();
            string day4Max = day4MaxTemp.Content.ToString();
            string day4Min = day4MinTemp.Content.ToString();
            string day5Max = day5MaxTemp.Content.ToString();
            string day5Min = day5MinTemp.Content.ToString();

            day2MinTemp.Content = ConvertToC(double.Parse(day2Min.Remove(day2Min.Length - 1))) + "°";
            day2MaxTemp.Content = ConvertToC(double.Parse(day2Max.Remove(day2Max.Length - 1))) + "°";
            day3MinTemp.Content = ConvertToC(double.Parse(day3Min.Remove(day3Min.Length - 1))) + "°";
            day3MaxTemp.Content = ConvertToC(double.Parse(day3Max.Remove(day3Max.Length - 1))) + "°";
            day4MinTemp.Content = ConvertToC(double.Parse(day4Min.Remove(day4Min.Length - 1))) + "°";
            day4MaxTemp.Content = ConvertToC(double.Parse(day4Max.Remove(day4Max.Length - 1))) + "°";
            day5MinTemp.Content = ConvertToC(double.Parse(day5Min.Remove(day5Min.Length - 1))) + "°";
            day5MaxTemp.Content = ConvertToC(double.Parse(day5Max.Remove(day5Max.Length - 1))) + "°";
        }
    }
}
