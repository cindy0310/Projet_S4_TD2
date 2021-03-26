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






    }
}