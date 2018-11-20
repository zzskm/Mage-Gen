using System.Collections.Generic;

public class Maze
{
    public readonly List<string> m_snapshots = new List<string>();

    public MazeGenerator m_gen;

    public Grid m_grid;

    private Cell m_start;
    private Cell m_end;

    public void Generate(int w, int h)
    {
        m_snapshots.Clear();

        m_gen.Generate(w, h);
        m_grid = m_gen.Draw();
    }

    public void TakeSnapshot()
    {
        Grid grid = m_gen.Draw();
        m_snapshots.Add(grid.ToString());
    }
}
