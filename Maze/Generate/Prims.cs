using System.Collections.Generic;

public class Prims : MazeGenerator
{
    private System.Random m_rnd;
    public readonly List<int> m_frontiers;

    public Prims()
    {
        m_rnd = new System.Random();
        m_frontiers = new List<int>();
    }

    public override Grid Generate(int w, int h)
    {
        base.Generate(w, h);

        m_frontiers.Clear();

        Mark(m_grid.GetRandom());

        while (m_frontiers.Count > 0)
        {
            int index = m_frontiers[m_rnd.Next(m_frontiers.Count)];
            m_frontiers.Remove(index);

            Cell curr = m_grid.GetCell(index);
            Cell next = m_grid.ChoiceNeighbor(curr, st => (st & Cell.State.Visited) != 0);

            if (next.IsValid())
            {
                Cell.State dir = Cell.GetDir(curr, next);

                m_grid.AddState(curr, dir);
                m_grid.AddState(next, Cell.ToOppositeDir(dir));
            }

            Mark(curr);
        }

        return m_grid;
    }

    public void Mark(Cell curr)
    {
        m_grid.AddState(curr, Cell.State.Visited);

        Cell[] neighbors = m_grid.FindNeighbors(curr, st => st == Cell.State.None);

        for (int i = 0; i < neighbors.Length; ++i)
        {
            int index = Cell.ToIndex(neighbors[i], m_grid.Width);

            if (!m_frontiers.Contains(index))
            {
                m_frontiers.Add(index);
            }
        }
    }
}
