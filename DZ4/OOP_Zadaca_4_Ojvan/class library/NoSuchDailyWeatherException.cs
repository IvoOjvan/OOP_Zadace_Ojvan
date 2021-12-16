using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace class_library
{
    [Serializable]
    public class NoSuchDailyWeatherException : Exception
    {
        #region Constructor
        public NoSuchDailyWeatherException() { }
        public NoSuchDailyWeatherException(string messaage) : base(messaage) { }
        public NoSuchDailyWeatherException(string message, Exception innerException) : base(message, innerException) { }
        public NoSuchDailyWeatherException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion
        
    }
}
