// gLevel
// Global Level Type

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using com.mojang.minecraft.level;
using LibMinecraft;

namespace YaMcIso
{
	class gLevel
	{
		public int height, length, width;
		private fTile[, ,] tiles;

		public gLevel(String filename)
		{
			if (Path.GetExtension(filename) == ".dat")
			{
				datLoad(filename);
			}
			else if (Path.GetExtension(filename) == ".mclevel")
			{
				nbtLoad(filename);
			}
			else
			{
				throw new ArgumentException("Not a recognised file extension.");
			}
		}

		private void datLoad(String filename)
		{
			Level lvl = Level.Load(filename);

			width = lvl.width;
			length = lvl.depth;
			height = lvl.height;

			tiles = new fTile[width, length, height];
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < length; y++)
				{
					for (int z = 0; z < height; z++)
					{
						tiles[x, y, z] = new fTile(lvl.GetTile(x,y,z));
					}
				}

				Console.WriteLine("Loaded " + (x + 1) + "/" + width);
			}
		}

		private void nbtLoad(String filename)
		{
			McLevel lvl = new McLevel();
			lvl.LoadFile(filename);

			// This is extremely confusing, but it works.
			width = lvl.Map.Length;
			length = lvl.Map.Height;
			height = lvl.Map.Width;

			tiles = new fTile[width, length, height];
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < length; y++)
				{
					for (int z = 0; z < height; z++)
					{
						tiles[x, y, z] = new fTile((McBlock)lvl.Map.Blocks[x, y, z]);
					}
				}

				Console.WriteLine("Loaded " + (x + 1) + "/" + width);
			}
		}

		public fTile getTile(int x, int y, int z)
		{
			if (x < width && y < length && z < height)
				return tiles[x, y, z];
			else
				return new fTile();
		}
	}
}
