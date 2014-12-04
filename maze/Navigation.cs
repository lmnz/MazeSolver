using System;
using System.Drawing;

namespace MazeSolver
{
	public class PositionLocator
	{
		public const String END = "ff0000ff";
		public const String START = "ffff0000";
		public const String WALL = "ff000000";
		public const String OPEN = "ffffffff";

		// As the name indicates, this grabs the first instance of given color.
			// Mainly used to find the start position.
		public static Position getColorLocation(Bitmap maze, String COLOR) {
			MazeSolver.Position pos;
			String color;
			int h = maze.Height, w = maze.Width;
			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					color = maze.GetPixel (i, j).Name;
					if (color == COLOR) {
						pos = new Position (i, j);
						return pos;
					}
				}
			}
			return null;
		}
	}

	// Small abstraction for x and y coordinates to keep things a bit more organized.
	public class Position
	{
		private int x;
		private int y;

		public Position(int x, int y) {
			setX (x);
			setY (y);
		}

		public void setX(int new_x) {
			x = new_x;
		}
		public void setY(int new_y) {
			y = new_y;
		}
		public int getX() {
			return x;
		}
		public int getY() {
			return y;
		}

		// This is for my sanity
		public override String ToString() {
			return "(" + x + ", " + y + ")";
		}
	}
}

