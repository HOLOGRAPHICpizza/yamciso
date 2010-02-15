using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YaMcIso
{
	internal static class CommandConfig
	{
		public static string MapFile = "";
		public static string Output = "";

		public static int ImgWidth = -1;

		static CommandConfig()
		{
			ResetConfig();
		}

		public static void ResetConfig()
		{
			MapFile = "";
			Output = "";
			ImgWidth = -1;
		}

		public static void LoadConfig(string[] args)
		{
			for (int idx = 0; idx < args.Length; idx++)
			{
				if (args[idx].StartsWith("-"))
				{
					switch (args[idx].ToLower())
					{
						case "-map":
							if (args.Length < idx + 1 || args[idx + 1].StartsWith("-"))
								throw new ArgumentException("Please specify a map name to use.");

							MapFile = args[++idx];
							break;
						case "-out":
							if (args.Length < idx + 1 || args[idx + 1].StartsWith("-"))
								throw new ArgumentException("Please specify a file to save to.");

							Output = args[++idx];
							break;
						case "-width":
							try
							{
								ImgWidth = Convert.ToInt16(args[++idx]);
							}
							catch
							{
								throw new ArgumentException("Invalid image size.");
							}
							break;
					}
				}
			}
		}

		public static string Usage()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("\nApplication Parameters:\n");

			sb.Append("-map\t\tThe Minecraft dat file to render.\n\n");

			sb.Append("-out\t\tFilename of the png to save.\n\n");

			sb.Append("-width\t\t(Optional) Output size of image.");

			return sb.ToString();
		}

		public static void ValidateConfig()
		{
			if (MapFile.Length < 1)
			{
				throw new ArgumentException("Please specify a level to load.");
			}

			if (Output.Length < 1)
			{
				throw new ArgumentException("Please specify the name of the png to save.");
			}
		}
	}
}
