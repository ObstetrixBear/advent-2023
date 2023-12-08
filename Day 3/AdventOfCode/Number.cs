namespace AdventOfCode;

public record GridPoint(int Column, int Row);

public class Number : IEquatable<Number>
{
    private readonly List<string> _map;
    private readonly int _mapWidth;
    private readonly List<GridPoint> _pointsOccupied;

    public Number(GridPoint point, List<string> map)
    {
        _map = map;
        _mapWidth = _map.First().Length;

        _pointsOccupied = MapArea(point);
        Get = int.Parse(string.Join(string.Empty,
            _pointsOccupied.Select(x => _map.ElementAt(x.Row)[x.Column])));
    }

    public GridPoint GridPoint => _pointsOccupied.First();

    private bool IsNumber(GridPoint gridPoint) =>
        char.IsDigit(_map.ElementAt(gridPoint.Row)[gridPoint.Column]);

    public int Get { get; } = 0;

    private List<GridPoint> MapArea(GridPoint point)
    {
        var points = new List<GridPoint>();
        var leftmostPosition = 0;

        // Find leftmost point.
        for (var x = point.Column; x >= 0; x--)
        {
            if (!IsNumber(new GridPoint(x, point.Row)))
            {
                break;
            }
            leftmostPosition = x;
        }

        var candidate = leftmostPosition;

        // Find points to the right of that.
        while (candidate < _mapWidth && IsNumber(new GridPoint(candidate, point.Row)))
        {
            points.Add(new GridPoint(candidate++, point.Row));
        }

        return points;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Number);
    }

    public bool Equals(Number other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return GridPoint.Equals(other.GridPoint) && Get == other.Get;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            hash = hash * 23 + GridPoint.GetHashCode();
            hash = hash * 23 + Get.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(Number left, Number right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Number left, Number right)
    {
        return !(left == right);
    }
}