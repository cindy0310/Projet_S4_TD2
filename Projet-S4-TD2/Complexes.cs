using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_S4_TD2
{
    public class Complexes
    {
        public double a; // Partie Réelle
        public double b; // Partie Imaginaire

        public Complexes(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        /// <summary>
        /// Calcule le module |Z| = Sqrt(a*a + b*b).
        /// </summary>
        /// retourne le module d'un nombre complexe
        public double Module()
        {
            return Math.Sqrt(a * a + b * b);
        }
        public void Add(Complexes c)
        {
            a = a + c.a;
            b = b = c.b;
        }
    }
}
