using System;
using System.Timers;
using System.Threading.Tasks;
namespace EsbensLort
{
    internal class Program
    {
        static ConsoleKeyInfo KeyInfo = new ConsoleKeyInfo();
        static char Key = 'd';
        static List<int> SnakeX = new List<int>();
        static List<int> SnakeY = new List<int>();
        static int width = 20;
        static int height = 20;

        static void Main(string[] args)
        {
            Int64 timestamp = Convert.ToInt64(DateTime.Now.ToFileTime());


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
                if (SnakeX[0] < 2 || SnakeX[0] > width - 1)
                {
                    break;

                }
                if (SnakeY[0] < 2 || SnakeY[0] > height - 2)
                {
                    break;
                }
                if (Convert.ToInt64(DateTime.Now.ToFileTime()) - timestamp > 5000000)
                {
                    MakeBoard();
                    Input();
                    Logic();

                    timestamp = Convert.ToInt64(DateTime.Now.ToFileTime());
                }
                Console.SetCursorPosition(0, height + 2);
            }








        }


        static void MakeBoard()
        {

            Console.Clear();
            for (int i = 1; i < width; i++)
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
                    SnakeY[0]--;
                    break;
                case ('s'):
                    SnakeY[0]++;
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
