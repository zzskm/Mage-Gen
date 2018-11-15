public class RecursiveBacktracking : MazeGenerator
{
    public override Grid Generate(int w, int h)
    {
        base.Generate(w, h);

        CarvePassages(m_grid.GetRandom());
        return m_grid;
    }

    public void CarvePassages(Cell curr)
    {
        Cell[] neighbors = m_grid.GetNeighbors(curr);

        if (neighbors.Length > 0)
        {
            for (int i = 0; i < neighbors.Length; ++i)
            {
                Cell next = neighbors[i];

                if (m_grid[next] != 0)
                {
                    continue;
                }

                Cell.Dir dir = Cell.GetDir(curr, next);

                m_grid.SetDir(curr, dir);
                m_grid.SetReverseDir(next, dir);

                CarvePassages(next);
            }
        }
    }
}
