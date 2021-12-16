using System;
using System.Collections.Generic;
using System.Text;

namespace class_library
{
    public class WeatherGenerator
    {
        #region Properties
        public double MinTemperature { get; private set; }
        public double MaxTemperature { get; private set; }
        public double MinHumidity { get; private set; }
        public double MaxHumidity { get; private set; }
        public double MinWindSpeed { get; private set; }
        public double MaxWindSpeed { get; private set; }

        public IRandomGenerator RandomGenerator { get; private set; }
        #endregion

        #region Constructor
        public WeatherGenerator
        (
            double minTemperature,
            double maxTemperature,
            double minHumidity,
            double maxHumidity,
            double minWindSpeed,
            double maxWindSpeed,
            IRandomGenerator randomGenerator
        )
        {
            MinTemperature = minTemperature;
            MaxTemperature = maxTemperature;
            MinHumidity = minHumidity;
            MaxHumidity = maxHumidity;
            MinWindSpeed = minWindSpeed;
            MaxWindSpeed = maxWindSpeed;
            RandomGenerator = randomGenerator;
        }
        #endregion

        #region Methods

        public void SetGenerator(IRandomGenerator generator)
        {
            this.RandomGenerator = generator;
        }
        public Weather Generate() 
        {
            double randomTemperature = RandomGenerator.GenerateDouble(MinTemperature, MaxTemperature);
            double randomHumidity = RandomGenerator.GenerateDouble(MinHumidity, MaxHumidity);
            double randomWindSpeed = RandomGenerator.GenerateDouble(MinWindSpeed, MaxWindSpeed);

            return new Weather(randomTemperature, randomHumidity, randomWindSpeed);
        }
        #endregion

    }
}
