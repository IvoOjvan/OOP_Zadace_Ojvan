using System;
using System.Collections.Generic;
using System.Text;

namespace AV1_Zadatak_4._2
{
    class Resistance
    {
        #region Properties
        private double physicalDamageResistance;
        public double PhysicalDamageResistance
        {
            get { return this.physicalDamageResistance; }
            set { this.physicalDamageResistance = value; }
        }

        private double magicDamageResistance;
        public double MagicDamageResistance
        {
            get { return this.magicDamageResistance; }
            set { this.magicDamageResistance = value; }
        }
        #endregion

        #region Constructor
        public Resistance()
        {
            Random random = new Random();
            MagicDamageResistance = Math.Round(random.NextDouble(), 2);
            PhysicalDamageResistance = Math.Round(random.NextDouble(), 2);
        }
        #endregion
    }
}
