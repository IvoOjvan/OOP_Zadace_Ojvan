using System;
using System.Collections.Generic;
using System.Text;

namespace class_library
{
    public class BiasedGenerator : IRandomGenerator
    {
       
        #region Properties
        public Random Generator { get; private set; }
        #endregion

        #region Constructor
        public BiasedGenerator(Random generator) 
        {
            Generator = generator;
        }
        #endregion

        #region Methods
        public double GenerateDouble(double lowerBound, double upperBound)
        {
            double probability = Generator.NextDouble();

            if (probability <= 0.66) 
            {
                return Generator.NextDouble() * ((upperBound / 2) - lowerBound) - lowerBound;
            }
            else 
            {
                return Generator.NextDouble() * (upperBound - lowerBound) + lowerBound;
            } 
        }
        #endregion
    }
}
