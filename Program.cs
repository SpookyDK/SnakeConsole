using System;

namespace Snake_test
{
    internal class Program
    {
        static ConsoleKeyInfo KeyInfo = new ConsoleKeyInfo();
        static char Key = 'd';
        static List<int> SnakeX = new List<int>();
        static List<int> SnakeY = new List<int>();

        static void Main(string[] args)
        {
            int width = 20;
            int height = 20;
            SnakeX.Add(4);
            SnakeY.Add(3);
            SnakeX.Add(3);
            SnakeY.Add(3);
            SnakeX.Add(2);
            SnakeY.Add(3);
            SnakeX.Add(2);
            SnakeY.Add(4);
            SnakeX.Add(2);
            SnakeY.Add(5);
            while (true)
            {
                MakeBoard(width, height);
                Logic();
                Console.ReadKey();
            }
           
            
            
            
            
            
           
        }
        static void MakeBoard(int width, int height)
        {

            Console.Clear();
            for (int i = 1; i < width; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("_");
            }
           


            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("|");
            }
            for (int i = 1; i < height; i++)
            {
                Console.SetCursorPosition(width, i);
                Console.Write("|");
            }
            for (int i = 1; i < width; i++)
            {
                Console.SetCursorPosition(i, height);
                Console.Write("_");
            }












        }
        static void Input()
        {
            if (Console.KeyAvailable)
            {
                KeyInfo = Console.ReadKey(true);
                Key = KeyInfo.KeyChar;
            }
        }

        
        static void Logic()
        {
            


            for (int i = SnakeX.Count; i > 1; i--)
            {
                SnakeX[i - 1] = SnakeX[i - 2];
            }
            for (int i = SnakeY.Count; i > 1; i--)
            {
                SnakeY[i - 1] = SnakeY[i - 2];
            }
            switch (Key)
            {
                case ('a'):
                    SnakeX[0]--;
                    break;
                case ('d'):
                    SnakeX[0]++;
                    break;
                case ('w'):
                    SnakeY[0]++;
                    break;
                case ('s'):
                    SnakeY[0]--;
                    break;
            }

            for (int i = 0; i < SnakeX.Count; i++)
            {
                Console.SetCursorPosition(SnakeX[i], SnakeY[i]);
                Console.Write("¤");
            }
        }

    }
}