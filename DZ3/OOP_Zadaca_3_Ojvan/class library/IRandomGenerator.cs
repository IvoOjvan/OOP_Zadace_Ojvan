using System;
using System.Collections.Generic;
using System.Text;

namespace class_library
{
    public interface IRandomGenerator
    {
        public double GenerateDouble(double lowerBound, double upperBound);
    }
}
