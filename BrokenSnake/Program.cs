using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
///█ ■
////https://www.youtube.com/watch?v=SGZgvMwjq2U
namespace Snake
{
	class Program
	{
		public static int maxTimeToReact = 500;
		public static int screenwidth = 16;
		public static int screenheight = 8;

		static void Main(string[] args)
		{
			Console.WindowHeight = screenheight;
			Console.WindowWidth = screenwidth;



			Random randomNumber = new Random();
			int score = 5;
			bool gameover = false;

			pixel snakeHead = new pixel();
			berry snakeBerry = new berry();

			snakeHead.xpos = screenwidth / 2;
			snakeHead.ypos = screenheight / 2;

			snakeHead.color = ConsoleColor.Red;

			string movement = "RIGHT";



			List<Tuple<int, int>> snakeBody = new List<Tuple<int, int>>();

			generateNewBerry(snakeBerry);

			DateTime timeAtStart = DateTime.Now;
			DateTime timeCurrent = DateTime.Now;

			while (true)
			{
				Console.Clear();
				
				if (snakeHead.xpos == screenwidth - 1 || snakeHead.xpos == 0 || snakeHead.ypos == screenheight - 1 || snakeHead.ypos == 0)
				{
					gameover = true;
				}
				for (int i = 0; i < screenwidth; i++)
				{
					drawPixel(i, 0);
					drawPixel(i, screenheight - 1);

				}

				for (int i = 0; i < screenheight; i++)
				{
					drawPixel(0, i); //-------------------------------------------------------------

					drawPixel(screenwidth- 1, i);

					//Console.SetCursorPosition(screenwidth - 1, i);
					//Console.Write("■");
				}

				Console.ForegroundColor = ConsoleColor.Green;

				if (snakeBerry.xpos == snakeHead.xpos && snakeBerry.ypos == snakeHead.ypos)
				{
					score++;
					generateNewBerry(snakeBerry);
				}

				for (int i = 0; i < snakeBody.Count(); i++)
				{
					drawPixel(snakeBody[i].Item1, snakeBody[i].Item2);

					//Console.SetCursorPosition(snakeBody[i].Item1, snakeBody[i].Item2);
					//Console.Write("■");

					gameover = checkCollision(snakeHead, snakeBody[i]);
				}


				if (gameover)
				{
					break;
				}

				Console.SetCursorPosition(snakeHead.xpos, snakeHead.ypos);
				Console.ForegroundColor = snakeHead.color;
				Console.Write("■");
				Console.SetCursorPosition(snakeBerry.xpos, snakeBerry.ypos);
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write("■");
				timeAtStart = DateTime.Now;

				while (true)
				{
					timeCurrent = DateTime.Now;
					if (timeCurrent.Subtract(timeAtStart).TotalMilliseconds > maxTimeToReact) { break; }
					if (Console.KeyAvailable)
					{
						ConsoleKeyInfo toets = Console.ReadKey(true);
						//Console.WriteLine(toets.Key.ToString());
						if (toets.Key.Equals(ConsoleKey.UpArrow) && movement != "DOWN")
						{
							movement = "UP";
						}
						if (toets.Key.Equals(ConsoleKey.DownArrow) && movement != "UP")
						{
							movement = "DOWN";
						}
						if (toets.Key.Equals(ConsoleKey.LeftArrow) && movement != "RIGHT")
						{
							movement = "LEFT";
						}
						if (toets.Key.Equals(ConsoleKey.RightArrow) && movement != "LEFT")
						{
							movement = "RIGHT";
						}
					}
				}
				snakeBody.Add(new Tuple<int, int>(snakeHead.xpos, snakeHead.ypos));
				switch (movement)
				{
					case "UP":
						snakeHead.ypos--;
						break;
					case "DOWN":
						snakeHead.ypos++;
						break;
					case "LEFT":
						snakeHead.xpos--;
						break;
					case "RIGHT":
						snakeHead.xpos++;
						break;
				}
				if (snakeBody.Count() > score)
				{
					snakeBody.RemoveAt(0);
				}
			}
			Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
			Console.WriteLine("Game over, Score: " + score);
			Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);
		}

		private static void drawPixel(int x, int y)
		{
			Console.SetCursorPosition(x, y);
			Console.Write("■");
		}

		private static bool checkCollision(pixel snakeHead, Tuple<int, int> snakeBodyPosition)
		{
			if (snakeBodyPosition.Item1 == snakeHead.xpos && snakeBodyPosition.Item2 == snakeHead.ypos)
			{
				return true;
			}

			return false;
		}

		private static void generateNewBerry(berry snakeBerry)
		{
			Random randomnummer = new Random();
			snakeBerry.xpos = randomnummer.Next(1, screenwidth);
			snakeBerry.ypos = randomnummer.Next(1, screenheight);
		}

		class pixel
		{
			public int xpos { get; set; }
			public int ypos { get; set; }
			public ConsoleColor color { get; set; }
		}

		class berry
		{
			public int xpos { set; get; }
			public int ypos { set; get; }
		}


	}
}
//¦