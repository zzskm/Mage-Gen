public class MazeGenerator
{
    public Maze m_owner;

    public Grid m_grid;

    public MazeGenerator(Maze owner)
    {
        m_owner = owner;
    }

    public virtual Grid Generate(int w, int h)
    {
        m_grid = new Grid(w, h);
        m_grid.Fill(0);

        m_owner.TakeSnapshot();

        return m_grid;
    }

    public virtual Grid Draw()
    {
        Grid grid = new Grid(m_grid.Width * 2 + 1, m_grid.Height * 2 + 1);
        grid.Fill(Cell.State.Wall);

        for (int i = 0; i < m_grid.Length; ++i)
        {
            Cell cell = m_grid.GetCell(i);

            cell.x = cell.x * 2 + 1;
            cell.y = cell.y * 2 + 1;

            grid.SetState(cell.x, cell.y, Cell.State.None);

            if ((cell.state & Cell.State.W) != 0)
            {
                grid.SetState(cell.x - 1, cell.y, Cell.State.None);
            }

            if ((cell.state & Cell.State.E) != 0)
            {
                grid.SetState(cell.x + 1, cell.y, Cell.State.None);
            }

            if ((cell.state & Cell.State.N) != 0)
            {
                grid.SetState(cell.x, cell.y - 1, Cell.State.None);
            }

            if ((cell.state & Cell.State.S) != 0)
            {
                grid.SetState(cell.x, cell.y + 1, Cell.State.None);
            }
        }

        return grid;
    }
}
