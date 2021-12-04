using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace class_library
{
    public static class ForecastUtilities
    {
        public static Weather FindWeatherWithLargestWindchill(Weather[] weathers)
        {
            Weather LargestWindChillWeather = weathers[0];

            for (int i = 0; i < weathers.Length; i++)
                if (LargestWindChillWeather.CalculateWindChill() < weathers[i].CalculateWindChill())
                    LargestWindChillWeather = weathers[i];

            return LargestWindChillWeather;
        }

        public static void PrintWeathers(IPrinter[] printers, Weather[] weathers)
        {
            ConsolePrinter consolePrinter = (ConsolePrinter)printers[0];
            Console.ForegroundColor = consolePrinter.TextColor;
   
            FilePrinter filePrinter = (FilePrinter)printers[1];
            StreamWriter writer = new StreamWriter(filePrinter.FileName);

            foreach (Weather weather in weathers)
            {
                Console.WriteLine(weather.GetAsString());
                writer.WriteLine(weather.GetAsString());
            }

            Console.ResetColor();
            writer.Close();
        }
    }
}
