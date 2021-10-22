using System;
using System.Collections.Generic;
using System.Text;

namespace AV1_Zadatak_4._2
{
    class Player
    {
        #region Properties
        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private Damage damage;
        public Damage Damage
        {
            get { return this.damage; }
            set { this.damage = value; }
        }

        private Resistance resistance;
        public Resistance Resistance
        {
            get { return this.resistance; }
            set { this.resistance = value; }
        }
        #endregion

        #region Constructor
        public Player(string Name, Damage Damage, Resistance Resistance)
        {
            this.Name = Name;
            this.Damage = Damage;
            this.Resistance = Resistance;
        }
        #endregion

        #region Methods
        public double TakeDamage()
        {
            double magicDamageTaken = Damage.MagicDamage - (Damage.MagicDamage * Resistance.MagicDamageResistance);
            double physicalDamageTaken = Damage.PhysicalDamage - (Damage.PhysicalDamage * Resistance.PhysicalDamageResistance);

            return Math.Round(physicalDamageTaken + magicDamageTaken + Damage.TrueDamage, 2);
        }

        public void ShowStats()
        {
            Console.WriteLine($"Name: {this.Name}\nPhysical Res.: {this.Resistance.PhysicalDamageResistance}\nMagic Res.: {this.Resistance.MagicDamageResistance}");
        }
        #endregion
    }
}
