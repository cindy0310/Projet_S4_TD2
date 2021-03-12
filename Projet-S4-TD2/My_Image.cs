using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Projet_S4_TD2
{
    class My_Image
    {
        byte [] image ;
        private string typeimage;
        private int tailleFichier;
        private int offset;
        private int largeur;
        private int hauteur;
        private int bitRGB;
        private int tailleimage;
        byte[,] matriceRGB;
        

        public My_Image(byte [] image , string typeimage, int tailleFichier, int offset, int largeur, int hauteur, int bitRGB, byte[,] matriceRGB)
        {
            this.image = image;
            this.typeimage = typeimage;
            this.tailleFichier = tailleFichier;
            this.offset = offset;
            this.largeur = largeur;
            this.hauteur = hauteur;
            this.bitRGB = bitRGB;
            this.matriceRGB = matriceRGB;        
        }
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

        public My_Image(string myfile)
        {
            // IMAGE 

            this.image = File.ReadAllBytes(myfile);

            // HEADER info 14 premiers octets
            for (int i = 0; i < 14; i++)
            {
                Console.Write(image[i]+" ");
            }
            Console.WriteLine(" ");

            // TYPE IMAGE : les 2 premiers octets 0 et 1 

            typeimage = "BM"; //BM pour bitmap


            //TAILLEFICHIER : 4 suivants 2,3,4,5

            byte[] TailleFichier = new byte[4];
            for (int i = 0; i < 4; i++)
                TailleFichier[i] = image[i + 2];
            tailleFichier = Convertir_Endian_To_Int(TailleFichier);
            //Console.WriteLine("Taille Fichier:");
            //Console.WriteLine(tailleFichier);

            //OFFSET: les octets 10,11,12,13
            byte[] Offset = new byte[4];
            for (int i = 0; i < 4; i++)
                Offset[i] = image[i + 10];
            offset = Convertir_Endian_To_Int1(Offset);
            //Console.WriteLine("Offset :");
            //Console.WriteLine(offset);


            //Métadonnées de l'image
            // HEADER image : 40 suivants
            for (int i = 14; i < 54; i++)
            {
                Console.Write(image[i] + " ");
            }
            Console.WriteLine("");

            // taille header image : 14,15,16,17

            // LARGEUR : 18,19,20,21

            byte[] Largeur = new byte[4];
            for (int i = 0; i < 4; i++)
                Largeur[i] = image[i + 18];
            largeur = Convertir_Endian_To_Int(Largeur);
            //Console.WriteLine("Largeur");
            //Console.WriteLine(largeur);


            //HAUTEUR : 22,23,24,25
            byte[] Hauteur = new byte[4];
            for (int i = 0; i < 4; i++)
                Hauteur[i] = image[i + 22];
            hauteur = Convertir_Endian_To_Int(Hauteur);
            //Console.WriteLine("Hauteur");
            //Console.WriteLine(hauteur);

            //bitRGB : 28,29

            byte[] BitRGB = new byte[2];
            for (int i = 0; i < 2; i++)
                BitRGB[i] = image[i + 28];
            bitRGB = Convertir_Endian_To_Int(BitRGB);
            //Console.WriteLine("bitRGB");
            //Console.WriteLine(bitRGB);

            // IMAGE

            int debutimage = 54;
            matriceRGB = new byte[hauteur, largeur*3 ];
            for (int i = 0; i < hauteur; i++)
                for (int j = 0; j < largeur*3; j++)
                {

                    matriceRGB[i, j] = image[debutimage];
                    debutimage++;
                    //Console.Write(matriceRGB[i, j]);

                }

        }

        
       public void From_Image_To_File2(string file)
       {
           byte[] Fichier = new byte[tailleFichier];
           // Type d'image 
           Fichier[0] = 66;
           Fichier[1] = 77;

           //TAILLEFICHIER : 4 suivants 2,3,4,5
           for (int i = 0; i < 4; i++)
           {
               Fichier[i + 2] = Convertir_Int_To_Endian(tailleFichier)[i];

           }
           // Taille Offset 
           for (int i = 0; i < 4; i++)
           {
               Fichier[i+10] = Convertir_Int_To_Endian(offset)[i];
           }
           // Taille header image : octet 14
                Fichier[14] = 40;

           // LARGEUR : 18,19,20,21
           for (int i = 0; i < 4; i++)
           {
               Fichier[i + 18] = Convertir_Int_To_Endian(largeur)[i];

           }
           //HAUTEUR : 22,23,24,25
           for (int i = 0; i < 4; i++)
           {
               Fichier[i + 22] = Convertir_Int_To_Endian(hauteur)[i];

           }
            //bitRGB : 28,29
            for (int i = 0; i < 2; i++)
            {
                Fichier[i + 28] = Convertir_Int_To_Endian(bitRGB)[i];

            }

            for (int i = 0; i < 54; i++)
            {
                Fichier[i] = image[i];
            }
            byte[] tab = new byte[3 * largeur * hauteur];
            int k = 0;
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur * 3; j++)
                {
                    tab[k] = matriceRGB[i, j];
                    k++;
                }
            }
            for (int i = 54; i < image.Length; i++)
            {
                Fichier[i] = tab[i - 54];
            }
            File.WriteAllBytes(file, Fichier);



            // RESTE entre HEADER IMAGE ET L'IMAGE
            for (int i = 29; i < 54; i++)
           {
               Fichier[i] = 0;

           }

            // IMAGE 54 jusqu'au reste 
            // à compléter

            /*


            // AFFICHAGE HEADER INFO
            for (int i = 0; i < 14; i++)
            {

                Console.Write(Fichier[i] + " ");
            }
            Console.WriteLine("");

            // AFFICHAGE HEADER IMAGE 
            for (int i = 14; i < 54; i++)
            {

                Console.Write(Fichier[i] + " ");
            }
            Console.WriteLine("");

            // AFFICHAGE IMAGE 


            for (int i = 54; i < Fichier.Length; i++)
            {

                Console.Write(Fichier[i] + " ");
            }
            */


        }
        public void From_Image_To_File(string file) 
        {
            byte[] tab = new byte[image.Length];
            tab[0] = Image[0];
            tab[1] = Image[1];            
            for (int i = 2; i < 6; i++)
            {
                tab[i] = Convertir_Int_To_Endian(tailleFichier)[i - 2];
            }
            for(int i=6; i < 14; i++)
            {
                tab[i] = 0;
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
            tab[26] = 0; tab[27] = 0;
            for (int i = 28; i < 30; i++)
            {
                tab[i] = Convertir_Int_To_Endian(bitRGB / 8)[i - 28];
            }
            for(int i=30; i < 54; i++) { tab[i] = 0; } 
            for (int i = 0; i < 54; i++)
            {
                tab[i] = image[i];
            }
            byte[] tableau = new byte[3 * largeur * hauteur];
            int k = 0;
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur * 3; j++)
                {
                    tableau[k] = matriceRGB[i, j];
                    k++;
                }
            }
            for (int i = 54; i < image.Length; i++)
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
        public int Convertir_Endian_To_Int1(byte[] tab)
        {
            return BitConverter.ToInt32(tab);
        }

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

        
        public void Convertir_Noir_Blanc()
        {
            byte[] imagefin = image;

            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur*3; j += 3)
                {
                    matriceRGB[i, j] = matriceRGB[i, j + 1] = matriceRGB[i, j + 2] = Convert.ToByte(Convert.ToInt32((matriceRGB[i, j] + matriceRGB[i, j + 1] + matriceRGB[i, j + 2]) / 3));
                }
            }
            this.image = imagefin;
        }
       
        public byte[] Convertir_Image_To_BW(byte[] image)
        {
            byte[] image2 = new byte[image.Length];
            for (int i = 0; i < 54; i++)
            {
                image2[i] = image[i];
            }

            /* for (int j = 54; j < image.Length; j += 3)
             {

                 int val1 = image[j]; int val2 = image[j + 1]; int val3 = image[j + 2];
                 int moy = Convert.ToInt32((val1 + val2 + val3) / 3);
                 image2[j] = Convert.ToByte(moy); image2[j + 1] = Convert.ToByte(moy); image2[j + 2] = Convert.ToByte(moy);
             }*/
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur * 3; j += 3)
                {
                    int val1 = matriceRGB[i, j] ; 
                    int val2 = matriceRGB[i, j + 1]; 
                    int val3 = matriceRGB[i, j + 2];

                    int moy = Convert.ToInt32((val1 + val2 + val3) / 3);
                    matriceRGB[i, j] = Convert.ToByte(moy);
                    matriceRGB[i, j + 1] = Convert.ToByte(moy);
                    matriceRGB[i, j + 2] = Convert.ToByte(moy);
                }
            }
            return image2;
        }


        public void Miroir()
        {
            byte[,] interm = new byte[hauteur, largeur * 3];
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur * 3; j += 3)
                {
                    interm[i, j] = matriceRGB[i, 3*largeur - 1 - j - 2];
                    interm[i, j + 1] = matriceRGB[i, 3*largeur - 1 - (j + 1)];
                    interm[i, j + 2] = matriceRGB[i, 3 * largeur - 1 - j];
                }
            }
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur * 3; j++)
                {
                    matriceRGB[i, j] = interm[i, j];
                }
            }
        }
        public byte[,] Agrandir(int val)
        {
            int hauteurU = hauteur * val;
            int largeurU = largeur * 3 * val;
            byte[,] matrice = new byte[hauteurU, largeurU];
            for (int i = 0; i < hauteurU; i += val)
            {
                for (int j = 0; j < largeurU; j += val)
                {
                    for (int k = 0; k < val; k++)
                    {
                        for (int l = 0; l < val; l++)
                        {
                            matrice[i + k, j + l] = matriceRGB[i, j];

                        }
                    }
                }
            }
            return matrice;
        }
        
        public void Agrandir2(int taux)
        {
            int hauteurA = hauteur * taux;
            int largeurA = largeur * taux;
            matriceRGB = new byte[hauteurA, largeurA];
            byte[,] RGBA = new byte[hauteurA, largeurA];
            for(int i = 0; i<hauteurA; i++)
            {
                for(int j = 0; j<largeurA; j++)
                {
                    for(int n = 0; n< taux; n++)
                    {
                        

                    }
                }
            }
        }
        public byte[,] MatriceA()
        {
            byte[,] matriceA = new byte[hauteur + 2, largeur * 3 + 6];
            int colonne = 0;
            int ligne = 0;
            
            for (int i = 1; i < matriceA.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < matriceA.GetLength(1) - 4; j++)
                {
                    matriceA[i, j] = matriceRGB[ i-1 , j-1];
                    colonne++;
                }
                ligne++;
            }
            return matriceA;
        }

        /*
        public byte[,] Convolution(byte[,] echantillon, double[,]noyau)
        {
            byte[,] RBGconv = new byte[echantillon.GetLength(0), echantillon.GetLength(1)];
            double end = 0;
            for (int i = 1; i < echantillon.GetLength(0)-1; i++) 
            {
                for (int j = 1; j < echantillon.GetLength(1)-1; j++)
                {
                    end = end + echantillon[i, j] * noyau[i, j];
                }
            }
        */
        public void Filtre(byte[,] motif)
        {
            byte[,] matrice = new byte[motif.GetLength(0), motif.GetLength(1)];
            byte[,] matriceA = MatriceA();
            int ligne = 0;
            int colonne = 0;
            for (int i = 1; i < matriceA.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < matriceA.GetLength(1) - 1; j++)
                {
                    for (int m = -1; m < 2; m++)
                    {
                        for (int n = -1; n < 2; n++)
                        {
                            matrice[ligne, colonne] = matriceA[i + m, j + n];
                            colonne++;
                        }
                        ligne++;
                    }
                    matriceRGB[i - 1, j - 1] = Convert.ToByte(ConvoCalcul(matrice, motif));
                }
            }
        }
        public int ConvoCalcul(byte[,] matrice, byte[,] motif)
        {
            int convocalcul = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    convocalcul = matrice[i, j] * motif[i, j];
                }
            }
            return convocalcul;
        }





    }
}
