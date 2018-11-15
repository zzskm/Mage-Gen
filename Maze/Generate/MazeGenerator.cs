public class MazeGenerator
{
    public Grid m_grid;

    public virtual Grid Generate(int w, int h)
    {
        m_grid = new Grid(w, h);
        m_grid.Fill(0);

        return m_grid;
    }
}
