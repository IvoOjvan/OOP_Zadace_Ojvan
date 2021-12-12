using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace class_library
{
    public class FilePrinter : IPrinter
    {
        #region Propeties
        public string FileName { get; private set; }
        #endregion

        #region Constructor
        public FilePrinter(string fileName)
        {
            FileName = fileName;
        }
        #endregion

        #region Methods
        public void SetFileName(string fileName) 
        {
            FileName = fileName;
        }

        public void Print(Weather[] weathers)
        {
            StringBuilder weathersInfo = new StringBuilder();
            StreamWriter writer = new StreamWriter(FileName);

            foreach (Weather weather in weathers)
            {
                weathersInfo.Append(weather.GetAsString() + "\n");
            }

            writer.WriteLine(weathersInfo.ToString());
            writer.Close();
        }
        #endregion
    }
}
