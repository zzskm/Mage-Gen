using System.Collections.Generic;

public class Kruskals : MazeGenerator
{
    private System.Random m_rnd;
    private readonly List<Cell> m_edges;

    private int[] m_sets;

    public Kruskals()
    {
        m_rnd = new System.Random();
        m_edges = new List<Cell>();
    }

    public override Grid Generate(int w, int h)
    {
        base.Generate(w, h);

        m_edges.Clear();

        m_sets = new int[w * h];

        for (int y = 0; y < h; ++y)
        {
            for (int x = 0; x < w; ++x)
            {
                int index = Cell.ToIndex(x, y, w);
                m_sets[index] = index;

                if (y > 0)
                {
                    m_edges.Add(new Cell(x, y, Cell.State.N));
                }

                if (x > 0)
                {
                    m_edges.Add(new Cell(x, y, Cell.State.W));
                }
            }
        }

        // shuffle
        for (int i = 0, count = m_edges.Count; i < count; ++i)
        {
            int index = m_rnd.Next(count - i);

            Cell temp = m_edges[i];
            m_edges[i] = m_edges[index];
            m_edges[index] = temp;
        }

        while (m_edges.Count > 0)
        {
            Cell curr = m_edges[0];
            Cell next = Cell.invalid;

            switch (curr.state)
            {
                case Cell.State.N:
                    next = new Cell(curr.x, curr.y - 1, Cell.State.None);
                    break;

                case Cell.State.W:
                    next = new Cell(curr.x - 1, curr.y, Cell.State.None);
                    break;
            }

            int cSetId = m_sets[Cell.ToIndex(curr, w)];
            int nSetId = m_sets[Cell.ToIndex(next, w)];

            m_edges.RemoveAt(0);

            if (cSetId != nSetId)
            {
                // combine sets
                for (int i = 0; i < m_sets.Length; ++i)
                {
                    if (m_sets[i] == nSetId)
                    {
                        m_sets[i] = cSetId;
                    }
                }

                Cell.State dir = Cell.GetDir(curr, next);

                m_grid.AddState(curr, dir);
                m_grid.AddState(next, Cell.ToOppositeDir(dir));
            }
        }

        return m_grid;
    }
}
