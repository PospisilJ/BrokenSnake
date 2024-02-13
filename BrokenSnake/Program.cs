using System;
using System.Collections.Generic;

namespace SnakeGame
{
    class Program
    {
        public const int MaxTimeToReact = 500;
        public const int ScreenWidth = 32;
        public const int ScreenHeight = 16;

        static void Main(string[] args)
        {
            Console.WindowHeight = ScreenHeight;
            Console.WindowWidth = ScreenWidth;

            Random random = new Random();
            int score = 5;
            bool gameOver = false;

            Pixel snakeHead = new Pixel
            {
                XPos = ScreenWidth / 2,
                YPos = ScreenHeight / 2,
                Color = ConsoleColor.Red
            };

            Berry snakeBerry = new Berry();
            snakeBerry.GenerateNewBerry(random, ScreenWidth, ScreenHeight);

            List<Pixel> snakeBody = new List<Pixel>();

            string movement = "RIGHT";

            while (!gameOver)
            {
                Console.Clear();
                DrawBorder();
                Console.ForegroundColor = ConsoleColor.Green;

                if (snakeBerry.IsBerryEaten(snakeHead))
                {
                    score++;
                    snakeBerry.GenerateNewBerry(random, ScreenWidth, ScreenHeight);
                }

                foreach (Pixel bodyPart in snakeBody)
                {
                    if (bodyPart.XPos == snakeHead.XPos && bodyPart.YPos == snakeHead.YPos)
                    {
                        gameOver = true;
                        break;
                    }
                    Console.SetCursorPosition(bodyPart.XPos, bodyPart.YPos);
                    Console.Write("■");
                }

                if (gameOver)
                    break;

                Console.SetCursorPosition(snakeHead.XPos, snakeHead.YPos);
                Console.ForegroundColor = snakeHead.Color;
                Console.Write("■");

                Console.SetCursorPosition(snakeBerry.XPos, snakeBerry.YPos);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");


                if (snakeHead.XPos == ScreenWidth - 1 || snakeHead.XPos == 0 ||
                    snakeHead.YPos == ScreenHeight - 1 || snakeHead.YPos == 0)
                {
                    gameOver = true;
                    break;
                }

                DateTime startTime = DateTime.Now;

                while (true)
                {
                    DateTime currentTime = DateTime.Now;
                    if (currentTime.Subtract(startTime).TotalMilliseconds > MaxTimeToReact)
                        break;

                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.UpArrow && movement != "DOWN")
                            movement = "UP";
                        if (keyInfo.Key == ConsoleKey.DownArrow && movement != "UP")
                            movement = "DOWN";
                        if (keyInfo.Key == ConsoleKey.LeftArrow && movement != "RIGHT")
                            movement = "LEFT";
                        if (keyInfo.Key == ConsoleKey.RightArrow && movement != "LEFT")
                            movement = "RIGHT";
                    }
                }

                snakeBody.Add(new Pixel { XPos = snakeHead.XPos, YPos = snakeHead.YPos });

                switch (movement)
                {
                    case "UP":
                        snakeHead.YPos--;
                        break;
                    case "DOWN":
                        snakeHead.YPos++;
                        break;
                    case "LEFT":
                        snakeHead.XPos--;
                        break;
                    case "RIGHT":
                        snakeHead.XPos++;
                        break;
                }

                if (snakeBody.Count > score)
                    snakeBody.RemoveAt(0);
            }

            Console.SetCursorPosition(ScreenWidth / 5, ScreenHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(ScreenWidth / 5, ScreenHeight / 2 + 1);
        }


        private static void DrawBorder()
        {
            for (int i = 0; i < ScreenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");

                Console.SetCursorPosition(i, ScreenHeight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < ScreenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");

                Console.SetCursorPosition(ScreenWidth - 1, i);
                Console.Write("■");


            }
        }
    }

    class Pixel
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public ConsoleColor Color { get; set; }
    }

    class Berry
    {
        public int XPos { get; set; }
        public int YPos { get; set; }

        public void GenerateNewBerry(Random random, int screenWidth, int screenHeight)
        {
            XPos = random.Next(1, screenWidth);
            YPos = random.Next(1, screenHeight);
        }

        public bool IsBerryEaten(Pixel snakeHead)
        {
            return XPos == snakeHead.XPos && YPos == snakeHead.YPos;
        }
    }
}
