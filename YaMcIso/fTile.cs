using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.mojang.minecraft.level;
using System.Drawing;

namespace YaMcIso
{
	class fTile
	{
		public Color Main = Color.FromArgb(128, 128, 128);
        public Color Highlight = Color.FromArgb(164, 164, 164);
        public Color Shadow = Color.FromArgb(100, 100, 100);
		public Boolean renderGlass = false;
		public Boolean renderWater = false;
		public Boolean transparent = false;
		public Boolean isAir = false;

		public fTile(Tile til)
		{
			switch (til)
			{
				case Tile.Air:
					transparent = true;
					isAir = true;
					break;

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
					transparent = true;
					break;

				case Tile.StillWater:
					Main = Color.FromArgb(0, 171, 236);
					Highlight = Color.FromArgb(70, 204, 255);
					Shadow = Color.FromArgb(0, 123, 170);
					renderWater = true;
					transparent = true;
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
					transparent = true;
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
		}
	}
}
