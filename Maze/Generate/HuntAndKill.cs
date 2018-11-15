public class HuntAndKill : MazeGenerator
{
    public override Grid Generate(int w, int h)
    {
        base.Generate(w, h);

        Cell curr = m_grid.GetRandom();

        while (curr.IsValid())
        {
            Walk(curr);
            curr = Hunt();
        }

        return m_grid;
    }

    public void Walk(Cell curr)
    {
        Cell next = m_grid.ChoiceNeighbor(curr, 0);

        while (next.IsValid())
        {
            Cell.Dir dir = Cell.GetDir(curr, next);

            m_grid.SetDir(curr, dir);
            m_grid.SetReverseDir(next, dir);

            curr = next;
            next = m_grid.ChoiceNeighbor(curr, 0);
        }
    }

    public Cell Hunt()
    {
        for (int i = 0; i < m_grid.Length; ++i)
        {
            Cell origin = m_grid.GetCell(i);

            if (m_grid[i] != 0)
            {
                continue;
            }


            Cell neighbor = m_grid.ChoiceNeighbor(origin, val => val != 0);

            if (!neighbor.IsValid())
            {
                continue;
            }

            Cell.Dir dir = Cell.GetDir(origin, neighbor);

            m_grid.SetDir(origin, dir);
            m_grid.SetReverseDir(neighbor, dir);

            origin.value = m_grid[origin];

            return origin;
        }

        return Cell.invalid;
    }
}
