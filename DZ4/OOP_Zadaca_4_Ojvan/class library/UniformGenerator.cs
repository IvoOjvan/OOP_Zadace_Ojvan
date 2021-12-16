using System;
using System.Collections.Generic;
using System.Text;

namespace class_library
{
    public class UniformGenerator : IRandomGenerator
    {
        #region Properties
        public Random Generator { get; private set; }
        #endregion

        #region Constructor
        public UniformGenerator(Random generator)
        {
            Generator = generator;
        }
        #endregion

        #region Methods
        public double GenerateDouble(double lowerBound, double upperBound)
        {
            return Generator.NextDouble() * (upperBound - lowerBound) + lowerBound;
        }
        #endregion
    }
}
