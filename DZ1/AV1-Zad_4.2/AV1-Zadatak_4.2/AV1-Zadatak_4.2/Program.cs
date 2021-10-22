using System;

namespace AV1_Zadatak_4._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Damage damage = new Damage(10, 10, 20);
            Resistance resistance = new Resistance();
            Player player = new Player("Pero", damage, resistance);
            player.ShowStats();
            Console.WriteLine($"Damge amount: {damage.DamageAmount()}");
            Console.WriteLine($"Damage taken: {player.TakeDamage()}");
        }
    }
}
