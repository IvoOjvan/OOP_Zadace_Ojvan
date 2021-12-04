using System;
using System.Collections.Generic;
using System.Text;

namespace class_library
{
    public class ConsolePrinter : IPrinter
    {
        #region Properties
        public ConsoleColor TextColor { get; private set; }
        #endregion

        #region Constructor
        public ConsolePrinter(ConsoleColor textColor)
        {
            TextColor = textColor;
        }
        #endregion

        #region Methods
        public void SetTextColor(ConsoleColor color) 
        {
            TextColor = color;
        }


        #endregion
    }
}
