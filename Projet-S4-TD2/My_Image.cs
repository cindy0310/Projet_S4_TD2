using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Projet_S4_TD2
{
    class My_Image
    {
        byte[] image;
        private string typeimage;
        private int tailleFichier;
        private int offset;
        private int largeur;
        private int hauteur;
        private int bitRGB;
        private int tailleimage;
        byte[,] matriceRGB;
        private Pixel[,] pixel;


        /*public MyImage(byte[] image, string typeimage, int tailleFichier, int offset, int largeur, int hauteur, int bitRGB, byte[,] matriceRGB)
        {
            this.image = image;
            this.typeimage = typeimage;
            this.tailleFichier = tailleFichier;
            this.offset = offset;
            this.largeur = largeur;
            this.hauteur = hauteur;
            this.bitRGB = bitRGB;
            this.matriceRGB = matriceRGB;

        }*/
        public string TypeImage
        {
            get { return typeimage; }
            set { this.typeimage = value; }
        }
        public int TailleFichier
        {
            get { return tailleFichier; }
            set { this.tailleFichier = value; }
        }
        public int Offset
        {
            get { return offset; }
            set { this.offset = value; }
        }
        public int Largeur
        {
            get { return largeur; }
            set { this.largeur = value; }
        }
        public int Hauteur
        {
            get { return hauteur; }
            set { this.hauteur = value; }
        }
        public int TailleImage
        {
            get { return tailleimage; }
            set { this.tailleimage = value; }
        }
        public int BitRGB
        {
            get { return bitRGB; }
            set { this.bitRGB = value; }
        }
        public byte[,] MatriceRGB
        {
            get { return matriceRGB; }
            set { this.matriceRGB = value; }
        }

        public byte[] Image
        {
            get { return image; }
            set { this.image = value; }
        }
        public Pixel[,] Pixel
        {
            get { return pixel; }
            set { this.pixel = value; }
        }

        public My_Image(string myfile)
        {
            this.image = File.ReadAllBytes(myfile);
            this.typeimage = "BM";                   //i0 et i1
            byte[] Fichier = new byte[4];
            for (int i = 2; i < 6; i++)
            {
                Fichier[i - 2] = image[i];
            }
            this.tailleFichier = Convertir_Endian_To_Int(Fichier);
            byte[] Largeur = new byte[4];
            for (int j = 18; j < 22; j++)
            {
                Largeur[j - 18] = image[j];
            }
            int largeur = Convertir_Endian_To_Int(Largeur);
            this.largeur = largeur;
            byte[] Hauteur = new byte[4];
            for (int k = 22; k < 25; k++)
            {
                Hauteur[k - 22] = image[k];
            }
            this.hauteur = Convertir_Endian_To_Int(Hauteur);
            byte[] NbOctet = new byte[2];
            for (int l = 28; l < 30; l++)
            {
                NbOctet[l - 28] = image[l];
            }
            this.bitRGB = Convertir_Endian_To_Int(NbOctet) * 8;
            byte[] Offset = new byte[4];
            for (int m = 14; m < 18; m++)
            {
                Offset[m - 14] = image[m];
            }
            this.offset = Convertir_Endian_To_Int(Offset);
            Pixel[,] pixel = new Pixel[hauteur, largeur];
            int depart = 54;
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < (largeur); j++)
                {
                    Pixel pix = new Pixel(image[depart], image[depart + 1], image[depart + 2]);
                    pixel[i, j] = pix;
                    depart += 3;
                }
            }
            this.pixel = pixel;

        }
        public void From_Image_To_File(string file)
        {
            byte[] tab = new byte[14 + offset + hauteur * largeur * 3];
            tab[0] = Image[0];
            tab[1] = Image[1];
            for (int i = 2; i < 6; i++)
            {
                tab[i] = Convertir_Int_To_Endian(tailleFichier)[i - 2];
            }
            for (int i = 6; i < 14; i++)
            {
                tab[i] = image[i];
            }
            for (int i = 14; i < 18; i++)
            {
                tab[i] = Convertir_Int_To_Endian(offset)[i - 14];

            }
            for (int i = 18; i < 22; i++)
            {
                tab[i] = Convertir_Int_To_Endian(largeur)[i - 18];

            }
            for (int i = 22; i < 26; i++)
            {
                tab[i] = Convertir_Int_To_Endian(hauteur)[i - 22];
            }
            tab[26] = image[26]; tab[27] = image[27];
            for (int i = 28; i < 30; i++)
            {
                tab[i] = Convertir_Int_To_Endian(bitRGB / 8)[i - 28];
            }
            for (int i = 30; i < 54; i++)
            { tab[i] = image[i]; }

            byte[] tableau = new byte[largeur * 3 * hauteur];
            int k = 0;
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    tableau[k] = Pixel[i, j].R;
                    k++;
                    tableau[k] = Pixel[i, j].G;
                    k++;
                    tableau[k] = Pixel[i, j].B;
                    k++;
                }
            }
            for (int i = 54; i < tableau.Length; i++)
            {
                tab[i] = tableau[i - 54];
            }
            File.WriteAllBytes(file, tab);

        }



        public int Convertir_Endian_To_Int(byte[] tab)
        {
            double sortie = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                sortie += tab[i] * Math.Pow(256, i);
            }
            int end = Convert.ToInt32(sortie);

            return end;
        }
        /*public int Convertir_Endian_To_Int1(byte[] tab)
        {
            return BitConverter.ToInt32(tab);
        }*/

        public byte[] Convertir_Int_To_Endian(int val)
        {

            byte[] tab = BitConverter.GetBytes(val);
            if (!BitConverter.IsLittleEndian) Array.Reverse(tab);

            return tab;
        }
        public void AfficherTabByte(byte[] tab)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                Console.Write(tab[i] + " ");
            }
        }

        /// <summary>
        /// Converti une image en une image noire et blanche
        /// </summary>
        public void Convertir_Noir_Blanc()
        {

            /*
            byte[] imagefin = image;

            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur * 3; j += 3)
                {
                    matriceRGB[i, j] = matriceRGB[i, j + 1] = matriceRGB[i, j + 2] = Convert.ToByte(Convert.ToInt32((matriceRGB[i, j] + matriceRGB[i, j + 1] + matriceRGB[i, j + 2]) / 3));
                }
            }
            this.image = imagefin;*/

            byte[] imagefin = image;
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    pixel[i, j].R = pixel[i, j].G = pixel[i, j].B = Convert.ToByte(Convert.ToInt32((pixel[i, j].R + pixel[i, j].G + pixel[i, j].B) / 3));
                }
            }
            this.image = imagefin;
        }

        /// <summary>
        /// Méthode qui transforme une image et renvoie sa symétrie verticale
        /// </summary>
        public void Miroir()
        {
            /*
            byte[,] interm = new byte[hauteur, largeur * 3];
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur * 3; j += 3)
                {
                    interm[i, j] = matriceRGB[i, 3 * largeur - 1 - j - 2];
                    interm[i, j + 1] = matriceRGB[i, 3 * largeur - 1 - (j + 1)];
                    interm[i, j + 2] = matriceRGB[i, 3 * largeur - 1 - j];
                }
            }
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur * 3; j++)
                {
                    matriceRGB[i, j] = interm[i, j];
                }
            }*/
            Pixel[,] interm = new Pixel[hauteur, largeur];
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {

                    /*interm[i, j].R = pixel[i, largeur - 1-j].R;
                    interm[i, j].G = pixel[i, largeur - 1-j].G;
                    interm[i, j].B = pixel[i, largeur - 1-j].B;*/
                    interm[i, j] = pixel[i, largeur - 1 - j];
                }
            }
            pixel = interm;
        }
        public void Rotation90()
        {
            int temp = hauteur;
            hauteur = largeur;
            largeur = temp;
            Pixel[,] rota = new Pixel[hauteur, largeur];
            for (int i = 0; i < hauteur; i++)
            { 
                for (int j = 0; j < largeur; j++)
                {
                    rota[i, j] = pixel[largeur - 1 - j, i];
                }
            }
            pixel = rota;
        }
        
        
        public void Rotation180()
        {
            Pixel[,] rota = new Pixel[Hauteur, Largeur];

            for (int i = 0; i < Hauteur; i++)
            {
                for (int u = 0; u <Largeur; u++)
                {
                    rota[Hauteur - 1 - i, Largeur - 1 - u] = pixel[i, u];
                }
            }
            pixel = rota;

        }

        /// <summary>
        /// Méthode qui agrandi une image
        /// </summary>
        /// <param name="val"></param>
        public void Agrandir(int val)
        {
            int hauteurU = hauteur * val;
            int largeurU = largeur * val;
            Pixel[,] matrice = new Pixel[hauteurU, largeurU];
            int m = 0;
            int n = 0;
            for (int i = 0; i < hauteur; i++)
            {
                m = i * val;
                for (int j = 0; j < largeur; j++)
                {
                    n = j * val;

                    for (int k = 0; k < val; k++)
                    {
                        for (int l = 0; l < val; l++)
                        {
                            matrice[m + k, n + l] = pixel[i, j];

                        }
                    }

                }

            }
            pixel = matrice;
            largeur = largeurU;
            hauteur = hauteurU;
        }

        /// <summary>
        /// Méthode qui rétréci une image
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public byte[,] Rétrécir(int val)
        {
            int hauteurU = hauteur / val;
            int largeurU = largeur * 3 / val;
            byte[,] matrice = new byte[hauteurU, largeurU];
            for (int i = 0; i < hauteurU; i += val)
            {
                for (int j = 0; j < largeurU; j += val)
                {
                    for (int k = 0; k < val; k++)
                    {
                        byte o = matriceRGB[i * val, j * val];
                        for (int l = 0; l < val; l++)
                        {
                            matrice[i + k, j + l] = o;
                        }
                    }
                }
            }
            return matrice;
        }

        /// <summary>
        /// Méthode qui retourne la matrice de pixel entouré de 0 tout autour afin d'appliquer le calcul de convolution
        /// </summary>
        /// <returns></returns>
        public Pixel[,] MatriceA()
        {
            Pixel[,] matriceA = new Pixel[hauteur + 2, largeur + 2];
            int ligne = 0;
            for (int i = 1; i < matriceA.GetLength(0) - 1; i++)
            {
                int colonne = 0;
                for (int j = 1; j < matriceA.GetLength(1) - 1; j++)
                {
                    matriceA[i, j] = pixel[ligne, colonne];
                    colonne++;
                }
                ligne++;
            }
            for (int m = 0; m < matriceA.GetLength(0); m++)
            {
                for (int n = 0; n < matriceA.GetLength(1); n++)
                {
                    if (matriceA[m, n] == null)
                    {
                        matriceA[m, n] = new Pixel(0, 0, 0);
                    }
                }
            }
            return matriceA;
        }

        /// <summary>
        /// Méthode qui applique la convolution à un échantillon
        /// </summary>
        /// <param name="matrice"></param>
        /// <param name="motif"></param>
        /// <returns></returns>
        public Pixel ConvoCalcul(Pixel[,] matrice, int[,] motif)
        {
            double r = 0;
            double g = 0;
            double b = 0;
            int[,] flou = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //Console.WriteLine(i +" " + j);
                    r += matrice[i, j].R * motif[i, j];
                    g += matrice[i, j].G * motif[i, j];
                    b += matrice[i, j].B * motif[i, j];
                }
            }
            if (r < 0) { r = -r; }
            if (g < 0) { g = -g; }
            if (b < 0) { b = -b; }
            int cpt = 0;
            for (int m = 0; m < 3; m++)
            {
                for (int n = 0; n < 3; n++)
                {
                    if (flou[m, n] == motif[m, n])
                    {
                        cpt++;
                    }
                }
            }
            if (cpt == 9)
            {
                r = r / 9;
                g = g / 9;
                b = b / 9;
            }

            Pixel convo = new Pixel((byte)r, (byte)g, (byte)b);
            return convo;
        }

        /// <summary>
        /// Méthode qui retourne un échantillon d'une matrice de Pixel afin d'y appliquer la convolution
        /// </summary>
        /// <param name="l"></param>
        /// <param name="c"></param>
        /// <param name="matriceA"></param>
        /// <returns></returns>
        public Pixel[,] Echantillon(int l, int c, Pixel[,] matriceA)
        {
            Pixel[,] echantillon = new Pixel[3, 3];
            int i = 0;
            for (int m = -1; m < 2; m++)
            {
                int j = 0;
                for (int n = -1; n < 2; n++)
                {
                    //Console.WriteLine(matriceA[l + m, c + n]);
                    echantillon[i, j] = matriceA[l + m, c + n];
                    //Console.WriteLine(echantillon[i, j]);
                    j++;
                }
                i++;
            }

            return echantillon;
        }

        public int[,] EchantillonTest(int l, int c, int[,] matrice)
        {
            int[,] echantillon = new int[3, 3];
            int i = 0;
            for (int m = -1; m < 2; m++)
            {
                int j = 0;
                for (int n = -1; n < 2; n++)
                {
                    //Console.WriteLine(matrice[l + m, c + n]);
                    echantillon[i, j] = matrice[l + m, c + n];
                    //Console.WriteLine(echantillon[i, j]);
                    j++;
                }
                i++;
            }
            return echantillon;
        }

        /// <summary>
        /// Méthode qui applique le filtre à une matrice de Pixel
        /// </summary>
        /// <param name="motif"></param>
        public void Filtre(int[,] motif)
        {
            Pixel[,] matrice = new Pixel[motif.GetLength(0), motif.GetLength(1)];
            Pixel[,] matriceA = MatriceA();
            Pixel[,] interm = pixel;
            int ligne = 0;

            for (int i = 1; i < matriceA.GetLength(0) - 1; i++)
            {
                int colonne = 0;
                for (int j = 1; j < matriceA.GetLength(1) - 1; j++)
                {
                    interm[ligne, colonne] = ConvoCalcul(Echantillon(i, j, matriceA), motif);
                    colonne++;
                }
                ligne++;
            }
            pixel = interm;
        }

        public void AfficherMatrice(int[,] matrice)
        {
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    Console.Write(matrice[i, j] + " ");
                }
                Console.WriteLine();

            }
        }

        public void AfficherMatriceP(Pixel[,] matrice)
        {
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    if (matrice[i, j] == null)
                    {
                        Console.Write("null");
                    }
                    else
                    {
                        Console.Write(matrice[i, j].R + " " + matrice[i, j].G + " " + matrice[i, j].B + "   ");
                    }


                }
                Console.WriteLine();

            }
        }
        public void HistogrammeR()
        {
            Hauteur = 900;
            Largeur = 300;
            Pixel[,] histo = new Pixel[Hauteur, Largeur];
            Pixel[,] Rouge = new Pixel[Hauteur, Largeur];
            for (int i = 0; i < 300; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    Rouge[i, j] = new Pixel(255, 255, 255);
                    histo[i, j] = new Pixel(255, 255, 255); // R B G // tout mettre en blanc
                }
            }

            //histo[5, 255] = new Pixel(0, 0, 0);
            //histo[6, 255] = new Pixel(0, 0, 0);
            // histo[7, 255] = new Pixel(0, 0, 0);

            histo[1, 1] = new Pixel(255, 0, 0);
            histo[1, 255] = new Pixel(255, 0, 0);
            //histo[8, 255] = new Pixel(255, 0, 0);
            //histo[9, 255] = new Pixel(255, 0, 0);
            //histo[10, 255] = new Pixel(255, 0, 0);
            //histo[11, 255] = new Pixel(255, 0, 0);
            Pixel = histo;
        }
        public void FractaleMandelbrot()
        {
            Largeur = 320; // 320
            Hauteur = 320;
            Pixel[,] fractale = new Pixel[Hauteur, Largeur];
            for (int i = 0; i < Hauteur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    fractale[i, j] = new Pixel(0, 0, 0);
                }
            }
            for (int i = 0; i < Hauteur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {

                    double a = (double)(i - (Hauteur / 2)) / (double)(Hauteur / 4); // 2 ok
                    double b = (double)(j - (Largeur / 4)) / (double)(Largeur / 4);


                    double c_r = a;
                    double c_i = b;
                    double z_r = 0;
                    double z_i = 0;
                    int start = 0;
                    int iterationMax = 25;

                    while (start < iterationMax)
                    {
                        double tmp = z_r;
                        z_r = z_r * z_r - z_i * z_i + c_r;
                        z_i = 2 * z_i * tmp + c_i;
                        start++;
                    }


                    if ((start == iterationMax) && (z_r * z_r + z_i * z_i < 4))
                    {
                        fractale[j, i].R = Convert.ToByte(i / 2);
                        fractale[j, i].G = 0;
                        fractale[j, i].B = Convert.ToByte(j / 2);

                    }
                    else
                    {
                        fractale[j, i] = new Pixel(0, 0, 0);
                    }


                }
                Pixel = fractale;
            }
        }
        public void Fractale()
        {
            Largeur = 320; // 320
            Hauteur = 320;
            Pixel[,] matrice = new Pixel[Hauteur, Largeur];

            for (int i = 0; i < Hauteur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    matrice[i, j] = new Pixel(255, 255, 255);
                }
            }


            double x1 = -2.1;
            double x2 = 0.6;
            double y1 = -1.2;
            double y2 = 1.2;
            double zoom = 70; // pour une distance de 1 sur le plan, on a 100 pixels sur l'image
            int NbIteration = 50;

            // calcul de la taille de l'image 
            double largeur = (x2 - x1) * zoom;
            double hauteur = (y2 - y1) * zoom;

            int l = Convert.ToInt32(largeur);
            int h = Convert.ToInt32(hauteur);



            for (int x = 0; x < largeur; x++)
            {
                for (int y = 0; y < hauteur; y++)
                {
                    double c_r = x / zoom + x1; 
                    double c_i = y / zoom + y1; 
                    double z_r = 0;
                    double z_i = 0;
                    double i = 0;



                    while ((i < NbIteration) && (z_r * z_r) - (z_i * z_i) + c_r < 4)
                    {
                        double tmp = z_r;
                        z_r = (z_r * z_r) - (z_i * z_i) + c_r;
                        z_i = 2 * z_i * tmp + c_i;
                        i++;
                    }

                    if (i == NbIteration)
                    {
                        matrice[y, x].R = Convert.ToByte(x / 2);
                        matrice[y, x].B = 2;
                        matrice[y, x].G = Convert.ToByte(y / 2);


                    }
                    else
                    {
                        matrice[y, x] = new Pixel(255, 255, 255);
                    }

                }

            }
            Pixel = matrice;

        }

        public void Histogramme()
        {
            Pixel[,] Rouge = new Pixel[250, 256];
            Pixel[,] Bleu = new Pixel[250, 256];
            Pixel[,] Vert = new Pixel[250, 256];


            for (int i = 0; i < 250;i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    Rouge[i, j] = new Pixel(255, 255, 255);
                    Bleu[i, j] = new Pixel(255, 255, 255);
                    Vert[i, j] = new Pixel(255, 255, 255);
                }
            }
            int p = 0;
            int R = 0; 
            int V = 0; 
            int B = 0;

            while (p < 256)
            {
                for (int i = 0; i < Hauteur; i++)
                {
                    B = 0;
                    R = 0;
                    V = 0;

                    for (int j = 0; j < Largeur; j++)
                    {
                        if (Pixel[i, j].R == p)
                        {                     
                            Rouge[R, p] = new Pixel(255, 0, 0);
                            R++;
                        }
                        if (Pixel[i, j].G == p)
                        {    
                            Vert[V, p] = new Pixel(0, 255, 0);
                            V++;

                        }
                        if (Pixel[i, j].B == p)
                        {                           
                            Bleu[B, p] = new Pixel(0, 0, 255);
                            B++;
                        }
                    }
                }
                p++;
            }
            Largeur = 256;
            Hauteur = 3 * 250;
            Pixel[,] histo = new Pixel[Hauteur, Largeur];

            for (int i = 0; i < Hauteur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    histo[i, j] = new Pixel(255, 255, 255);
                }
            }
            for (int i = 0; i < 750; i++)
            {

            }
            for (int i = 0; i < 250; i++) 
            {
                for (int j = 0; j < 256; j++) 
                {
                    histo[i, j] = Rouge[i, j];
                }
            }
            for (int i = 250; i < 500; i++)
            {
                for (int j = 0; j < 256; j++) 
                {
                    histo[i, j] = Vert[i - 250, j];
                }
            }
            for (int i = 500; i < 750; i++)
            {
                for (int j = 0; j < 256; j++) 
                {
                    histo[i, j] = Bleu[i - 500, j];
                }
            }
            Pixel = histo;
        }
        
    }
}
