using System;
using System.Threading;

namespace Maze_Gen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Maze mz = new Maze();

            mz.m_gen = new Kruskals(mz);
            //mz.m_gen = new Prims(mz);
            //mz.m_gen = new RecursiveBacktracking(mz);
            //mz.m_gen = new HuntAndKill(mz);

            Console.WriteLine(mz.m_gen.GetType().Name);

            int w, h;

            Console.Write("Width : ");
            w = Convert.ToInt32(Console.ReadLine());

            Console.Write("Height : ");
            h = Convert.ToInt32(Console.ReadLine());

            mz.Generate(w, h);

            Console.WindowWidth = w * 2 + 10;
            Console.WindowHeight = h * 2 + 5;

            foreach (string snapshot in mz.m_snapshots)
            {
                string draw = snapshot;

                draw = draw.Replace(Cell.State.Wall.ToString(), "█");
                draw = draw.Replace(Cell.State.None.ToString(), "░");
                draw = draw.Replace(",", "");

                Console.Clear();
                Console.WriteLine(draw);

                Thread.Sleep(20);
            }
        }
    }
}
