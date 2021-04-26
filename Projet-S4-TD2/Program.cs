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
            
            string myfile = "coco.bmp";
            My_Image coco = new My_Image(myfile);
            coco.Rotation90();
           // My_Image coco2 = coco;
           // coco.Histogramme();
           // coco.From_Image_To_File("histo.bmp");


            // fractale.Fractale();
            // fractale.From_Image_To_File("fractalet.bmp");


            /*
            string myfile = "Test.bmp";
            My_Image image = new My_Image(myfile);
            My_Image image2 = image;
            Console.WriteLine();
            image2.From_Image_To_File2("cocotest2.bmp");
            AfficherMatrice(image2.MatriceRGB);
            Console.WriteLine();
            //AfficherMatrice(image2.MatriceA())
            */




            //coco.Convertir_Noir_Blanc();
            //coco.Miroir();
            coco.From_Image_To_File("cocoBlack&White.bmp");
            


            //coco.Miroir();
            //coco.From_Image_To_File("cocoMiroir.bmp");

            Console.ReadKey();



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
        






    }
}