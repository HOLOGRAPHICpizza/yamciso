// Yet another Minecraft Isometric image renderer

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Diagnostics;


namespace YaMcIso
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CommandConfig.LoadConfig(args);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Console.WriteLine(CommandConfig.Usage());
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unknown exception occurred: {0}", ex.Message);
                Console.WriteLine(ex.StackTrace);
                return;
            }

            try
            {
                CommandConfig.ValidateConfig();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Console.WriteLine(CommandConfig.Usage());
                return;
            }

            if (!File.Exists(CommandConfig.MapFile))
            {
                Console.WriteLine("Unable to load level: {0}", CommandConfig.MapFile);
                return;
            }

            Console.WriteLine("Loading Level...");
            gLevel lvl = new gLevel(CommandConfig.MapFile);

			// Begin drawing the image.
			Console.WriteLine("\nRendering Level...");
			System.Drawing.Bitmap flag = new System.Drawing.Bitmap((1024 + 128) * 2, (1500 + 64) * 2);
            for (int x = 0; x < lvl.width; x++)
            {
                for (int y = 0; y < lvl.length; y++)
                {
                    for (int z = 0; z < lvl.height; z++)
                    {
						if (!lvl.getTile(x, y, z).isAir)
                        {
							if (lvl.getTile(x + 1, y, z).transparent ||
								lvl.getTile(x, y + 1, z).transparent ||
								lvl.getTile(x, y, z + 1).transparent)
                            {
								drawBlock(flag, x, z, y, lvl.getTile(x, y, z));
                            }
                        }
                    }
                }

				Console.WriteLine("Rendered " + (x + 1) + "/" + lvl.width);
            }

            flag.Save(CommandConfig.Output, System.Drawing.Imaging.ImageFormat.Png);

            Console.WriteLine("\nProcessing image...");
            processImage(CommandConfig.Output, CommandConfig.ImgWidth);

            Console.WriteLine(CommandConfig.Output + " rendered successfully!");
        }

		// TODO: Make this call ImageMagick directly, instead of using an external exe.
        static void processImage(String file, int width)
        {
            String resize = "";
            if (width > 0) // User wants resize.
            {
                resize = "-resize " + width + "x ";
            }

            Process pro = new Process();
            pro.StartInfo.UseShellExecute = false;
            pro.StartInfo.RedirectStandardOutput = false;
            pro.StartInfo.FileName = "ImageMagick/convert.exe";
            pro.StartInfo.Arguments = file + " -trim " + resize + file;
            pro.Start();
            pro.WaitForExit();
        }

        static void drawBlock(Bitmap flag, int x, int y, int z, fTile til)
        {
			int xOffset = (flag.Width / 4);
            int yOffset = (flag.Height / 2);
            if (til.renderGlass)
            {
                drawGlass(flag, (x + xOffset - y), (x + yOffset + y), z);
            }
            else if (til.renderWater)
            {
                drawWater(flag, (x + xOffset - y), (x + yOffset + y), z);
            }
            else
            {
                drawCube(flag, (x + xOffset - y), (x + yOffset + y), z, til.Main, til.Highlight, til.Shadow);
            }
        }

        static void drawCube(Bitmap img, int px, int py, int pz, Color Main, Color Highlight, Color Shadow)
        {
            int x = (px) * 2;
            int y = (py - (pz + 1 * pz)) * 1;
            img.SetPixel(x + 0, y + 1, Highlight);
            img.SetPixel(x + 0, y + 2, Highlight);

            img.SetPixel(x + 1, y + 0, Main);
            img.SetPixel(x + 1, y + 1, Main);
            img.SetPixel(x + 1, y + 2, Highlight);
            img.SetPixel(x + 1, y + 3, Highlight);

            img.SetPixel(x + 2, y + 0, Main);
            img.SetPixel(x + 2, y + 1, Main);
            img.SetPixel(x + 2, y + 2, Shadow);
            img.SetPixel(x + 2, y + 3, Shadow);

            img.SetPixel(x + 3, y + 1, Shadow);
            img.SetPixel(x + 3, y + 2, Shadow);
        }

        static void drawWater(Bitmap img, int px, int py, int pz)
        {
            int x = (px) * 2;
            int y = (py - (pz + 1 * pz)) * 1;
            makeWater(img, x + 0, y + 1);
            makeWater(img, x + 0, y + 2);

            makeWater(img, x + 1, y + 0);
            makeWater(img, x + 1, y + 1);
            makeWater(img, x + 1, y + 2);
            makeWater(img, x + 1, y + 3);

            makeWater(img, x + 2, y + 0);
            makeWater(img, x + 2, y + 1);
            makeWater(img, x + 2, y + 2);
            makeWater(img, x + 2, y + 3);

            makeWater(img, x + 3, y + 1);
            makeWater(img, x + 3, y + 2);
        }

        static void drawGlass(Bitmap img, int px, int py, int pz)
        {
            int x = (px) * 2;
            int y = (py - (pz + 1 * pz)) * 1;
            makeGlass(img, x + 0, y + 1);
            makeGlass(img, x + 0, y + 2);

            makeGlass(img, x + 1, y + 0);
            makeGlass(img, x + 1, y + 1);
            makeGlass(img, x + 1, y + 2);
            makeGlass(img, x + 1, y + 3);

            makeGlass(img, x + 2, y + 0);
            makeGlass(img, x + 2, y + 1);
            makeGlass(img, x + 2, y + 2);
            makeGlass(img, x + 2, y + 3);

            makeGlass(img, x + 3, y + 1);
            makeGlass(img, x + 3, y + 2);
        }

        static void makeWater(Bitmap img, int x, int y)
        {
            Color Col;
            Color NewCol;
            Col = img.GetPixel(x, y);
            int nR = Col.R;
            int nG = Col.G;
            int nB = Col.B;

            if (Col.R - 15 < 0)
            {
                nR = 0;
            }
            else
            {
                nR = Col.R - 15;
            }

            if (Col.G - 8 < 0)
            {
                nG = 0;
            }
            else
            {
                nG = Col.G - 8;
            }

            if (Col.B + 25 > 255)
            {
                nB = 255;
            }
            else
            {
                nB = Col.B + 20;
            }

            NewCol = Color.FromArgb(nR, nG, nB);
            img.SetPixel(x, y, NewCol);
        }

        static void makeGlass(Bitmap img, int x, int y)
        {
            Color Col;
            Color NewCol;
            int change = 30;
            Col = img.GetPixel(x, y);
            int nR;
            int nG;
            int nB;
            if (Col.R + change > 255)
            {
                nR = 255;
            }
            else
            {
                nR = Col.R + change;
            }
            if (Col.G + change > 255)
            {
                nG = 255;
            }
            else
            {
                nG = Col.G + change;
            }
            if (Col.B + change > 255)
            {
                nB = 255;
            }
            else
            {
                nB = Col.B + change;
            }
            NewCol = Color.FromArgb(nR, nG, nB);
            img.SetPixel(x, y, NewCol);
        }

    }
}