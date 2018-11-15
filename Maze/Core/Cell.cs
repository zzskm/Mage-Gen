public struct Cell
{
    [System.Flags]
    public enum State
    {
        None = 0,

        Invalid = 1 << 0,

        // direction
        N = 1 << 1,
        S = 1 << 2,
        E = 1 << 3,
        W = 1 << 4,

        // state
        Visited = 1 << 5,
        Marked = 1 << 6,

        Wall = 1 << 9,
    }

    public static State GetDir(Cell curr, Cell next)
    {
        int x = next.x - curr.x;
        int y = next.y - curr.y;

        if (x == 0 && y == 0)
        {
            return State.None;
        }

        if (x == 0)
        {
            return y > 0 ? State.S : State.N;
        }
        else
        {
            return x > 0 ? State.E : State.W;
        }
    }

    public static State ToOppositeDir(State dir)
    {
        switch (dir)
        {
            case State.N: return State.S;
            case State.S: return State.N;
            case State.E: return State.W;
            case State.W: return State.E;

        }

        return State.None;
    }

    public static int ToIndex(Cell cell, int w)
    {
        return w * cell.y + cell.x;
    }

    public static int ToIndex(int x, int y, int w)
    {
        return w * y + x;
    }

    public static Cell FromIndex(int index, int w)
    {
        return new Cell(index % w, index / w, 0);
    }

    private static readonly Cell s_Invalid = new Cell(0, 0, State.Invalid);
    public static Cell invalid { get { return s_Invalid; } }

    private int m_x;
    private int m_y;

    private State m_state;

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

    public State state
    {
        get { return m_state; }
        set { m_state = value; }
    }

    public Cell(int x, int y, State value)
    {
        m_x = x;
        m_y = y;

        m_state = value;
    }

    public bool IsValid()
    {
        return (m_state & State.Invalid) == 0;
    }

    public override string ToString()
    {
        return string.Format("({0}, {1}) / {2}", x, y, state);
    }
}
