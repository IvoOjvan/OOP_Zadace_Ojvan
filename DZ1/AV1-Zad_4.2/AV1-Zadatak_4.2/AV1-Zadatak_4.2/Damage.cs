using System;
using System.Collections.Generic;
using System.Text;

namespace AV1_Zadatak_4._2
{
    class Damage
    {
        #region Properties
        private int physicalDamage;
        public int PhysicalDamage
        {
            get { return this.physicalDamage; }
            private set { this.physicalDamage = value; }
        }

        private int magicDamage;
        public int MagicDamage
        {
            get { return this.magicDamage; }
            private set { this.magicDamage = value; }
        }

        private int trueDamage;
        public int TrueDamage
        {
            get { return this.trueDamage; }
            private set { this.trueDamage = value; }
        }

        #endregion

        #region Constructor
        public Damage(int PhysicalDamage, int MagicDamage, int TrueDamage)
        {
            this.PhysicalDamage = PhysicalDamage;
            this.MagicDamage = PhysicalDamage;
            this.TrueDamage = TrueDamage;
        }
        #endregion

        #region Methods
        public int DamageAmount()
        {
            return this.PhysicalDamage + this.MagicDamage + this.TrueDamage;
        }
        #endregion
    }
}
