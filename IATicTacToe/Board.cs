using System.Text;

namespace IATicTacToe;

public class Board
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    private List<Slot> grid = new();

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
        for (int i = 0; i < Width * Height; i++)
        {
            grid.Add(Slot.None);
        }
    }

    public Slot Get(int row, int column)
    {
        return grid[IndexFromPosition(row, column)];
    }

    public void Set(int row, int column, Slot slot)
    {
        grid[IndexFromPosition(row, column)] = slot;
    }

    public int IndexFromPosition(int row, int column)
    {
        return row * Width + column;
    }

    public (int, int) PositionFromIndex(int index)
    {
        return (index / Width, index % Width);
    }

    public bool IsDraw()
    {
        return !OpenSlotIndexes().Any();
    }

    public Slot Winner()
    {
        for (int column = 0; column < Width; column++)
        {
            var cross_count = 0;
            var circle_count = 0;

            for (int row = 0; row < Height; row++)
            {
                switch (Get(row,column))
                { 
                    case Slot.Cross:
                        cross_count++;
                        break;
                    case Slot.Circle:
                        circle_count++;
                        break;
                }
            }

            if (cross_count == Height)
            {
                return Slot.Cross;
            } else if (circle_count == Height)
            {
                return Slot.Circle;
            }
        }
        for (int row = 0; row < Height; row++)
        {
             var cross_count = 0;
             var circle_count = 0;
 
             for (int column = 0; column < Width; column++)
             {
                 switch (Get(row,column))
                 { 
                     case Slot.Cross:
                         cross_count++;
                         break;
                     case Slot.Circle:
                         circle_count++;
                         break;
                 }
             }
 
             if (cross_count == Width)
             {
                 return Slot.Cross;
             }

             if (circle_count == Width)
             {
                 return Slot.Circle;
             }
        }

        return Slot.None;
    }

    public IEnumerable<int> OpenSlotIndexes()
    {
        return grid.Select((v, i) => (v, i)).Where(v => v.v == Slot.None).Select((v, i) => v.i).ToList();
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        for (int row = 0; row < Height; row++)
        {
            for (int column = 0; column < Width; column++)
            {
                builder.Append(this.Get(row, column) switch
                {
                    Slot.Cross => 'X',
                    Slot.Circle => 'O',
                    Slot.None => ' ',
                    _ => throw new ArgumentOutOfRangeException()
                });
            }

            builder.Append('\n');
        }

        return builder.ToString();
    }
}