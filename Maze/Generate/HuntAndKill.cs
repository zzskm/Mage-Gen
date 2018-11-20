public class HuntAndKill : MazeGenerator
{
    public HuntAndKill(Maze owner) : base(owner)
    {
    }

    public override Grid Generate(int w, int h)
    {
        base.Generate(w, h);

        Cell curr = m_grid.GetRandom();

        while (curr.IsValid())
        {
            Walk(curr);
            curr = Hunt();
        }

        m_owner.TakeSnapshot();

        return m_grid;
    }

    public void Walk(Cell curr)
    {
        Cell next = m_grid.ChoiceNeighbor(curr);

        while (next.IsValid())
        {
            Cell.State dir = Cell.GetDir(curr, next);

            m_grid.AddState(curr, dir);
            m_grid.AddState(next, Cell.ToOppositeDir(dir));

            curr = next;
            next = m_grid.ChoiceNeighbor(curr);

            m_owner.TakeSnapshot();
        }
    }

    public Cell Hunt()
    {
        for (int i = 0; i < m_grid.Length; ++i)
        {
            Cell curr = m_grid.GetCell(i);

            if (m_grid[i] != 0)
            {
                continue;
            }

            Cell next = m_grid.ChoiceNeighbor(curr, st => st != Cell.State.None);

            if (!next.IsValid())
            {
                continue;
            }

            Cell.State dir = Cell.GetDir(curr, next);

            m_grid.AddState(curr, dir);
            m_grid.AddState(next, Cell.ToOppositeDir(dir));

            curr.state = m_grid[curr];

            m_owner.TakeSnapshot();

            return curr;
        }

        return Cell.invalid;
    }
}
