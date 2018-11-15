using System;

namespace Maze_Gen
{
    class Program
    {
        static void Main(string[] args)
        {
            int w, h;

            Console.Write("Width : ");
            w = Convert.ToInt32(Console.ReadLine());

            Console.Write("Height : ");
            h = Convert.ToInt32(Console.ReadLine());

            Maze mz = new Maze();

            mz.m_gen = new RecursiveBacktracking();
            //mz.m_gen = new HuntAndKill();

            mz.Generate(w, h);

            string draw = mz.m_grid.ToString();

            draw = draw.Replace("99", "█");
            draw = draw.Replace("0", "░");
            draw = draw.Replace(",", "");

            Console.OutputEncoding = System.Text.Encoding.UTF8; 
            Console.WriteLine(draw);
        }
    }
}
