namespace IATicTacToe;

// Cross +
// Circle -
using Position = ValueTuple<int, int>;
public class Bot
{
    private Board board;
    public Slot Side { get; private set; }
    public bool CanWin = true;

    public Dictionary<string, (double, Position?)> cache = new();

    public Bot(Slot side, Board board)
    {
        this.board = board;
        this.Side = side;
    }

    protected double ScoreBoard()
    {
        return board.Winner() switch
        {
            Slot.Cross => 10.0,
            Slot.Circle => -10.0,
            Slot.None => 0.0,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private Slot Opposite(Slot side)
    {
        return side switch
        {
            Slot.Cross => Slot.Circle,
            Slot.Circle => Slot.Cross,
            Slot.None => Slot.None
        };
    }

    public bool MakeMove()
    {
        var (_, position) = minimax_to_completion(Side);
 
        if (position.HasValue)
        {
            var (row, column) = position.Value;
            board.Set(row, column, Side);
            return true;
        }

        return false;
    }

    private (double, Position?) minimax_to_completion(Slot side)
    {
        var key = board.ToString();
        if (cache.ContainsKey(key))
        {
            return cache[key];
        }
        if (ScoreBoard() != 0 && CanWin)
        {
            cache[board.ToString()] = (ScoreBoard(), null); 
            return (ScoreBoard(), null);
        }
        if (board.IsDraw() && ScoreBoard() == 0)
        {
            cache[board.ToString()] = (ScoreBoard(), null);
            return (ScoreBoard(), null);
        }

        

        double bestScore = side switch
        {
            Slot.Cross => Double.NegativeInfinity,
            Slot.Circle => Double.PositiveInfinity,
            _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
        };
        Position? bestPosition = null;

        foreach (var position in board.OpenSlotIndexes())
        {
            var pos = board.PositionFromIndex(position);
            var (row, column) = pos;
            board.Set(row, column, side);
            var (newScore, _) = minimax_to_completion(Opposite(side));
            if (side == Slot.Cross && newScore > bestScore)
            {
                bestScore = newScore;
                bestPosition = pos;
            }
            if (side == Slot.Circle && newScore < bestScore)
            {
                bestScore = newScore;
                bestPosition = pos;
            }
            board.Set(row, column, Slot.None);
        }

        cache[board.ToString()] = (bestScore, bestPosition);
        return (bestScore, bestPosition);
    }
}