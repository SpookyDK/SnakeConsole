using System;
using System.Collections.Generic;
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
        static float frameTiming = 0.2f;
        static int waitFor;
        static int frames = 0;
        static float totalFrameTime = 0;
        static float frameTime = 0;
        static int fruitSpawnTime = 3333;           //in milliseconds
        static List<int> fruitX = new List<int>();
        static Queue<int> fruitXQ = new Queue<int>();
        static List<int> fruitY = new List<int>();
        static Queue<int> fruitYQ = new Queue<int>();
        static Random random;
        static bool alive = true;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            random = new Random();
            Thread randomFruitThread = new Thread(RandomFruitSpawn);
            randomFruitThread.Start();
            while (true)
            {
                Key = 'd';
                MakeBoard();
                SnakeX.Clear();
                SnakeY.Clear();
                fruitX.Clear();
                fruitY.Clear();
                SnakeX.Add(4);
                SnakeY.Add(3);
                while (alive)
                {
                    DateTime timeStampStart = DateTime.UtcNow;
                    CollisionCheck();
                    Input();
                    Logic();
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (fruitXQ.Count > 0)
                        WriteFruits();
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
                    Thread.Sleep(waitFor);
                }
                Console.WriteLine(waitFor + "ms");
                Console.WriteLine("THIS PROGRAM WAS CREATED BY");
                Console.Write("Esben & ");
                Console.WriteLine("David helped, i suppose");
                Console.WriteLine("");
                Console.WriteLine("Want to play again?");
                Console.WriteLine("Press any button to continue");
                Console.WriteLine("press ctrl+c to exit");
                Console.ReadKey();
                Console.Clear();
                alive = true;
            }
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
                    fruitXQ.Enqueue(tempFruitX);
                    fruitYQ.Enqueue(tempFruitY);
                    return;
                }
            }
        }
        static void RandomFruitSpawn()
        {
            while (true)
            {
                if (fruitX.Count < 5)
                    SpawnFruit();
                Thread.Sleep(fruitSpawnTime);           //you removed my randomizer, you bastard | Well fuck you, it was just not necessary. | fuck you again, no it was not "needed" but its called random ticks which makes good gameplay, loser.
            }
        }
        static void CollisionCheck()
        {
            if (SnakeX[0] < 2 || SnakeX[0] > width - 1)
                alive = false;
            if (SnakeY[0] < 2 || SnakeY[0] > height - 1)
                alive = false;
            for (int i = 1; i < SnakeX.Count - 1; i++)
            {
                if (SnakeX[0] == SnakeX[i] && SnakeY[0] == SnakeY[i])
                    alive = false;
            }
        }
        static void Logic()
        {
            SnakeX.Insert(1, SnakeX[0]);
            SnakeY.Insert(1, SnakeY[0]);
            Console.SetCursorPosition(SnakeX[1], SnakeY[1]);
            Console.Write("¤");
            Console.SetCursorPosition(SnakeX[SnakeX.Count - 1], SnakeY[SnakeY.Count - 1]);
            Console.Write(' ');
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
            Console.SetCursorPosition(SnakeX[0], SnakeY[0]);
            Console.Write("@");
            SnakeX.RemoveAt(SnakeX.Count - 1);
            SnakeY.RemoveAt(SnakeY.Count - 1);
            for (int i = 0; i < fruitX.Count; i++)
            {
                if (fruitX[i] == SnakeX[0])
                {
                    if (fruitY[i] == SnakeY[0])
                    {
                        AddTail();
                        Console.SetCursorPosition(fruitX[i], fruitY[i]);
                        Console.Write("¤");
                        fruitX.RemoveAt(i);
                        fruitY.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        static void WriteFruits()
        {
            while (fruitXQ.Count > 0)
            {
                Console.SetCursorPosition(fruitXQ.Peek(), fruitYQ.Peek());
                Console.Write("#");
                fruitX.Add(fruitXQ.Dequeue());
                fruitY.Add(fruitYQ.Dequeue());
            }
        }
    }
}