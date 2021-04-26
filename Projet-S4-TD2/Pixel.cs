using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projet_S4_TD2
{
    class Pixel
    {
        private byte r;
        private byte g;
        private byte b;

        public Pixel(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
        public void Nuance(byte couleur)
        {
            r = couleur;
            b = couleur;
            g = couleur;

        }
        public byte R
        {
            get { return r; }
            set { r = value; }
        }
        public byte B
        {
            get { return b; }
            set { b = value; }
        }
        public byte G
        {
            get { return g; }
            set { g = value; }
        }
        
    }
}

