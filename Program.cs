using System.Diagnostics;

namespace Snake_test
{
    internal class Program
    {
        static ConsoleKeyInfo KeyInfo = new ConsoleKeyInfo();
        static char Key;
        
        static void Main(string[] args)
        {
            int width = 20;
            int height = 20;
            MakeBoard(width, height);
            Input();
            WritePoint(4, 5);
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

        static void WritePoint(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("*");
        }

    }
}