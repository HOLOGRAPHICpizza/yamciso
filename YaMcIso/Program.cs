// Yet another Minecraft Isometric image renderer

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using com.mojang.minecraft.level;


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

			Console.WriteLine("Rendering...");

			Level lvl = Level.Load(CommandConfig.MapFile);

			System.Drawing.Bitmap flag = new System.Drawing.Bitmap((1024 + 128) * 2, (1500 + 64) * 2);

			for (int x = 0; x < lvl.width; x++)
			{
				for (int y = 0; y < lvl.depth; y++)
				{
					for (int z = 0; z < lvl.height; z++)
					{
						if (lvl.GetTile(x, y, z).ToString() != "Air")
						{
                            if (checkValidBlock(lvl.GetTile(x+1, y, z)) ||
                                checkValidBlock(lvl.GetTile(x, y+1, z)) ||
                                checkValidBlock(lvl.GetTile(x, y, z+1)))
                            {
                                drawBlock(flag, x, z, y, lvl.GetTile(x, y, z));
                            }
						}
					}
				}

				Console.WriteLine("Slice " + (x+1) + "/" + lvl.width);
			}

			flag.Save(CommandConfig.Output, System.Drawing.Imaging.ImageFormat.Png);

			Console.WriteLine("Processing image...");
			processImage(CommandConfig.Output, CommandConfig.ImgWidth);

			Console.WriteLine(CommandConfig.Output + " rendered successfully!");
		}

        static bool checkValidBlock(Tile tile)
        {
            if (tile.ToString() == "Air" ||
                tile.ToString() == "Water" ||
                tile.ToString() == "Glass" ||
                tile.ToString() == "StillWater"
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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

		static void drawBlock(Bitmap flag, int x, int y, int z, Tile Tile)
		{
			Color Main;
			Color Highlight;
			Color Shadow;
			Boolean renderGlass = false;
			Boolean renderWater = false;
			Main = Color.FromArgb(128, 128, 128);
			Highlight = Color.FromArgb(164, 164, 164);
			Shadow = Color.FromArgb(100, 100, 100);
			switch (Tile)
			{
				case Tile.Dirt:
					Main = Color.FromArgb(147, 96, 0);
					Highlight = Color.Sienna;
					Shadow = Color.SaddleBrown;
					break;

				case Tile.Grass:
					Main = Color.Green;
					Highlight = Color.Sienna;
					Shadow = Color.SaddleBrown;
					break;

				case Tile.Stone:
					Main = Color.FromArgb(128, 128, 128);
					Highlight = Color.FromArgb(164, 164, 164);
					Shadow = Color.FromArgb(100, 100, 100);
					break;

				case Tile.Cobblestone:
					Main = Color.FromArgb(90, 90, 90);
					Highlight = Color.FromArgb(124, 124, 124);
					Shadow = Color.FromArgb(64, 64, 64);
					break;

				case Tile.Water:
					Main = Color.FromArgb(0, 171, 236);
					Highlight = Color.FromArgb(70, 204, 255);
					Shadow = Color.FromArgb(0, 123, 170);
					renderWater = true;
					break;

				case Tile.StillWater:
					Main = Color.FromArgb(0, 171, 236);
					Highlight = Color.FromArgb(70, 204, 255);
					Shadow = Color.FromArgb(0, 123, 170);
					renderWater = true;
					break;

				case Tile.Lava:
					Main = Color.FromArgb(255, 125, 0);
					Highlight = Color.FromArgb(255, 100, 0);
					Shadow = Color.FromArgb(170, 50, 0);
					break;

				case Tile.StillLava:
					Main = Color.FromArgb(236, 50, 0);
					Highlight = Color.FromArgb(255, 100, 25);
					Shadow = Color.FromArgb(170, 0, 0);
					break;

				/*case Tile.StillLava:
				Main = Color.FromArgb(236, 171, 0);
				Highlight = Color.FromArgb(255, 204, 70);
				Shadow = Color.FromArgb(170, 123, 0);
				break;*/

				case Tile.Glass:
					Main = Color.FromArgb(255, 255, 255);
					Highlight = Color.FromArgb(255, 255, 255);
					Shadow = Color.FromArgb(255, 255, 255);
					renderGlass = true;
					break;

				case Tile.SolidGold:
					Main = Color.FromArgb(238, 237, 120);
					Highlight = Color.FromArgb(238, 225, 64);
					Shadow = Color.FromArgb(217, 202, 19);
					break;

				case Tile.Sand:
					Main = Color.FromArgb(208, 206, 121);
					Highlight = Color.FromArgb(217, 213, 157);
					Shadow = Color.FromArgb(197, 190, 109);
					break;

				case Tile.Gravel:
					Main = Color.FromArgb(158, 156, 71);
					Highlight = Color.FromArgb(117, 113, 57);
					Shadow = Color.FromArgb(97, 90, 09);
					break;

				case Tile.Red:
					Main = Color.FromArgb(255, 0, 0);
					Highlight = Color.FromArgb(255, 50, 50);
					Shadow = Color.FromArgb(205, 0, 0);
					break;

				case Tile.Orange:
					Main = Color.FromArgb(255, 128, 0);
					Highlight = Color.FromArgb(255, 178, 50);
					Shadow = Color.FromArgb(205, 78, 0);
					break;

				case Tile.Yellow:
					Main = Color.FromArgb(255, 255, 0);
					Highlight = Color.FromArgb(255, 255, 50);
					Shadow = Color.FromArgb(205, 205, 0);
					break;

				case Tile.LightGreen:
					Main = Color.FromArgb(128, 255, 0);
					Highlight = Color.FromArgb(178, 255, 50);
					Shadow = Color.FromArgb(78, 205, 0);
					break;

				case Tile.Green:
					Main = Color.FromArgb(0, 255, 0);
					Highlight = Color.FromArgb(50, 255, 50);
					Shadow = Color.FromArgb(0, 205, 0);
					break;

				case Tile.AquaGreen:
					Main = Color.FromArgb(0, 255, 128);
					Highlight = Color.FromArgb(50, 255, 178);
					Shadow = Color.FromArgb(0, 205, 78);
					break;

				case Tile.Cyan:
					Main = Color.FromArgb(0, 128, 255);
					Highlight = Color.FromArgb(50, 178, 255);
					Shadow = Color.FromArgb(0, 78, 205);
					break;

				case Tile.Blue:
					Main = Color.FromArgb(0, 0, 255);
					Highlight = Color.FromArgb(50, 50, 205);
					Shadow = Color.FromArgb(0, 0, 205);
					break;

				case Tile.Indigo:
					Main = Color.FromArgb(128, 0, 255);
					Highlight = Color.FromArgb(178, 50, 255);
					Shadow = Color.FromArgb(78, 0, 205);
					break;

				case Tile.Purple:
					Main = Color.FromArgb(255, 0, 255);
					Highlight = Color.FromArgb(255, 50, 255);
					Shadow = Color.FromArgb(205, 0, 205);
					break;

				case Tile.Pink:
					Main = Color.FromArgb(255, 0, 128);
					Highlight = Color.FromArgb(255, 50, 178);
					Shadow = Color.FromArgb(205, 0, 78);
					break;

				case Tile.Bedrock:
					Main = Color.FromArgb(50, 50, 50);
					Highlight = Color.FromArgb(100, 100, 100);
					Shadow = Color.FromArgb(25, 25, 25);
					break;

				case Tile.DarkGrey:
					Main = Color.FromArgb(65, 65, 65);
					Highlight = Color.FromArgb(115, 115, 115);
					Shadow = Color.FromArgb(40, 40, 40);
					break;

				case Tile.LightGrey:
					Main = Color.FromArgb(125, 125, 125);
					Highlight = Color.FromArgb(175, 175, 175);
					Shadow = Color.FromArgb(75, 75, 75);
					break;

				case Tile.White:
					Main = Color.FromArgb(200, 200, 200);
					Highlight = Color.FromArgb(255, 255, 255);
					Shadow = Color.FromArgb(150, 150, 150);
					break;

				case Tile.Trunk:
					Main = Color.FromArgb(147, 96, 0);
					Highlight = Color.Sienna;
					Shadow = Color.SaddleBrown;
					break;

				case Tile.Leaves:
					Main = Color.FromArgb(0, 128, 0);
					Highlight = Color.FromArgb(50, 178, 0);
					Shadow = Color.FromArgb(0, 78, 0);
					break;
			}
			int xOffset = (flag.Width / 4);
			int yOffset = (flag.Height / 2);
			if (renderGlass)
			{
				drawGlass(flag, (x + xOffset - y), (x + yOffset + y), z);
			}
			else if (renderWater)
			{
				drawWater(flag, (x + xOffset - y), (x + yOffset + y), z);
			}
			else
			{
				drawCube(flag, (x + xOffset - y), (x + yOffset + y), z, Main, Highlight, Shadow);
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