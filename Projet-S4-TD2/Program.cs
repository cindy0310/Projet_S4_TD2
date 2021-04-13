using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Projet_S4_TD2
{
    class Program  
    {
        static void Main(string[] args)
        {
            string myfile = "Test.bmp";
            My_Image image = new My_Image(myfile);
            My_Image image2 = image;
            Console.WriteLine();
            //image2.From_Image_To_File2("cocotest2.bmp");
            AfficherMatrice(image2.MatriceRGB);
            Console.WriteLine();
            //AfficherMatrice(image2.MatriceA());

            //image2.Agrandir2(2);
            //image2.From_Image_To_File("cocotest.bmp");
            /*
            image2.Convertir_Noir_Blanc();
            image2.From_Image_To_File("cocoBlack&White.bmp");
            Console.ReadKey();
            */

            /*
            Console.ReadKey();
            string myfile = "coco.bmp";
            My_Image coco = new My_Image(myfile);
            My_Image coco2 = coco;
            coco2 = coco.Miroir();
            coco.From_Image_To_File("coco3.bmp");
            */

            ///byte[,] image = coco.Image;
            //byte[,] miroir = coco.Miroir;

            /*
        
            byte[] imagefin = coco.Convertir_Noir_Blanc(image);
            for (int i = 54; i < imagefin.Length; i++)
            {
                Console.Write(imagefin[i] + " ");
            }
            Console.ReadKey();
            */



        }
        public static void AfficherMatrice(byte[,] matrice)
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
        public void Fractale()
        {
            double x1 = -2.1;
            double x2 = 0.6;
            double y1 = -1.2;
            double y2 = 1.2;
            double zoom = 100; // pour une distance de 1 sur le plan, on a 100 pixels sur l'image
            int NbIteration = 50;

            // calcul de la taille de l'image 
            double largeur = (x2 - x1) * zoom;
            double hauteur = (y2 - y1) * zoom;

            int l = Convert.ToInt32(largeur);
            int h = Convert.ToInt32(hauteur);
            byte[,] matricergbfractale = new byte[l,h];


            //  Création image


            //My_Image fractal
            //(byte[] image, "bm", int tailleFichier, int offset, int largeur, int hautuer, int bitRGB, byte[,] matriceRGB);



            for (int x = 0; x < largeur; x++)
            {
                for (int y = 0; y < hauteur; y++)
                {
                    double c_r = x / zoom + x1; // partie réelle de c
                    double c_i = y / zoom + y1; // partie imaginaire de c
                    double z_r = 0;
                    double z_i = 0;
                    double i = 0;




                    while (((z_r * z_r) - (z_i * z_i) + c_r) < 4 && (i < NbIteration))
                    {
                        double tmp = z_r;
                        z_r = (z_r * z_r) - (z_i * z_i) + c_r;
                        z_i = 2 * z_i * tmp + c_i;
                        i++;
                    }

                    if (i == NbIteration)
                    {
                        //matriceRGB[x, y] = 0; //########### Dessiner pixel(x,y)

                    }

                }
            }




        }






    }
}