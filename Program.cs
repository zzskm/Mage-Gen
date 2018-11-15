using System;

namespace Maze_Gen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Maze mz = new Maze();

            mz.m_gen = new Prims();
            //mz.m_gen = new RecursiveBacktracking();
            //mz.m_gen = new HuntAndKill();

            Console.WriteLine(mz.m_gen.GetType().Name);

            int w, h;

            Console.Write("Width : ");
            w = Convert.ToInt32(Console.ReadLine());

            Console.Write("Height : ");
            h = Convert.ToInt32(Console.ReadLine());

            mz.Generate(w, h);

            string draw = mz.m_grid.ToString();

            draw = draw.Replace(Cell.State.Wall.ToString(), "█");
            draw = draw.Replace(Cell.State.None.ToString(), "░");
            draw = draw.Replace(",", "");

            Console.WriteLine(draw);
        }
    }
}
