using System;
using System.Collections.Generic;
using System.Text;

namespace class_library
{
    public class Weather
    {
        #region Properties
        private double temperature;
        private double humidity;
        private double windSpeed;
        #endregion

        #region Constructor
        public Weather(double temperature, double humidity, double windSpeed)
        {
            this.temperature = temperature;
            this.humidity = humidity;
            this.windSpeed = windSpeed;
        }

        public Weather()
        {
            this.temperature = 0;
            this.humidity = 0;
            this.windSpeed = 0;
        }
        #endregion

        #region Methods
        public void SetTemperature(double temperature) 
        { 
            this.temperature = temperature; 
        }

        public double GetTemperature() 
        { 
            return this.temperature; 
        }

        public void SetHumidity(double humidity) 
        { 
            this.humidity = humidity; 
        }

        public double GetHumidity() 
        { 
            return this.humidity; 
        }

        public void SetWindSpeed(double windSpeed) 
        { 
            this.windSpeed = windSpeed; 
        }

        public double GetWindSpeed() 
        { 
            return this.windSpeed; 
        }

        public double CalculateFeelsLikeTemperature()
        {
            double[] C = { -8.78469475556, 1.61139411, 2.33854883889, -0.14611605, -0.012308094, -0.0164248277778, 0.002211732, 0.00072546, -0.000003582};
            double T = this.temperature;
            double R = this.humidity;

            double feelsLikeTemperature = C[0] + C[1]*T + C[2]*R + C[3]*T*R + C[4]*T*T + C[5]*R*R + C[6]*T*T*R + C[7]*T*R*R + C[8]*T*T*R*R;
            
            return feelsLikeTemperature;
        }

        public double CalculateWindChill()
        {
            double T = this.temperature;
            double V = this.windSpeed;

            double windChill = 13.12 + 0.6215 * T - 11.37 * Math.Pow(V, 0.16) + 0.3965 * T * Math.Pow(V, 0.16); 

            if (T > 10 || this.windSpeed < 4.8) 
                return 0;

            return windChill;
        }
        #endregion
    }
}
