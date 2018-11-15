using System;
using System.Collections.Generic;

public class Grid
{
    public int Length { get; protected set; }

    public int Width { get; protected set; }
    public int Height { get; protected set; }

    private Random m_rnd;

    private int[] m_grid;

    public int this[int index]
    {
        get { return m_grid[index]; }
        set { m_grid[index] = value; }
    }

    public int this[int x, int y]
    {
        get { return GetValue(x, y); }
        set { SetValue(x, y, value); }
    }

    public int this[Cell c]
    {
        get { return GetValue(c.x, c.y); }
        set { SetValue(c.x, c.y, value); }
    }

    public Grid(int w, int h)
    {
        m_rnd = new Random();

        m_grid = new int[w * h];

        Length = m_grid.Length;

        Width = w;
        Height = h;
    }

    public Cell GetCell(int index)
    {
        Cell cell = Cell.FromIndex(index, Width);
        cell.value = m_grid[index];

        return cell;
    }

    public Cell GetRandom()
    {
        int x = m_rnd.Next(Width);
        int y = m_rnd.Next(Height);

        return new Cell(x, y, GetValue(x, y));
    }

    public void Fill(int value)
    {
        for (int i = 0; i < m_grid.Length; ++i)
        {
            m_grid[i] = value;
        }
    }

    public int GetValue(int x, int y)
    {
        return m_grid[Cell.ToIndex(x, y, Width)];
    }

    public void SetValue(int x, int y, int value)
    {
        m_grid[Cell.ToIndex(x, y, Width)] = value;
    }

    public void SetDir(Cell cell, Cell.Dir dir)
    {
        m_grid[Cell.ToIndex(cell.x, cell.y, Width)] |= (int)dir;
    }

    public void SetReverseDir(Cell cell, Cell.Dir dir)
    {
        Cell.Dir revDir;

        switch (dir)
        {
            case Cell.Dir.N: revDir = Cell.Dir.S; break;
            case Cell.Dir.S: revDir = Cell.Dir.N; break;
            case Cell.Dir.E: revDir = Cell.Dir.W; break;
            case Cell.Dir.W: revDir = Cell.Dir.E; break;

            default:
                revDir = Cell.Dir.None;
                break;
        }

        m_grid[Cell.ToIndex(cell.x, cell.y, Width)] |= (int)revDir;
    }

    public Cell ChoiceNeighbor(Cell origin, System.Predicate<int> match)
    {
        Cell[] neighbors = FindNeighbors(origin, match);

        if (neighbors.Length > 0)
        {
            // array shuffle
            for (int i = 0; i < neighbors.Length; ++i)
            {
                int n = m_rnd.Next(neighbors.Length);

                Cell temp = neighbors[i];
                neighbors[i] = neighbors[n];
                neighbors[n] = temp;
            }

            return neighbors[0];
        }

        return Cell.invalid;
    }

    public Cell ChoiceNeighbor(Cell origin, int value = 0)
    {
        Cell[] neighbors = GetNeighbors(origin);

        if (neighbors.Length > 0)
        {
            // array shuffle
            for (int i = 0; i < neighbors.Length; ++i)
            {
                int n = m_rnd.Next(neighbors.Length);

                Cell temp = neighbors[i];
                neighbors[i] = neighbors[n];
                neighbors[n] = temp;
            }

            for (int i = 0; i < neighbors.Length; ++i)
            {
                if (neighbors[i].value == value)
                {
                    return neighbors[i];
                }
            }
        }

        return Cell.invalid;
    }

    public Cell[] FindNeighbors(Cell origin, System.Predicate<int> match)
    {
        List<Cell> neighbors = new List<Cell>();

        if (origin.x > 0 && match(GetValue(origin.x - 1, origin.y)))
        {
            int x = origin.x - 1;
            int y = origin.y;

            neighbors.Add(new Cell(x, y, GetValue(x, y)));
        }

        if (origin.x < Width - 1 && match(GetValue(origin.x + 1, origin.y)))
        {
            int x = origin.x + 1;
            int y = origin.y;

            neighbors.Add(new Cell(x, y, GetValue(x, y)));
        }

        if (origin.y > 0 && match(GetValue(origin.x, origin.y - 1)))
        {
            int x = origin.x;
            int y = origin.y - 1;

            neighbors.Add(new Cell(x, y, GetValue(x, y)));
        }

        if (origin.y < Height - 1 && match(GetValue(origin.x, origin.y + 1)))
        {
            int x = origin.x;
            int y = origin.y + 1;

            neighbors.Add(new Cell(x, y, GetValue(x, y)));
        }

        return neighbors.ToArray();
    }

    public Cell[] GetNeighbors(Cell origin)
    {
        List<Cell> neighbors = new List<Cell>();

        if (origin.x > 0)
        {
            int x = origin.x - 1;
            int y = origin.y;

            neighbors.Add(new Cell(x, y, GetValue(x, y)));
        }

        if (origin.x < Width - 1)
        {
            int x = origin.x + 1;
            int y = origin.y;

            neighbors.Add(new Cell(x, y, GetValue(x, y)));
        }

        if (origin.y > 0)
        {
            int x = origin.x;
            int y = origin.y - 1;

            neighbors.Add(new Cell(x, y, GetValue(x, y)));
        }

        if (origin.y < Height - 1)
        {
            int x = origin.x;
            int y = origin.y + 1;

            neighbors.Add(new Cell(x, y, GetValue(x, y)));
        }

        return neighbors.ToArray();
    }

    public override string ToString()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x)
            {
                sb.Append(m_grid[Cell.ToIndex(x, y, Width)] + "\t");
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}
