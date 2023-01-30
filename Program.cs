using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;


namespace SnakeConsole
{
    internal class Program
    {
        static ConsoleKeyInfo KeyInfo = new ConsoleKeyInfo();
        static char Key = 'd';
        static List<int> SnakeX = new List<int>();
        static List<int> SnakeY = new List<int>();
        static int width = 20;
        static int height = 20;
        static float frameTiming = 0.33f;
        static int waitFor;
        static float elapsedTime;
        static int frames = 0;
        static float avgFrameTime = 0;
        static float totalFrameTime = 0;
        static float frameTime = 0;

        static int randomizer = 5;                  //in percentage
        static int fruitSpawnTime = 2000;           //in milliseconds
        static List<int> fruitX = new List<int>();
        static List<int> fruitY = new List<int>();
        static Random random;
        static bool alive = true;
        static void Main(string[] args)
        {

            Console.CursorVisible = false;

            totalFrameTime = 0;

            random = new Random();
            MakeBoard();

            SnakeX.Add(4);
            SnakeY.Add(3);
            SpawnFruit();
            Thread randomFruitThread = new Thread(RandomFruitSpawn);
            randomFruitThread.Start();

            while (alive)
            {

                DateTime timeStampStart = DateTime.UtcNow;




                Input();
                Logic();
                CollisionCheck();
                Console.SetCursorPosition(fruitX[fruitX.Count-1], fruitY[fruitY.Count - 1]);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("#");
                Console.ForegroundColor = ConsoleColor.Green;

                Console.SetCursorPosition(0, 0);
                Console.WriteLine(SnakeX.Count);
                //tids stuffd
                frames++;
                frameTime = (float)(DateTime.UtcNow - timeStampStart).TotalMilliseconds;

                totalFrameTime += frameTime;

                Console.SetCursorPosition(0, height + 2);
                Console.WriteLine(frameTime + "ms");
                Console.WriteLine("totalFrameTime " + totalFrameTime);
                Console.WriteLine("avg " + totalFrameTime / frames + "ms");

                waitFor = (int)(frameTiming * 1000 - frameTime);
                System.Threading.Thread.Sleep(waitFor);
            }


            Console.WriteLine(waitFor + "ms");


            Console.WriteLine("THIS PROGRAM WAS CREATED BY");
            Console.Write("Esben & ");
            Console.WriteLine("David helped, i suppose");


        }


        static void MakeBoard()
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            for (int i = 2; i < width; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("_");
                Console.SetCursorPosition(i, height);
                Console.Write("_");
            }



            for (int i = 2; i < height + 1; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("|");
                Console.SetCursorPosition(width, i);
                Console.Write("|");
            }
            Console.ForegroundColor = ConsoleColor.Green;


        }
        static void Input()
        {
            if (Console.KeyAvailable)
            {
                KeyInfo = Console.ReadKey(true);
                Key = KeyInfo.KeyChar;
            }
        }

        static void AddTail()
        {
            SnakeX.Add(SnakeX.Count - 1);
            SnakeY.Add(SnakeY.Count - 1);

        }

        static void SpawnFruit()
        {
            while (true)
            {
                int tempFruitX = random.Next(2, width);
                int tempFruitY = random.Next(2, height);
                if (!SnakeX.Contains(tempFruitX) && !SnakeY.Contains(tempFruitY))
                {
                   
                    fruitX.Add(tempFruitX);
                    fruitY.Add(tempFruitY);
                    return;
                }
            }
        }
        static void RandomFruitSpawn()
        {
            int plusMinus = random.Next(100 - randomizer, 100 + randomizer);
            while (alive)
            {
                if (fruitX.Count < 5)
                {
                    SpawnFruit();
                }
                Thread.Sleep(fruitSpawnTime * (plusMinus / 100));
            }

        }
        static void CollisionCheck()
        {
            if (SnakeX[0] < 2 || SnakeX[0] > width - 1)
            {
                alive = false;

            }
            if (SnakeY[0] < 2 || SnakeY[0] > height - 1)
            {
                alive = false;
            }
            for (int i = 1; i < SnakeX.Count - 1; i++)
            {
                if (SnakeX[0] == SnakeX[i] && SnakeY[0] == SnakeY[i])
                {
                    alive = false;
                }
            }
        }


        static void Logic()
        {


            Console.SetCursorPosition(SnakeX[SnakeX.Count - 1], SnakeY[SnakeY.Count - 1]);
            Console.Write(' ');
            for (int i = 0; i < fruitX.Count; i++)
            {
                if (fruitX[i] == SnakeX[0])
                {
                    if (fruitY[i] == SnakeY[0])
                    {
                        AddTail();
                        fruitX.RemoveAt(i);
                        fruitY.RemoveAt(i);
                        break;
                    }
                }
            }
            //Moves the snake. Starts from the back and moves everypoint to the coordinates of the next point.

            for (int i = SnakeX.Count; i > 1; i--)
            {
                SnakeX[i - 1] = SnakeX[i - 2];
            }
            for (int i = SnakeY.Count; i > 1; i--)
            {
                SnakeY[i - 1] = SnakeY[i - 2];
            }

            //Finds out which way the front of the snake should go
            switch (Key)
            {
                case ('a'):
                    SnakeX[0]--;
                    break;
                case ('d'):
                    SnakeX[0]++;
                    break;
                case ('w'):
                    SnakeY[0]--;
                    break;
                case ('s'):
                    SnakeY[0]++;
                    break;
            }

            //Writes the actual snake position 
            /*for (int i = 0; i < SnakeX.Count; i++)
            {
                Console.SetCursorPosition(SnakeX[i], SnakeY[i]);
                Console.Write("¤");
            }
            */
            Console.SetCursorPosition(SnakeX[0], SnakeY[0]);
            Console.Write("¤");
        }


    }
}
