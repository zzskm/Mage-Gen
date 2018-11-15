public class Maze
{
    public MazeGenerator m_gen;

    public Grid m_grid;

    private Cell m_start;
    private Cell m_end;

    public void Generate(int w, int h)
    {
        Grid grid = m_gen.Generate(w, h);

        m_grid = new Grid(grid.Width * 2 + 1, grid.Height * 2 + 1);
        m_grid.Fill(Cell.State.Wall);

        for (int i = 0; i < grid.Length; ++i)
        {
            Cell cell = grid.GetCell(i);

            cell.x = cell.x * 2 + 1;
            cell.y = cell.y * 2 + 1;

            m_grid.SetState(cell.x, cell.y, Cell.State.None);

            if ((cell.state & Cell.State.W) != 0)
            {
                m_grid.SetState(cell.x - 1, cell.y, Cell.State.None);
            }

            if ((cell.state & Cell.State.E) != 0)
            {
                m_grid.SetState(cell.x + 1, cell.y, Cell.State.None);
            }

            if ((cell.state & Cell.State.N) != 0)
            {
                m_grid.SetState(cell.x, cell.y - 1, Cell.State.None);
            }

            if ((cell.state & Cell.State.S) != 0)
            {
                m_grid.SetState(cell.x, cell.y + 1, Cell.State.None);
            }
        }
    }
}
