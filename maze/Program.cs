using System;
using System.Drawing;

/*
 * Author: Won Kyu (Justin) Lee
 * Operating System: OS X Yosemite
 * IDE: Xamarin Studio Version. 5.5.4 (build 15)
 * Date: 12/4/14
 * Usage: maze.exe source destination
 * 
 * NOTES: This is a maze solving application that reads in the source image file and prints out
 * the solved version to the destination. The appilcation assumes red = start, blue = end, 
 * white = open path, black = walls. 
 * 
 * THE ALGORTHM
 * Using these assumptions, it locates the starting position by scanning from top left to 
 * bottom right. Then, it applies BFS with an additional data structure to remember the path
 * it came from till it reaches an end point. Once the end is located, the path stored in the
 * memory data structure will kick in and help draw the shortest path obtained in GREEN.
 */

namespace MazeSolver
{
	class MainClass
	{
		private const int ERROR_INVALID_COMMAND_LINE = 0x667;
		// Open file and send it off to the solver!
		public static void Main (string[] args)
		{
			if (args.Length != 2) {
				System.Console.WriteLine ("Error: There was a problem with the arguments!");
				System.Console.WriteLine ("Expected: maze.exe “source.[bmp,png,jpg]” “destination.[bmp,png,jpg]”");
				Environment.Exit (ERROR_INVALID_COMMAND_LINE);
			}
			try {
				// open the image
				Bitmap bmp = new Bitmap (args [0]);
				System.Console.WriteLine ("Opened Image:\t" + args [0]);

				// Let's do some maze solving
				Core solver = Core.getCore (bmp);
				System.Console.WriteLine ("Initialized maze solver core!");
				solver.solveMaze ();
				System.Console.WriteLine ("Solved the maze");

				System.Console.WriteLine("Writing To:\t" + args [1]);
				bmp.Save (args [1]);
			} catch(ArgumentException)
			{
				Console.WriteLine("There was an error. Check the path to the image file.");
			}
		}
	}
}