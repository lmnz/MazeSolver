using System;
using System.Drawing;
using System.Collections.Generic;

namespace MazeSolver
{
	// This class will do the solving part, singleton since we only need one solver engine
	// Algorithm of choice will be BFS
	public class Core
	{
		// variables to be used by the algorithm!
		private static Core instance = null;
		private static Position start_pos, end_pos;
		private static Status[,] visitStatus = null;
		private static int W;
		private static int H;
		private static Bitmap maze;
		// used to draw the green line later
		private static Position[,] pathMemory;

		private static List<Position> QUEUE = new List<Position>();

		enum Status {UNVISITED, VISITING, VISITED};

		// Constructors and singleton grabber
		private Core () {}
		private Core (Bitmap maze)
		{
			// Initialize starting conditions for BFS to start!
			Core.maze = maze;
			W = maze.Width;
			H = maze.Height;
			visitStatus = new Status[W, H];
			pathMemory = new Position[W, H];
			initializeVisitStatus ();
			start_pos = PositionLocator.getColorLocation (maze, PositionLocator.START);
			queue (start_pos);
			visitStatus [start_pos.getX (), start_pos.getY ()] = Status.VISITING;
			pathMemory [start_pos.getX (), start_pos.getY ()] = start_pos;
		}

		public static Core getCore(Bitmap maze) {
			if (instance == null) {
				instance = new Core (maze);
			}
			return instance;
		}

		// Aux method to initialize our 'visited' map of the image
		private void initializeVisitStatus() {
			for (int i = 0; i < W; i++) {
				for (int j = 0; j < H; j++) {
					visitStatus [i, j] = Status.UNVISITED;
				}
			}
		}

		// our maze solver
		public void solveMaze() {
			Position whee;
			int x, y;
			// LETS DO SOME BFS
			while (QUEUE.Count > 0) {
				whee = pop ();
				x = whee.getX ();
				y = whee.getY ();
				visitStatus [x, y] = Status.VISITED;

				if (maze.GetPixel (x, y).Name == PositionLocator.END) {
					end_pos = whee;
					break;
				}
				if (checkValidPos(x + 1, y)) {
					handlePosition (x + 1, y, whee);
				}
				if (checkValidPos(x, y + 1)) {
					handlePosition (x, y + 1, whee);
				}
				if (checkValidPos(x - 1, y)) {
					handlePosition (x - 1, y, whee);
				}
				if (checkValidPos(x, y - 1)) {
					handlePosition (x, y - 1, whee);
				}
			}
			drawPath ();
		}

		// backtrack and draw the green line!
		private void drawPath () {
			Position current = end_pos;
			int cx, cy;
			while (current != start_pos) {
				cx = current.getX ();
				cy = current.getY ();
				maze.SetPixel(cx, cy, Color.Green);
				current = pathMemory [cx, cy];
			}
		}

		// quick check to see if we're in a fresh spot AND not at a wall
		private Boolean checkValidPos(int x, int y) {
			return (visitStatus [x, y] == Status.UNVISITED && maze.GetPixel (x, y).Name != PositionLocator.WALL);
		}

		// mark position as currently visiting, add the position to the queue, and remember where we came from!
		private void handlePosition (int x, int y, Position parent) {
			visitStatus [x, y] = Status.VISITING;
			queue (new Position (x, y));
			pathMemory [x, y] = parent;
		}

		// Aux methods. Renaming QUEUE functionality for code readability!
		private void queue(Position queued) {
			QUEUE.Add (queued);
		}
		private Position pop() {
			Position popped = QUEUE[0];
			QUEUE.RemoveAt (0);
			return popped;
		}
	}
}

