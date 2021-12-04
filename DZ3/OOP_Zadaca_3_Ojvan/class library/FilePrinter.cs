using System;
using System.Collections.Generic;
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
        #endregion
    }
}
