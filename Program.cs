using System;
using System.Threading;

namespace Maze_Gen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Select one of the algorithms:");
            Console.WriteLine();

            Console.WriteLine("1. Hunt-and-Kill");
            Console.WriteLine("2. Recursive Backtracking");
            Console.WriteLine("3. Prim's Algorithm");
            Console.WriteLine("4. Kruskal's Algorithm");

            Console.WriteLine();
            Console.Write("> Enter your selection (1-4): ");

            int sel = Convert.ToInt32(Console.ReadLine());

            Console.Write("> Width : ");
            int w = Convert.ToInt32(Console.ReadLine());

            Console.Write("> Height : ");
            int h = Convert.ToInt32(Console.ReadLine());

            //Console.Write("> Fps : ");
            //int fps = Convert.ToInt32(Console.ReadLine());
            //int interval = 1000 / fps;
            int interval = 1000 / 360;

            Maze mz = new Maze();

            switch (sel)
            {
                case 1: mz.m_gen = new HuntAndKill(mz); break;
                case 2: mz.m_gen = new RecursiveBacktracking(mz); break;
                case 3: mz.m_gen = new Prims(mz); break;
                case 4: mz.m_gen = new Kruskals(mz); break;

                default:
                    Console.WriteLine("Invalid Selection!");
                    return;
            }

            int screenWidth = w * 2 + 5;
            int screenHeight = h * 2 + 5;

            if(Console.WindowWidth< screenWidth)
            {
                Console.WindowWidth = screenWidth;
            }

            if (Console.WindowHeight < screenHeight)
            {
                Console.WindowHeight = screenHeight;
            }

            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine(mz.m_gen.GetType().Name + " [" + w + ", " + h + "]");

            mz.Generate(w, h);

            foreach (string snapshot in mz.m_snapshots)
            {
                DrawMaze(snapshot);
                Thread.Sleep(interval);
            }
        }

        static void DrawMaze(string draw)
        {
            draw = draw.Replace(Cell.State.Wall.ToString(), "█");
            draw = draw.Replace(Cell.State.Curr.ToString(), "▓");
            draw = draw.Replace(Cell.State.None.ToString(), "⠀");
            draw = draw.Replace(",", "");

            Console.SetCursorPosition(0, 0);

            Console.WriteLine();
            Console.WriteLine(draw);
        }
    }
}
