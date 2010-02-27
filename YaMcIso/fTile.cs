// fTile
// Fast Tile Class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.mojang.minecraft.level;
using LibMinecraft;
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
		public McBlock type = McBlock.Rock; // Use LibMinecraft block types, they are faster. Default is rock.

		public fTile()
		{
			// Default tile
			transparent = true;
		}

		public fTile(Tile til)
		{
			// Convert Tile to McBlock. This list might be incorrect / incomplete, its kind of hard to check.
			switch (til)
			{
				case Tile.Air: type = McBlock.Air; break;
				case Tile.Stone: type = McBlock.Rock; break;
				case Tile.Grass: type = McBlock.Grass; break;
				case Tile.Dirt: type = McBlock.Dirt; break;
				case Tile.Cobblestone: type = McBlock.Cobblestone; break;
				case Tile.Wood: type = McBlock.Wood; break;
				case Tile.Shrub: type = McBlock.Sapling; break;
				case Tile.Bedrock: type = McBlock.Adminium; break;
				case Tile.Water: type = McBlock.Water; break;
				case Tile.StillWater: type = McBlock.StationaryWater; break;
				case Tile.Lava: type = McBlock.Lava; break;
				case Tile.StillLava: type = McBlock.StationaryLava; break;
				case Tile.Sand: type = McBlock.Sand; break;
				case Tile.Gravel: type = McBlock.Gravel; break;
				case Tile.GoldOre: type = McBlock.GoldOre; break;
				case Tile.IronOre: type = McBlock.IronOre; break;
				case Tile.CoalOre: type = McBlock.CoalOre; break;
				case Tile.Trunk: type = McBlock.TreeTrunk; break;
				case Tile.Leaves: type = McBlock.Leaves; break;
				case Tile.Sponge: type = McBlock.Sponge; break;
				case Tile.Glass: type = McBlock.Glass; break;
				case Tile.Red: type = McBlock.Cloth1; break;
				case Tile.Orange: type = McBlock.Cloth2; break;
				case Tile.Yellow: type = McBlock.Cloth3; break;
				case Tile.LightGreen: type = McBlock.Cloth4; break;
				case Tile.Green: type = McBlock.Cloth5; break;
				case Tile.AquaGreen: type = McBlock.Cloth6; break;
				case Tile.Cyan: type = McBlock.Cloth7; break;
				case Tile.Blue: type = McBlock.Cloth8; break;
				case Tile.Indigo: type = McBlock.Cloth9; break;
				case Tile.Purple: type = McBlock.Cloth10; break;
				case Tile.YellowFlower: type = McBlock.Flower; break;
				case Tile.RedFlower: type = McBlock.Rose; break;
				case Tile.RedMushroom: type = McBlock.RedMushroom; break;
				case Tile.BrownMushroom: type = McBlock.BrownMushroom; break;
				case Tile.SolidGold: type = McBlock.Gold; break;
				case Tile.DarkGrey: type = McBlock.Obsidian; break;
			}
			setColor();
		}

		public fTile(McBlock til)
		{
			type = til;
			setColor();
		}

		private void setColor()
		{
			// TODO: Add more colors to this list. A lot of stuff is default grey ATM.
			switch (type)
			{
				case McBlock.Air:
					transparent = true;
					isAir = true;
					break;

				case McBlock.Dirt:
					Main = Color.FromArgb(147, 96, 0);
					Highlight = Color.Sienna;
					Shadow = Color.SaddleBrown;
					break;

				case McBlock.Grass:
					Main = Color.Green;
					Highlight = Color.Sienna;
					Shadow = Color.SaddleBrown;
					break;

				case McBlock.Rock:
					Main = Color.FromArgb(128, 128, 128);
					Highlight = Color.FromArgb(164, 164, 164);
					Shadow = Color.FromArgb(100, 100, 100);
					break;

				case McBlock.Cobblestone:
					Main = Color.FromArgb(90, 90, 90);
					Highlight = Color.FromArgb(124, 124, 124);
					Shadow = Color.FromArgb(64, 64, 64);
					break;

				case McBlock.Water:
					Main = Color.FromArgb(0, 171, 236);
					Highlight = Color.FromArgb(70, 204, 255);
					Shadow = Color.FromArgb(0, 123, 170);
					renderWater = true;
					transparent = true;
					break;

				case McBlock.StationaryWater:
					Main = Color.FromArgb(0, 171, 236);
					Highlight = Color.FromArgb(70, 204, 255);
					Shadow = Color.FromArgb(0, 123, 170);
					renderWater = true;
					transparent = true;
					break;

				case McBlock.Lava:
					Main = Color.FromArgb(255, 125, 0);
					Highlight = Color.FromArgb(255, 100, 0);
					Shadow = Color.FromArgb(170, 50, 0);
					break;

				case McBlock.StationaryLava:
					Main = Color.FromArgb(236, 50, 0);
					Highlight = Color.FromArgb(255, 100, 25);
					Shadow = Color.FromArgb(170, 0, 0);
					break;

				case McBlock.Glass:
					Main = Color.FromArgb(255, 255, 255);
					Highlight = Color.FromArgb(255, 255, 255);
					Shadow = Color.FromArgb(255, 255, 255);
					renderGlass = true;
					transparent = true;
					break;

				case McBlock.Gold:
					Main = Color.FromArgb(238, 237, 120);
					Highlight = Color.FromArgb(238, 225, 64);
					Shadow = Color.FromArgb(217, 202, 19);
					break;

				case McBlock.Sand:
					Main = Color.FromArgb(208, 206, 121);
					Highlight = Color.FromArgb(217, 213, 157);
					Shadow = Color.FromArgb(197, 190, 109);
					break;

				case McBlock.Gravel:
					Main = Color.FromArgb(158, 156, 71);
					Highlight = Color.FromArgb(117, 113, 57);
					Shadow = Color.FromArgb(97, 90, 09);
					break;

				case McBlock.Cloth1: // Red
					Main = Color.FromArgb(255, 0, 0);
					Highlight = Color.FromArgb(255, 50, 50);
					Shadow = Color.FromArgb(205, 0, 0);
					break;

				case McBlock.Cloth2: // Orange
					Main = Color.FromArgb(255, 128, 0);
					Highlight = Color.FromArgb(255, 178, 50);
					Shadow = Color.FromArgb(205, 78, 0);
					break;

				case McBlock.Cloth3: // Yellow
					Main = Color.FromArgb(255, 255, 0);
					Highlight = Color.FromArgb(255, 255, 50);
					Shadow = Color.FromArgb(205, 205, 0);
					break;

				case McBlock.Cloth4: // Light Green
					Main = Color.FromArgb(128, 255, 0);
					Highlight = Color.FromArgb(178, 255, 50);
					Shadow = Color.FromArgb(78, 205, 0);
					break;

				case McBlock.Cloth5: // Green
					Main = Color.FromArgb(0, 255, 0);
					Highlight = Color.FromArgb(50, 255, 50);
					Shadow = Color.FromArgb(0, 205, 0);
					break;

				case McBlock.Cloth6: // Aqua
					Main = Color.FromArgb(0, 255, 128);
					Highlight = Color.FromArgb(50, 255, 178);
					Shadow = Color.FromArgb(0, 205, 78);
					break;

				case McBlock.Cloth7: // Cyan
					Main = Color.FromArgb(0, 128, 255);
					Highlight = Color.FromArgb(50, 178, 255);
					Shadow = Color.FromArgb(0, 78, 205);
					break;

				case McBlock.Cloth8: // Blue
					Main = Color.FromArgb(0, 0, 255);
					Highlight = Color.FromArgb(50, 50, 205);
					Shadow = Color.FromArgb(0, 0, 205);
					break;

				case McBlock.Cloth9: // Indigo
					Main = Color.FromArgb(128, 0, 255);
					Highlight = Color.FromArgb(178, 50, 255);
					Shadow = Color.FromArgb(78, 0, 205);
					break;

				case McBlock.Cloth10: // Purple
					Main = Color.FromArgb(255, 0, 255);
					Highlight = Color.FromArgb(255, 50, 255);
					Shadow = Color.FromArgb(205, 0, 205);
					break;

				case McBlock.Adminium:
					Main = Color.FromArgb(50, 50, 50);
					Highlight = Color.FromArgb(100, 100, 100);
					Shadow = Color.FromArgb(25, 25, 25);
					break;

				case McBlock.Obsidian:
					Main = Color.FromArgb(65, 65, 65);
					Highlight = Color.FromArgb(115, 115, 115);
					Shadow = Color.FromArgb(40, 40, 40);
					break;

				case McBlock.TreeTrunk:
					Main = Color.FromArgb(147, 96, 0);
					Highlight = Color.Sienna;
					Shadow = Color.SaddleBrown;
					break;

				case McBlock.Leaves:
					Main = Color.FromArgb(0, 128, 0);
					Highlight = Color.FromArgb(50, 178, 0);
					Shadow = Color.FromArgb(0, 78, 0);
					break;

				case McBlock.Sponge:
					Main = Color.FromArgb(250, 250, 0);
					Highlight = Color.FromArgb(255, 255, 50);
					Shadow = Color.FromArgb(205, 205, 0);
					break;

				case McBlock.RedBrickTile:
					Main = Color.FromArgb(250, 0, 0);
					Highlight = Color.FromArgb(255, 50, 50);
					Shadow = Color.FromArgb(205, 0, 0);
					break; ;
			}
		}
	}
}
