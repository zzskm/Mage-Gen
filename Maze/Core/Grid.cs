using System.Collections.Generic;

public class Grid
{
    public int Length { get; protected set; }

    public int Width { get; protected set; }
    public int Height { get; protected set; }

    private System.Random m_rnd;

    private Cell.State[] m_grid;

    public Cell.State this[int index]
    {
        get { return m_grid[index]; }
        set { m_grid[index] = value; }
    }

    public Cell.State this[int x, int y]
    {
        get { return GetState(x, y); }
        set { SetState(x, y, value); }
    }

    public Cell.State this[Cell c]
    {
        get { return GetState(c.x, c.y); }
        set { SetState(c.x, c.y, value); }
    }

    public Grid(int w, int h)
    {
        m_rnd = new System.Random();

        m_grid = new Cell.State[w * h];

        Length = m_grid.Length;

        Width = w;
        Height = h;
    }

    public Cell GetCell(int index)
    {
        Cell cell = Cell.FromIndex(index, Width);
        cell.state = m_grid[index];

        return cell;
    }

    public Cell GetRandom()
    {
        int x = m_rnd.Next(Width);
        int y = m_rnd.Next(Height);

        return new Cell(x, y, GetState(x, y));
    }

    public void Fill(Cell.State value)
    {
        for (int i = 0; i < m_grid.Length; ++i)
        {
            m_grid[i] = value;
        }
    }

    public Cell.State GetState(int x, int y)
    {
        return m_grid[Cell.ToIndex(x, y, Width)];
    }

    public void SetState(int x, int y, Cell.State state)
    {
        m_grid[Cell.ToIndex(x, y, Width)] = state;
    }

    public void AddState(Cell cell, Cell.State state)
    {
        m_grid[Cell.ToIndex(cell.x, cell.y, Width)] |= state;
    }

    public Cell ChoiceNeighbor(Cell origin)
    {
        return ChoiceNeighbor(origin, st => st == Cell.State.None);
    }

    public Cell ChoiceNeighbor(Cell origin, System.Predicate<Cell.State> match)
    {
        Cell[] neighbors = FindNeighbors(origin, match);

        if (neighbors.Length > 0)
        {
            return neighbors[0];
        }

        return Cell.invalid;
    }

    public Cell[] GetNeighbors(Cell origin)
    {
        List<Cell> neighbors = new List<Cell>();

        if (origin.x > 0)
        {
            int x = origin.x - 1;
            int y = origin.y;

            neighbors.Add(new Cell(x, y, GetState(x, y)));
        }

        if (origin.x < Width - 1)
        {
            int x = origin.x + 1;
            int y = origin.y;

            neighbors.Add(new Cell(x, y, GetState(x, y)));
        }

        if (origin.y > 0 )
        {
            int x = origin.x;
            int y = origin.y - 1;

            neighbors.Add(new Cell(x, y, GetState(x, y)));
        }

        if (origin.y < Height - 1)
        {
            int x = origin.x;
            int y = origin.y + 1;

            neighbors.Add(new Cell(x, y, GetState(x, y)));
        }

        if (neighbors.Count > 1)
        {
            Cell[] shuffled = new Cell[neighbors.Count];

            int length = shuffled.Length;

            for (int i = 0; i < length; ++i)
            {
                int index = m_rnd.Next(length - i);

                shuffled[i] = neighbors[index];
                neighbors.RemoveAt(index);
            }

            return shuffled;
        }
        else
        {
            return neighbors.ToArray();
        }
    }

    public Cell[] FindNeighbors(Cell origin, System.Predicate<Cell.State> match)
    {
        List<Cell> neighbors = new List<Cell>();

        if (origin.x > 0 && match(GetState(origin.x - 1, origin.y)))
        {
            int x = origin.x - 1;
            int y = origin.y;

            neighbors.Add(new Cell(x, y, GetState(x, y)));
        }

        if (origin.x < Width - 1 && match(GetState(origin.x + 1, origin.y)))
        {
            int x = origin.x + 1;
            int y = origin.y;

            neighbors.Add(new Cell(x, y, GetState(x, y)));
        }

        if (origin.y > 0 && match(GetState(origin.x, origin.y - 1)))
        {
            int x = origin.x;
            int y = origin.y - 1;

            neighbors.Add(new Cell(x, y, GetState(x, y)));
        }

        if (origin.y < Height - 1 && match(GetState(origin.x, origin.y + 1)))
        {
            int x = origin.x;
            int y = origin.y + 1;

            neighbors.Add(new Cell(x, y, GetState(x, y)));
        }

        if (neighbors.Count > 1)
        {
            Cell[] shuffled = new Cell[neighbors.Count];

            int length = shuffled.Length;

            for (int i = 0; i < length; ++i)
            {
                int index = m_rnd.Next(length - i);

                shuffled[i] = neighbors[index];
                neighbors.RemoveAt(index);
            }

            return shuffled;
        }
        else
        {
            return neighbors.ToArray();
        }
    }

    public override string ToString()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x)
            {
                sb.Append(m_grid[Cell.ToIndex(x, y, Width)] + ",");
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}
