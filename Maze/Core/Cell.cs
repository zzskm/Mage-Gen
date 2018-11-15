public struct Cell
{
    public enum Dir
    {
        None = 0,

        N = 1 << 0,
        S = 1 << 1,
        E = 1 << 2,
        W = 1 << 3,
    }

    public static Dir GetDir(Cell curr, Cell next)
    {
        int x = next.x - curr.x;
        int y = next.y - curr.y;

        if (x == 0 && y == 0)
        {
            return Dir.None;
        }

        if (x == 0)
        {
            return y > 0 ? Dir.S : Dir.N;
        }
        else
        {
            return x > 0 ? Dir.E : Dir.W;
        }
    }

    public static int ToIndex(int x, int y, int w)
    {
        return w * y + x;
    }

    public static Cell FromIndex(int index, int w)
    {
        return new Cell(index % w, index / w, 0);
    }

    private static readonly Cell s_Invalid = new Cell(0, 0, -1);
    public static Cell invalid { get { return s_Invalid; } }

    private int m_x;
    private int m_y;

    private int m_value;

    public int x
    {
        get { return m_x; }
        set { m_x = value; }
    }

    public int y
    {
        get { return m_y; }
        set { m_y = value; }
    }

    public int value
    {
        get { return m_value; }
        set { m_value = value; }
    }

    public Cell(int x, int y, int value)
    {
        m_x = x;
        m_y = y;

        m_value = value;
    }

    public bool IsValid()
    {
        return m_value > -1;
    }

    public override string ToString()
    {
        return string.Format("({0}, {1}) / {2}", x, y, value);
    }
}
