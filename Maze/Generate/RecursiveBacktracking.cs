public class RecursiveBacktracking : MazeGenerator
{
    public RecursiveBacktracking(Maze owner) : base(owner)
    {
    }

    public override Grid Generate(int w, int h)
    {
        base.Generate(w, h);

        CarvePassages(m_grid.GetRandom());

        m_owner.TakeSnapshot();

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

                Cell.State dir = Cell.GetDir(curr, next);

                m_grid.AddState(curr, dir);
                m_grid.AddState(next, Cell.ToOppositeDir(dir));

                m_owner.TakeSnapshot();

                CarvePassages(next);
            }
        }
    }
}
